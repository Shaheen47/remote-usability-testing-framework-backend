using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using videoconferencing_service.Dtos;
using videoconferencing_service.Services;

namespace videoconferencing_service.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class RecordingController : ControllerBase
    {
        private IRecordingService recordingService;

        public RecordingController(IRecordingService recordingService)
        {
            this.recordingService = recordingService;
        }

        [HttpPost]
        [Route("start-record")]
        public async Task<IActionResult> startRecording([FromBody] SessionDto sessionDto)
        {
            await recordingService.startRecording(sessionDto.sessionName);
            return NoContent();
        }
        
        [HttpPost]
        [Route("stop-record")]
        public async Task<IActionResult> stopRecording([FromBody] SessionDto sessionDto)
        {
            string url= await recordingService.stopRecording(sessionDto.sessionName);
            RecordingDto recordingDto = new RecordingDto();
            recordingDto.url = url;
            return Created("url", recordingDto);
        }
        
        /*[HttpDelete]
        [Route("delete-record")]
        public string deleteRecording([FromBody] SessionDto sessionDto)
        {
            
        }
        [HttpGet]
        [Route("get-record")]
        public string getRecording([FromBody] SessionDto sessionDto)
        {
            
        }*/
        /*[HttpGet]
        public string listRecordings()
        {
            
        }*/

    }
}