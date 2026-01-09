using Microsoft.AspNetCore.Mvc;
using maS = myapp.Services;
using myapp.Models;

namespace myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TextAnalysisController : ControllerBase
    {
        private readonly maS.ILogger _logger;
        private readonly maS.ITextAnalyzer _textAnalyzer;

        public TextAnalysisController(maS.ILogger logger, maS.ITextAnalyzer textAnalyzer)
        {
            _logger = logger;
            _textAnalyzer = textAnalyzer;
        }

        [HttpPost]
        public IActionResult AnalyzeText([FromBody] string text)
        {
            _logger.Log($"Analyzing text: {text}");
            AnalysisResult result = _textAnalyzer.Analyze(text);
            _logger.Log($"Analysis result; SlowBikeCount: {result.SlowBikeCount}");
            if (result.ConsonantCounts != null)
            {
                foreach (KeyValuePair<char, int> kvp in result.ConsonantCounts)
                {
                    _logger.Log($"Analysis result; Consonant: {kvp.Key}, Count: {kvp.Value}");
                }
            }
            return Ok(result);
        }
    }
}
