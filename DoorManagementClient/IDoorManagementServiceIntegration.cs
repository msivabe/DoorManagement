using GateManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoorManagementClient
{
    public interface IDoorManagementServiceIntegration
    {
        Task<IEnumerable<Door>> GetAllDoors();

        Task<int> DeleteDoor(string doorId);

        Task<int> AddDoor(Door door);

        Task<int> UpdateDoor(Door door);
    }
}
