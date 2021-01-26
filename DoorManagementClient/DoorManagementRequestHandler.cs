using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoorManagementClient
{
    public class DoorManagementRequestHandler : IDoorManagementRequestHandler
    {
        private static HttpClient _client = new HttpClient();

        public DoorManagementRequestHandler()
        {
            _client.Timeout = TimeSpan.FromMilliseconds(4000);
        }
       
        public Task<HttpResponseMessage> SendRequest(HttpRequestMessage httpRequestMessage)
            =>  _client.SendAsync(httpRequestMessage);
        
    }
}
