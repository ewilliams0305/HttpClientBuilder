using FluentAssertions;
using HttpClientBuilder.Model;

namespace HttpClientBuilder.UnitTests.Model
{
    public class HttpSchemeTests
    {
        [Fact]
        public void HttpProperty_ReturnHttpString()
        {
            // Arrange
            var scheme = HttpScheme.Http;

            // Act
            string value = scheme;

            // Assert
            value.Should().Be("http");
        }

        [Fact]
        public void HttpProperty_ReturnHttpsString()
        {
            // Arrange
            var scheme = HttpScheme.Https;

            // Act
            string value = scheme;

            // Assert
            value.Should().Be("https");
        }

        [Theory]
        [InlineData(SchemeType.Http)]
        [InlineData(SchemeType.Https)]
        public void CreateScheme_Returns_CorrectScheme(SchemeType type)
        {
            // Arrange
            var scheme = HttpScheme.CreateScheme(type);

            // Act
            string value = scheme;

            // Assert
            value.Should().Be(type.ToSchemeString());
        }

        [Fact]
        public void IsHttp_ReturnsTrue_WhenSchemeIsHttp()
        {
            // Arrange
            var scheme = HttpScheme.Http;

            // Act

            // Assert
            scheme.IsHttp.Should().BeTrue();
        }

        [Fact]
        public void IsHttps_ReturnsTrue_WhenSchemeIsHttps()
        {
            // Arrange
            var scheme = HttpScheme.Https;

            // Act

            // Assert
            scheme.IsHttps.Should().BeTrue();
        }

        [Fact]
        public void IsHttp_ReturnsFalse_WhenSchemeIsHttps()
        {
            // Arrange
            var scheme = HttpScheme.Https;

            // Act

            // Assert
            scheme.IsHttp.Should().BeFalse();
        }

        [Fact]
        public void IsHttps_ReturnsFalse_WhenSchemeIsHttp()
        {
            // Arrange
            var scheme = HttpScheme.Http;

            // Act

            // Assert
            scheme.IsHttps.Should().BeFalse();
        }

        [Theory]
        [InlineData(SchemeType.Http)]
        [InlineData(SchemeType.Https)]
        public void Implicate_ReturnsCorrectScheme_FromType(SchemeType type)
        {
            // Arrange
            HttpScheme scheme = type;

            // Act
            string value = scheme;

            // Assert
            value.Should().Be(type.ToSchemeString());
        }
    }
}