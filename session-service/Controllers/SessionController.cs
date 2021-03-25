using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using session_service.Contracts.Services;
using session_service.Core.Exceptions;
using session_service.Dtos;
using session_service.Entities;

namespace session_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController: ControllerBase
    {
        private ISessionService sessionService;
        private IModeratorService moderatorService;

        public SessionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        [HttpPost]
        public async Task<IActionResult> createSession([FromBody] SessionCreationRequestDto creationDto)
        {
            /*var moderator = await moderatorService.getModerator(creationDto.moderatorId);*/
            
            SessionCreationResponseDto sessionCreationDto;
            if (creationDto.isRecorded)
                sessionCreationDto=await sessionService.createSessionWithRecording();
                
            else
                sessionCreationDto=await sessionService.createSession();
            var session=await sessionService.getSession(sessionCreationDto.id);
            /*moderator.sessions.Add(session);
            await moderatorService.updateModerator(moderator);*/
            return Created("session",sessionCreationDto);

        }
        
        [HttpDelete("{sessionId}")]
        public async Task<IActionResult> stopSession(string sessionId)
        {
            Session session = await sessionService.getSession(sessionId);
            await sessionService.stopSession(session);
            return NoContent();
        }
        
        
        [HttpGet("{sessionId}")]
        public async Task<IActionResult> getSession(string sessionId)
        {
            var session =await sessionService.getSession(sessionId);
            return Ok(session);
        }
        
        [HttpGet("{sessionId}")]
        public async Task<IActionResult> getAllModeratorSessions(string sessionId,[FromBody] ModeratorLoginDto loginDto )
        {
            var moderator=await moderatorService.getModerator(loginDto.id);
            var sessions = moderator.sessions;
            return Ok(sessions);
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
        
        [HttpGet]
        [Route("get-recording-url")]
        public async Task<IActionResult> getRecordingUrl([FromBody] SessionLoginDto loginDto)
        {
            var session = await sessionService.getSession(loginDto.sessionId);
            var url = sessionService.getRecordingUrl(session);
            return Ok(url);
        }
        
        [HttpPost]
        [Route("reply-screensharing")]
        public async Task<IActionResult> replySession([FromBody] SessionLoginDto loginDto)
        {
            Session session = await sessionService.getSession(loginDto.sessionId);
            sessionService.replyScreensharing(session);
            return Ok();
        }
        
        
    }
}