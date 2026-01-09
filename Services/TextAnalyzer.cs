using myapp.Models;
using System.Text.RegularExpressions;

namespace myapp.Services
{
    public class TextAnalyzer : ITextAnalyzer
    {
        private readonly ILogger _logger;

        public TextAnalyzer(ILogger logger)
        {
            _logger = logger;
        }

        public AnalysisResult Analyze(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    return new AnalysisResult();
                }

                var result = new AnalysisResult() { ConsonantCounts = new Dictionary<char, int>() };

                // Count "slow bike"
                result.SlowBikeCount = Regex.Matches(text, "slow bike", RegexOptions.IgnoreCase).Count;

                // Remove vowels and count consonants
                var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
                var consonantCounts = new Dictionary<char, int>();
                var textWithoutVowels = new string(text.ToLower().Where(c => !vowels.Contains(c) && char.IsLetter(c)).ToArray());

                foreach (char c in textWithoutVowels)
                {
                    if (consonantCounts.ContainsKey(c))
                    {
                        consonantCounts[c]++;
                    }
                    else
                    {
                        consonantCounts[c] = 1;
                    }
                }

                result.ConsonantCounts = consonantCounts;

                return result;
            }
            catch (Exception ex)
            {
                _logger.Log($"An error occurred during text analysis: {ex.Message}");
                return new AnalysisResult();
            }
        }
    }
}
