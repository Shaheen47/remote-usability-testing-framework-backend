using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using videoconferencing_service.Dtos;
using videoconferencing_service.Services;

namespace videoconferencing_service.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class SessionController: ControllerBase
    {
        private ISessionService sessionService;
        /*private IRecordingService recordingService;*/


        public SessionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        [HttpPost]
        [Route("create-session")]
        public async Task<IActionResult> createSession()
        {
            string sessionName=await sessionService.createSession();
            SessionDto sessionDto = new SessionDto();
            sessionDto.sessionName = sessionName;
            return Created("sessionName",sessionDto);
        }
        
        [HttpDelete("{sessionName}")]
        [Route("close-session")]
        public async Task<IActionResult> closeSession(SessionDto sessionDto)
        {
            await sessionService.closeSession(sessionDto.sessionName);
            return NoContent();
        }
        
        
        [HttpPost]
        [Route("join-session-moderator")]
        public async Task<IActionResult> joinSessionAsModerator([FromBody] SessionDto sessionDto)
        {
            string token=await sessionService.joinSessionAsModerator(sessionDto.sessionName);
            TokenDto tokenDto = new TokenDto();
            tokenDto.token = token;
            return Created("token",tokenDto);
        }
        
        
        [HttpPost]  
        [Route("join-session-observer")]
        public async Task<IActionResult> joinSessionAsObserver([FromBody] SessionDto sessionDto)
        {
            string token=await sessionService.joinSessionAsObserver(sessionDto.sessionName);
            TokenDto tokenDto = new TokenDto();
            tokenDto.token = token;
            return Created("token",tokenDto);
        }
        
        [HttpPost]
        [Route("join-session-participant")]
        public async Task<IActionResult> joinSessionAsParticipant([FromBody] SessionDto sessionDto)
        {
            string token=await sessionService.joinSessionAsParticipant(sessionDto.sessionName);
            TokenDto tokenDto = new TokenDto();
            tokenDto.token = token;
            return Created("token",tokenDto);
        }
        

      
        /*[HttpPost]
        public string removeUser()
        {
            
        }*/
        
        
        
        /*[HttpPost]
        public string fetchInfo()
        {
            
        }
        [HttpGet]
        public string fetchAll()
        {
            
        }
        [HttpDelete]
        public string forceDisconnect()
        {
            
        }
        [HttpDelete]
        public string forceUnpublish()
        {
            
        }*/
       
    }
}