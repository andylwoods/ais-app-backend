using Microsoft.AspNetCore.Mvc;
using myapp.Models;
using maS = myapp.Services;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TextAnalysisController : ControllerBase
    {
        private readonly maS.ILogger _logger;
        private readonly maS.ITextAnalyzer _textAnalysisService;
        private readonly maS.ISerializerService _serializerService;

        public TextAnalysisController(maS.ILogger logger, maS.ITextAnalyzer textAnalysisService, maS.ISerializerService serializerService)
        {
            _logger = logger;
            _textAnalysisService = textAnalysisService;
            _serializerService = serializerService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json", "application/xml")]
        public async Task<IActionResult> AnalyzeText()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var requestBody = await reader.ReadToEndAsync();
                _logger.Log($"Raw request body: {requestBody}");
            }
            
            // For now, we will return a simple message since we can't process the request yet.
            return Ok("Request body logged.");
        }
    }
}
