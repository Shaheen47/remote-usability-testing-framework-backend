using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using screensharing_service.Contracts.Services;

namespace screensharing_service.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class SessionController: ControllerBase
    {
        private ISessionService sessionService;
        
        [HttpPost]
        [Route("create-session")]
        public async Task<IActionResult> createSession()
        {
            string session=await sessionService.generateScreenSharingHub();
            return Created("sessionName",session);
        }
    }
}