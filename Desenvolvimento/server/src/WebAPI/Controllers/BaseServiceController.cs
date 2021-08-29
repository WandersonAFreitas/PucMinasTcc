using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities;
using ApplicationCore.Helpers.Pagination;
using WebAPI.ViewModels.Pagination;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Base;
using System;

namespace WebAPI.Controllers
{
    public class BaseServiceController<TPrimaryKey, TEntity> : BaseController
        where TPrimaryKey : IComparable, IConvertible, IComparable<TPrimaryKey>, IEquatable<TPrimaryKey>
        where TEntity : class, IBaseEntity<TPrimaryKey>
    {

        protected readonly IService<TPrimaryKey, TEntity> _service;

        public BaseServiceController(IService<TPrimaryKey, TEntity> service)
        {
            _service = service;
        }

        [HttpPost("find")]
        [ProducesResponseType(200)]
        public virtual async Task<IActionResult> GetFind([FromBody] GridSettings postGrid)
        {
            var query = await _service.GetQueryableAsync();
            var rows = query.Where(postGrid, out int totalItens).OrderBy(x => x.Id);
            var result = new Pagination<TEntity>(rows, postGrid.page, postGrid.rows, totalItens);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public virtual async Task<IActionResult> Get(TPrimaryKey id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPut]
        [ProducesResponseType(200)]
        public virtual async Task<IActionResult> Put([FromBody] TEntity tag)
        {
            await _service.UpdateAsync(tag);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public virtual async Task<IActionResult> Post([FromBody] TEntity tag)
        {
            return Ok(await _service.AddAsync(tag));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public virtual async Task<IActionResult> Delete(TPrimaryKey id)
        {
            var tag = await _service.GetByIdAsync(id);
            await _service.DeleteAsync(tag);
            return Ok();
        }

        [HttpGet("")]
        [ProducesResponseType(200)]
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _service.GetAllAsync();
        }
    }
}