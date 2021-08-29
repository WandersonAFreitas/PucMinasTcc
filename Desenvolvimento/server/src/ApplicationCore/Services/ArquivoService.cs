using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;
using System.Threading.Tasks;
using ApplicationCore.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;

namespace ApplicationCore.Services
{
    public class ArquivoService : BaseService<long, Arquivo>, IArquivoService
    {
        public ArquivoService(IAsyncRepository<long, Arquivo> repository, IAppLogger<Arquivo> logger)
            : base(repository, logger) { }

        public override async Task<Arquivo> AddAsync(Arquivo entity)
        {
            var timeStamp = DateTime.Now.ToFileTime().ToString();
            var extensao = entity.Nome.Substring(entity.Nome.LastIndexOf("."));

            entity.Hash = timeStamp;
            entity.Extensao = extensao;
            entity.DataCriacao = DateTime.Now;
            entity.Tipo = MimeTypeMap.GetMimeType(extensao);

            return await _repository.AddAsync(entity);
        }

        public override async Task UpdateAsync(Arquivo entity)
        {
            var timeStamp = DateTime.Now.ToFileTime().ToString();
            var extensao = entity.Nome.Substring(entity.Nome.LastIndexOf("."));

            entity.Hash = timeStamp;
            entity.Extensao = extensao;
            entity.DataCriacao = DateTime.Now;
            entity.Tipo = MimeTypeMap.GetMimeType(extensao);

            await _repository.UpdateAsync(entity);
        }

        public async Task<Arquivo> GetByHashAsync(string hash)
        {
            var query = await _repository.ListQueryableAsync();
            return query.Where(x => x.Hash == hash).FirstOrDefault();
        }

        public async Task<List<Arquivo>> GetByHashesAsync(string[] hashes)
        {
            var arquivos = new List<Arquivo>();

            foreach (var hash in hashes)
            {
                var hashDecoded = HttpUtility.UrlDecode(hash);
                arquivos.Add(await GetByHashAsync(hashDecoded));
            }

            return arquivos;
        }
    }
}
