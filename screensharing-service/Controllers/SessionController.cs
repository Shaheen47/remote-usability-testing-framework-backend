using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using screensharing_service.Contracts.Services;
using screensharing_service.Entities;

namespace screensharing_service.Controllers
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
            Session session=await sessionService.createSession();
            return Created("session",session);
        }
    }
}