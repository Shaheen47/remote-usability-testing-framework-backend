using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using session_service.Contracts.Services;
using session_service.Entities;

namespace session_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController: ControllerBase
    {
        private ISessionService sessionService;

        public SessionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        [HttpPost]
        [Route("create-session")]
        public async Task<IActionResult> createSession()
        {
            Session sessionModeratorDto=await sessionService.createSession();
            return Created("session",sessionModeratorDto);
        }
        
        
        [HttpPost]
        [Route("join-as-moderator")]
        public async Task<IActionResult> joinAsModerator([FromBody] SessionLoginDto loginDto)
        {
            SessionModeratorDto sessionModeratorDto=await sessionService.joinAsModerator(loginDto.sessionId,"moderator");
            return Created("moderator",sessionModeratorDto);
        }
        
        [HttpPost]
        [Route("join-as-participant")]
        public async Task<IActionResult> getParticipantInformation([FromBody] SessionLoginDto loginDto)
        {
            SessionParticipantDto sessionParticipantDto=await sessionService.joinAsParticipant(loginDto.sessionId,"participant");
            return Created("participant",sessionParticipantDto);
        }
        
        [HttpPost]
        [Route("join-as-observer")]
        public async Task<IActionResult> joinAsObserver([FromBody] SessionLoginDto loginDto)
        {
            SessionObserverDto sessionObserverDto=await sessionService.joinAsObserver(loginDto.sessionId,"observer");
            return Created("observer",sessionObserverDto);
        }
        
        
        
    }
}