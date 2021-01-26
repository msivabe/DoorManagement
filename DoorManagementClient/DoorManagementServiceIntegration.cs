using GateManagement.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoorManagementClient
{
    public class DoorManagementServiceIntegration : IDoorManagementServiceIntegration
    {
        private string _serviceBaseUri;
        private IDoorManagementRequestHandler _doorManagementRequestHandler;
        public DoorManagementServiceIntegration(IAppConfig appConfig, IDoorManagementRequestHandler doorManagementRequestHandler)
        {
            _serviceBaseUri = appConfig.GetBaseServiceUrl();
            this._doorManagementRequestHandler = doorManagementRequestHandler;
        }

     
        public async Task<IEnumerable<Door>> GetAllDoors()
        {

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _serviceBaseUri + "doors");
            httpRequestMessage.Headers.Add("X-Auth-Security", "830866A3-0964-411E-B0E8-3A223438AF8E");
            HttpResponseMessage result;
            try
            {
                 result = await _doorManagementRequestHandler.SendRequest(httpRequestMessage).ConfigureAwait(false);
            }
            catch(Exception e)
            {
                throw new DoorManagementAppException(e.Message);
            }
            if (result != null && result.IsSuccessStatusCode)
            {
                var jsonResponseString = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Door[]>(jsonResponseString);
            }
            throw new DoorManagementAppException($"GetAllDoors service call failed.Returned Code:{result?.StatusCode}");
        }

        public async Task<int> DeleteDoor(string doorId)
        {

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete,  $"{_serviceBaseUri}door/{doorId}");
            httpRequestMessage.Headers.Add("X-Auth-Security", "830866A3-0964-411E-B0E8-3A223438AF8E");
            try
            {
                HttpResponseMessage result = await _doorManagementRequestHandler.SendRequest(httpRequestMessage).ConfigureAwait(false);

                if (result == null || result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Serilog.Log.Error($"DeleteDoor service failed.StatusCode:{result?.StatusCode}");
                    return -1;
                }
                Serilog.Log.Information("DeleteDoor succeeded");
                return 0;
            }
            catch (Exception e)
            {
                Serilog.Log.Error("DeleteDoor service failed", e);
                return -1;
            }
            
        }

        public async Task<int> AddDoor(Door door)
        {
            try
            {

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_serviceBaseUri}door");
                httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(door));
                httpRequestMessage.Content.Headers.Clear();
                httpRequestMessage.Content.Headers.Add("content-type", "application/json");
                httpRequestMessage.Content.Headers.Add("X-Auth-Security", "830866A3-0964-411E-B0E8-3A223438AF8E");

                HttpResponseMessage result = await _doorManagementRequestHandler.SendRequest(httpRequestMessage).ConfigureAwait(false);
                if (result == null || result.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    Serilog.Log.Error($"AddDoor service failed.StatusCode:{result?.StatusCode}");
                    return -1;
                }
                Serilog.Log.Information("AddDoor succeeded");
                return 0;
            }
            catch (Exception e)
            {
                Serilog.Log.Error("AddDoor service failed", e);
                return -1;
            }

        }

        public async Task<int> UpdateDoor(Door door)
        {

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, $"{_serviceBaseUri}door/{door.Id}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(door))
            };
            httpRequestMessage.Content.Headers.Clear();
            httpRequestMessage.Content.Headers.Add("content-type", "application/json");
            httpRequestMessage.Content.Headers.Add("X-Auth-Security", "830866A3-0964-411E-B0E8-3A223438AF8E");
            try
            {
                HttpResponseMessage result = await _doorManagementRequestHandler.SendRequest(httpRequestMessage).ConfigureAwait(false);
                if (result == null || result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Serilog.Log.Error($"UpdateDoor service failed.StatusCode:{result?.StatusCode}");
                    return -1;
                }
                Serilog.Log.Information("UpdateDoor succeeded");
                return 0;
            }
            catch (Exception e)
            {
                Serilog.Log.Error("UpdateDoor service failed", e);
                return -1;
            }

        }
    }
}
