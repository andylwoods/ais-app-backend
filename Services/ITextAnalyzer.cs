using myapp.Models;

namespace myapp.Services
{
    public interface ITextAnalyzer
    {
        AnalysisResult Analyze(string? text);
    }
}
