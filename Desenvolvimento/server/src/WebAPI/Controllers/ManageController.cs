using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Linq;
using WebAPI.ViewModels.Pagination;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities.Identity;
using Infrastructure.Data;
using ApplicationCore.Helpers.Pagination;
using WebAPI.ViewModels;
using ApplicationCore.Extensions;
using ApplicationCore.Services;
using ApplicationCore.Interfaces.Services;
using System.Collections.Generic;
using ApplicationCore.Entities;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class ManageController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ISetorService _setorService;
        private readonly IUserSetorService _userSetorService;
        private readonly IAppLogger<ManageController> _logger;
        private readonly IEmailService _emailService;

        public ManageController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ISetorService setorService,
            IUserSetorService userSetorService,
            IAppLogger<ManageController> logger,
            IEmailService emailService
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _setorService = setorService;
            _userSetorService = userSetorService;
            _logger = logger;
            _emailService = emailService;
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("user/find")]
        public async Task<IActionResult> FindUser([FromBody] GridSettings postGrid)
        {
            return await Task.Run(() =>
            {
                var users = _userManager.Users.Include(u => u.Roles).ThenInclude(r => r.Role);
                var usersViewModel = users.Select(u => new UserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Roles = u.Roles.Select(r => new RoleViewModel
                    {
                        Id = r.RoleId,
                        Name = r.Role.Name
                    }),
                    RolesSting = string.Join(',', u.Roles.Select(r => r.Role.Name))
                });
                var rows = usersViewModel.Where(postGrid, out int totalItens);
                var result = new Pagination<UserViewModel>(rows, postGrid.page, postGrid.rows, totalItens);
                return Ok(result);
            });
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("role/find")]
        public async Task<IActionResult> FindRole([FromBody] GridSettings postGrid)
        {
            return await Task.Run(() =>
            {
                var roles = _roleManager.Roles;
                var rolesViewModel = roles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                });
                var rows = rolesViewModel.Where(postGrid, out int totalItens);
                var result = new Pagination<RoleViewModel>(rows, postGrid.page, postGrid.rows, totalItens);
                return Ok(result);
            });
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            return Ok(await _userManager.FindByIdAsync(id));
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpGet("role/{id}")]
        public async Task<IActionResult> GetRole(string id)
        {
            return Ok(await _roleManager.FindByIdAsync(id));
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPut("user")]
        public async Task<IActionResult> PutUser([FromBody] UserViewModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var userIdentity = await _userManager.FindByIdAsync(user.Id.ToString());

            userIdentity.UserName = user.UserName;
            userIdentity.Email = user.Email;
            await _userManager.UpdateAsync(userIdentity);

            var userIdentityRoles = _userManager.GetRolesAsync(userIdentity).Result.OrderBy(x => x);
            var userRoles = user.Roles.Select(r => r.Name).OrderBy(x => x);
            if (!userIdentityRoles.SequenceEqual(userRoles))
            {
                await _userManager.RemoveFromRolesAsync(userIdentity, userIdentityRoles);
                await _userManager.AddToRolesAsync(userIdentity, userRoles);
            }

            var userIdentitySetore = _userSetorService.GetSetoresAsync(userIdentity.Id).Result.OrderBy(x => x);
            var userSetores = user.UserSetores.Select(x => x.SetorId).OrderBy(x => x);

            if (!userIdentitySetore.SequenceEqual(userSetores))
            {
                await _userSetorService.RemoveFromSetoresAsync(userIdentity.Id, userIdentitySetore);
                await _userSetorService.AddFromSetoresAsync(
                    user.UserSetores.Select(x => new UserSetor()
                    {
                        SetorId = x.SetorId,
                        UserId = x.UserId
                    }));
            }

            return Ok();
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPut("role")]
        public async Task<IActionResult> PutRole([FromBody] RoleViewModel role)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var roleIdentity = await _roleManager.FindByIdAsync(role.Id.ToString());

            roleIdentity.Name = role.Name;
            await _roleManager.UpdateAsync(roleIdentity);

            return Ok();
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("user")]
        public async Task<IActionResult> PostUser([FromBody] UserViewModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var newUser = new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = false
            };

            var findUser = await _userManager.FindByNameAsync(newUser.UserName);

            if (findUser == null)
            {
                var resultado = await _userManager.CreateAsync(newUser, "asdasd");
                if (resultado.Succeeded)
                {
                    var userRoles = user.Roles.Select(r => r.Name);
                    _userManager.AddToRolesAsync(newUser, userRoles).Wait();

                    _userSetorService.AddFromSetoresAsync(
                        user.UserSetores.Select(x => new UserSetor()
                        {
                            SetorId = x.SetorId,
                            UserId = newUser.Id
                        })).Wait();
                    return Ok();
                }
            }
            return BadRequest(GenerateClientError(new string[] { "Login já registrado." }));
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("role")]
        public async Task<IActionResult> PostRole([FromBody] RoleViewModel role)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var newRole = new Role()
            {
                Name = role.Name
            };

            var findRole = await _roleManager.FindByNameAsync(role.Name);

            if (findRole == null)
            {
                var resultado = await _roleManager.CreateAsync(newRole);
                if (resultado.Succeeded)
                {
                    return Ok();
                }
            }
            return BadRequest(GenerateClientError(new string[] { "Role já registrado." }));
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user);
            return Ok();
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpDelete("role/{id}")]
        public async Task<IActionResult> DeleteRole(long id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(role);
            return Ok();
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpGet("allRoles/{userId}")]
        public async Task<IActionResult> GetAllRoles(string userId)
        {
            if (userId == "0")
            {
                var roles = await _roleManager.Roles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                }).ToListAsync();
                return Ok(roles);
            }
            else
            {
                var userIdentity = await _userManager.FindByIdAsync(userId);
                var userIdentityRoles = _userManager.GetRolesAsync(userIdentity).Result;
                var roles = await _roleManager.Roles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Enabled = userIdentityRoles.Any(x => x == r.Name)
                }).ToListAsync();
                return Ok(roles);
            }
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpGet("allSetor/{userId}")]
        public async Task<IActionResult> GetAllSetor(long userId)
        {
            var query = await _setorService.GetQueryableAsync();
            var setores = await query
                .Include(s => s.Empresa)
                .ToListAsync();

            var queryUserSetor = await _userSetorService.GetQueryableAsync();

            var userSetor = setores
                .Select(x => new UserSetorViewModel()
                {
                    Nome = string.Concat(x.Empresa.Nome, _setorService.RetornaNives(x.SetorPaiId), " / ", x.Nome),
                    Enabled = queryUserSetor.Where(y => y.SetorId == x.Id && y.UserId == userId).Count() > 0,
                    SetorId = x.Id,
                    UserId = userId,
                    SetorPaiId = x.SetorPaiId
                })
                .OrderBy(x => x.Nome);

            return Ok(userSetor);
        }

        [HttpPost("current/alterar/password")]
        public async Task<IActionResult> CurrentAlterarPassword([FromBody] AlterarUsuarioPasswordViewModel model)
        {
            if (model.CheckPassword != model.NewPassword)
                throw new Exception("Nova senha diferente da confirmação de nova senha");

            var user = await _userManager.FindByIdAsync(UserClaim().Id.ToString());

            if (user == null)
                throw new Exception("Erro ao verificar usuário.");

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                if (result.Errors.Any(x => x.Code == "PasswordMismatch"))
                {
                    throw new Exception("Senha atual inválida");
                }
                throw new Exception("Erro ao  atualizar senha, tente novamente.");
            }

            _emailService.Send(
                    $"SCA: Senha alterada com sucesso!",
                    $"Sua senha foi aterada com sucesso por '{UserClaim().UserName}' ({UserClaim().Email}) no dia: {DateTime.Now}",
                    new string[] {
                    user.Email
                    }
                    );

            return Ok();
        }
    }
}
