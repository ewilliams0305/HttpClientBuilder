using FluentAssertions;
using HttpClientBuilder.Model;

namespace HttpClientBuilder.UnitTests.Model;

public class BuilderConfigurationTests
{
    [Fact]
    public void NewBuilderConfiguration_CreatesHeaderDictionary()
    {
        // Arrange
        var config = new BuilderConfiguration();

        // Act

        // Assert
        config.Headers.Should().NotBeNull();
        config.Headers.Should().BeEmpty();
    }
}