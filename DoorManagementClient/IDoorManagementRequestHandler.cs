using System.Net.Http;
using System.Threading.Tasks;

namespace DoorManagementClient
{
    public interface IDoorManagementRequestHandler
    {
        Task<HttpResponseMessage> SendRequest(HttpRequestMessage httpRequestMessage);
    }
}
