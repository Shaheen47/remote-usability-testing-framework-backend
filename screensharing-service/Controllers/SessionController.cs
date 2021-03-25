using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using screensharing_service.Contracts.Services;
using screensharing_service.Dtos;
using screensharing_service.Entities;

namespace screensharing_service.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class SessionController: ControllerBase
    {
        private ISessionService sessionService;
        private IScreenEventsReplyService screenEventsReplyService;
        private readonly IMapper Mapper;

        public SessionController(ISessionService sessionService,IScreenEventsReplyService screenEventsReplyService,IMapper Mapper)
        {
            this.sessionService = sessionService;
            this.screenEventsReplyService = screenEventsReplyService;
            this.Mapper = Mapper;
        }

        [HttpPost]
        [Route("create-session")]
        public async Task<IActionResult> createSession()
        {
            Session session=await sessionService.createSession();
            SessionWithoutReplyDto sessionWithoutReplyDto=Mapper.Map<Session, SessionWithoutReplyDto>(session);
            return Created("session",sessionWithoutReplyDto);
        }
        
        [HttpPost]
        [Route("create-session-with-recording")]
        public async Task<IActionResult> createSessionWithRecording()
        {
            Session session=await sessionService.createSessionWithRecording();
            return Created("session",session);
        }
        
        
        [HttpDelete("{sessionId}")]
        public async Task<IActionResult> stopSession(string sessionId)
        {
            sessionService.closeSession(sessionId);
            return NoContent();
        }
        
        
        
        [HttpPost]
        [Route("reply-session")]
        public async Task<IActionResult> replySession([FromBody] SessionLoginDto loginDto)
        {
            //check session
            
            //reply
           screenEventsReplyService.startSessionReply(loginDto.sessionId);
            return Ok("session");
        }
        
    }
}