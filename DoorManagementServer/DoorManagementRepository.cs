using GateManagement.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoorManagementServer
{
    public sealed class DoorManagementRepository : IDoorManagementRepository
    {
        private ConcurrentDictionary<string,Door> doorsCollection = new ConcurrentDictionary<string,Door>(10,30);

        public async Task<Door> AddDoor(Door door)
        {
            if(door == null)
            {
                throw new ArgumentException("Door object is null");
            }

            if (!doorsCollection.ContainsKey(door.Id))
            {
                if (doorsCollection.TryAdd(door.Id, door))
                {
                    return door;
                }
                throw new DoorCreationFailedException("Door not added into repository");
            }
            else
                throw new DoorCreationFailedException("Id already exists");
        }

        public async Task DeleteDoorById(string id)
        {
            doorsCollection.TryRemove(id,out var _);
        }



        public async Task<IEnumerable<Door>> GetAllDoors()
        {
           return doorsCollection.Values;
        }

        public async Task<Door> GetDoorById(string id)
        => doorsCollection.TryGetValue(id,out var door)?door:null;
            

        public async Task DeleteDoor(string id)
        {
            if (!doorsCollection.TryRemove(id, out var _))
                throw new DoorManagementException("Delete door failed (id not found)");
        }

        public async Task UpdateDoor(Door door)
        {
            if (!doorsCollection.TryUpdate(door.Id, door, doorsCollection[door.Id]))
                 throw new DoorManagementException("Update door failed (id not found)");
        }
    }

    
}
