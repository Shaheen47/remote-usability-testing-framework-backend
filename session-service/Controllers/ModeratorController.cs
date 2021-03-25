using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using session_service.Contracts.Services;
using session_service.Dtos;

namespace session_service.Controllers
{
    
    [ApiController]
    [Route("[Moderator]")]
    public class ModeratorController:ControllerBase
    {
        private IModeratorService moderatorService;


        [HttpPost]
        public async Task<IActionResult> createModerator([FromBody] ModeratorCreationDto creationDto)
        {
            var moderator=await moderatorService.createModerator(creationDto);
            return Created("moderator", moderator);
        }
    }
}