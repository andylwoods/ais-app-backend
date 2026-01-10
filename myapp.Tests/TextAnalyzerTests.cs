using Xunit;
using Moq;
using myapp.Services;
using myapp.Models;

namespace myapp.Tests
{
    public class TextAnalyzerTests
    {
        [Fact]
        public void Analyze_NullInput_ReturnsEmptyResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var analyzer = new TextAnalyzer(mockLogger.Object);

            // Act
            var result = analyzer.Analyze(null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.SlowBikeCount);
            Assert.Empty(result.ConsonantCounts);
        }

        [Fact]
        public void Analyze_EmptyInput_ReturnsEmptyResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var analyzer = new TextAnalyzer(mockLogger.Object);

            // Act
            var result = analyzer.Analyze("");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.SlowBikeCount);
            Assert.Empty(result.ConsonantCounts);
        }

        [Fact]
        public void Analyze_SlowBikeCounts_AreCorrect()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var analyzer = new TextAnalyzer(mockLogger.Object);

            // Act
            var result = analyzer.Analyze("A slow bike is a slow bike.");

            // Assert
            Assert.Equal(2, result.SlowBikeCount);
        }

        [Fact]
        public void Analyze_ConsonantCounts_AreCorrect()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var analyzer = new TextAnalyzer(mockLogger.Object);

            // Act
            var result = analyzer.Analyze("hello world");

            // Assert
            Assert.Equal(3, result.ConsonantCounts['l']);
            Assert.Equal(1, result.ConsonantCounts['h']);
            Assert.Equal(1, result.ConsonantCounts['w']);
            Assert.Equal(1, result.ConsonantCounts['r']);
            Assert.Equal(1, result.ConsonantCounts['d']);
        }

        [Fact]
        public void Analyze_MixedCaseInput_IsHandledCorrectly()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var analyzer = new TextAnalyzer(mockLogger.Object);

            // Act
            var result = analyzer.Analyze("Slow Bike");

            // Assert
            Assert.Equal(1, result.SlowBikeCount);
            Assert.Equal(1, result.ConsonantCounts['s']);
            Assert.Equal(1, result.ConsonantCounts['l']);
            Assert.Equal(1, result.ConsonantCounts['w']);
            Assert.Equal(1, result.ConsonantCounts['b']);
            Assert.Equal(1, result.ConsonantCounts['k']);
        }
    }
}
