using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Este tipo elimina a necessidade de depender diretamente do serviço de cliente de rest no ASP.NET Core
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRestService
    {
        T Get<T>(string requestUri) where T : new();
        Task<T> GetAsync<T>(string requestUri) where T : new();
        T Post<T>(string requestUri, object body = null) where T : new();
        Task<T> PostAsync<T>(string requestUri, object body = null) where T : new();
        Task<T> PostUploadAsync<T>(string requestUri, byte[] file, string filename) where T : new();
        Task<byte[]> GetBytesAsync(string requestUri);

    }

    public interface IRegraNegocioRestService : IRestService
    {
    }

    public interface ISgaRestService : IRestService
    {
    }

    public interface IEdoWebApiRestService : IRestService
    {
    }

    public interface IGuardiao3ClienteRestService : IRestService
    {
    }
    public interface ISeoRestService : IRestService
    {
    }
}
