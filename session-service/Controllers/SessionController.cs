using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using session_service.Contracts.Services;
using session_service.Core.Exceptions;
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
            SessionCreationDto sessionCreationDto=await sessionService.createSession();
            return Created("session",sessionCreationDto);
        }
        
        
        [HttpPost]
        [Route("join-as-moderator")]
        public async Task<IActionResult> joinAsModerator([FromBody] SessionLoginDto loginDto)
        {
            try
            {
                SessionModeratorDto sessionModeratorDto=await sessionService.joinAsModerator(loginDto.sessionId,"moderator");
                return Created("moderator",sessionModeratorDto);
            }
            catch (ModeratorAlreadyJoinedExecption e)
            {
                //ToDo check response type
                return BadRequest(e.Message);
            }
            
        }
        
        [HttpPost]
        [Route("join-as-participant")]
        public async Task<IActionResult> getParticipantInformation([FromBody] SessionLoginDto loginDto)
        {
            try
            {
                SessionParticipantDto sessionParticipantDto=await sessionService.joinAsParticipant(loginDto.sessionId,"participant");
                return Created("participant",sessionParticipantDto);
            }
            catch (ParticipantAlreadyJoinedExecption e)
            {
                //ToDo check response type
                return BadRequest(e.Message);
            }
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