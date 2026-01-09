using Microsoft.AspNetCore.Mvc;
using myapp.Models;
using maS = myapp.Services;
using System;

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
        public IActionResult AnalyzeText([FromBody] TextAnalysisRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Text))
            {
                return BadRequest("Text property cannot be null or empty in the JSON payload.");
            }

            var text = request.Text;
            _logger.Log($"Analyzing text: {text}");
            AnalysisResult result = _textAnalysisService.Analyze(text);
            _logger.Log($"Analysis result; SlowBikeCount: {result.SlowBikeCount}");

            if (result.ConsonantCounts != null)
            {
                foreach (KeyValuePair<char, int> kvp in result.ConsonantCounts)
                {
                    _logger.Log($"Analysis result; Consonant: {kvp.Key}, Count: {kvp.Value}");
                }
            }

            if (request.OutputFormat?.ToLower() == "xml")
            {
                return new ContentResult
                {
                    Content = _serializerService.SerializeToXml(result),
                    ContentType = "application/xml",
                    StatusCode = 200
                };
            }

            return Ok(result);
        }
    }
}
