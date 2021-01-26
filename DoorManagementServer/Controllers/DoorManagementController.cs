using GateManagement.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DoorManagementServer.Controllers
{
    [ApiController]
    [Route("DoorManagement")]
    public class DoorManagementController : ControllerBase
    {
        private readonly ILogger<DoorManagementController> _logger;
        private readonly IDoorManagementRepository _doorManagementRepository;

        public DoorManagementController(ILogger<DoorManagementController> logger,IDoorManagementRepository doorManagementRepository)
        {
            _logger = logger;
            _doorManagementRepository = doorManagementRepository;
        }

        [Route("/doors")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var doorsList = await _doorManagementRepository.GetAllDoors();
            return Ok(doorsList);
        }

        [Route("/door")]
        [HttpPost]
        public async Task<IActionResult> AddDoor(Door door)
        {
            _logger.LogDebug($"Create Door request received");
            var addedDoor = await _doorManagementRepository.AddDoor(door);
            _logger.LogInformation($"Door created. ID:{addedDoor.Id}");
            return Created($"/doors/{addedDoor.Id}", addedDoor);
        }

        [Route("/door/{doorId}")]
        [HttpGet]
        public async Task<IActionResult> Get(string doorId)
        {
            _logger.LogDebug($"Get Door request received.ID:{doorId}");
            var doorInfo = await _doorManagementRepository.GetDoorById(doorId);
            if (doorInfo != null)
            {
                _logger.LogDebug($"DoorID:{doorId} found ");
                return Ok(doorInfo);
            }
            else
            {
                _logger.LogWarning($"DoorID:{doorId} not found ");
                return NotFound();
            }
        }

        [Route("/door/{doorId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDoor(string doorId)
        {
            _logger.LogDebug($"Delete Door request received.ID:{doorId}");
            await _doorManagementRepository.DeleteDoorById(doorId);
            _logger.LogInformation($"Door ID:{doorId} deleted successfully");
            return Ok();
        }

        [Route("/door/{doorId}")]
        [HttpPut]
        public async Task<IActionResult> UpdateDoor(Door door)
        {
            _logger.LogDebug($"Update Door request received.ID:{door?.Id}");
            await _doorManagementRepository.UpdateDoor(door);
            _logger.LogInformation($"Door ID:{door?.Id} updated successfully");
            return Ok();
        }
    }
}
