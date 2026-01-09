using System.Text.Json.Serialization;

namespace myapp.Models
{
    public class TextAnalysisRequest
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }
}
