using Xunit;
using Moq;
using myapp.Controllers;
using myapp.Services;
using myapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace myapp.Tests
{
    public class TextAnalysisControllerTests
    {
        [Fact]
        public void AnalyzeText_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var mockTextAnalyzer = new Mock<ITextAnalyzer>();
            var mockSerializer = new Mock<ISerializerService>();
            var controller = new TextAnalysisController(mockLogger.Object, mockTextAnalyzer.Object, mockSerializer.Object);
            var request = new TextAnalysisRequest { Text = "" }; // Invalid request

            // Act
            var result = controller.AnalyzeText(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AnalyzeText_NullRequest_ReturnsBadRequest()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var mockTextAnalyzer = new Mock<ITextAnalyzer>();
            var mockSerializer = new Mock<ISerializerService>();
            var controller = new TextAnalysisController(mockLogger.Object, mockTextAnalyzer.Object, mockSerializer.Object);

            // Act
            var result = controller.AnalyzeText(new TextAnalysisRequest());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AnalyzeText_ValidRequest_ReturnsOk()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var mockTextAnalyzer = new Mock<ITextAnalyzer>();
            mockTextAnalyzer.Setup(s => s.Analyze(It.IsAny<string>())).Returns(new AnalysisResult());
            var mockSerializer = new Mock<ISerializerService>();
            var controller = new TextAnalysisController(mockLogger.Object, mockTextAnalyzer.Object, mockSerializer.Object);
            var request = new TextAnalysisRequest { Text = "Valid text" };

            // Act
            var result = controller.AnalyzeText(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void AnalyzeText_ValidRequest_ReturnsXml()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var mockTextAnalyzer = new Mock<ITextAnalyzer>();
            mockTextAnalyzer.Setup(s => s.Analyze(It.IsAny<string>())).Returns(new AnalysisResult());
            var mockSerializer = new Mock<ISerializerService>();
            mockSerializer.Setup(s => s.SerializeToXml(It.IsAny<AnalysisResult>())).Returns("<xml></xml>");
            var controller = new TextAnalysisController(mockLogger.Object, mockTextAnalyzer.Object, mockSerializer.Object);
            var request = new TextAnalysisRequest { Text = "Valid text", OutputFormat = "xml" };

            // Act
            var result = controller.AnalyzeText(request) as ContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("application/xml", result.ContentType);
        }

        [Fact]
        public void AnalyzeText_ValidRequest_CallsTextAnalyzer()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var mockTextAnalyzer = new Mock<ITextAnalyzer>();
            mockTextAnalyzer.Setup(s => s.Analyze(It.IsAny<string>())).Returns(new AnalysisResult());
            var mockSerializer = new Mock<ISerializerService>();
            var controller = new TextAnalysisController(mockLogger.Object, mockTextAnalyzer.Object, mockSerializer.Object);
            var request = new TextAnalysisRequest { Text = "Valid text" };

            // Act
            controller.AnalyzeText(request);

            // Assert
            mockTextAnalyzer.Verify(s => s.Analyze("Valid text"), Times.Once);
        }
    }
}
