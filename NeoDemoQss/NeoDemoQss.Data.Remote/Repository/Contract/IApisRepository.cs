using System.Net.Http;
using System.Threading.Tasks;

namespace NeoDemoQss.Data.Remote.Repository.Contract
{
    public interface IApisRepository
    {
        Task<T> GetAsync<T>(string uri, string authToken = "", bool hasJsonResponse = false);
        Task<T> PostAsync<TData, T>(string uri, TData data, string authToken = "");
        Task<T> PutAsync<T>(string uri, T data, string authToken = "");
        Task<T> DeleteAsync<T>(string uri, string authToken = "");

        Task<T> SendAsync<T>(string uri, string data, HttpMethod method, string authToken = "");
    }
}
