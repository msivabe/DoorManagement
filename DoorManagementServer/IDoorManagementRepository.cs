using GateManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoorManagementServer
{
    public interface IDoorManagementRepository
    {

        Task<Door> AddDoor(Door door);

        Task DeleteDoorById(string id);

        Task<IEnumerable<Door>> GetAllDoors();

        Task<Door> GetDoorById(string id);

        Task DeleteDoor(string id);

        Task UpdateDoor(Door door);

    }
}
