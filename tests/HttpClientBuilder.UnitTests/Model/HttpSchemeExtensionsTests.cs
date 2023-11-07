using System.Net;
using FluentAssertions;
using HttpClientBuilder.Model;

namespace HttpClientBuilder.UnitTests.Model;

public class HttpSchemeExtensionsTests
{
    [Fact]
    public void GetDefaultPort_ReturnsHttpPort_WhenSchemeIsHttp()
    {
        // Arrange
        var scheme = HttpScheme.Http;

        // Act
        var port = scheme.GetDefaultPort();

        // Assert
        port.Should().Be(80);
    }
    
    [Fact]
    public void GetDefaultPort_ReturnsHttpsPort_WhenSchemeIsHttps()
    {
        // Arrange
        var scheme = HttpScheme.Https;

        // Act
        var port = scheme.GetDefaultPort();

        // Assert
        port.Should().Be(443);
    }
    
    [Fact]
    public void ToSchemeString_ReturnsHttp_WhenSchemeIsHttp()
    {
        // Arrange
        var scheme = SchemeType.Http;

        // Act
        var value = scheme.ToSchemeString();

        // Assert
        value.Should().Be("http");
    }
    

    [Fact]
    public void ToSchemeString_ReturnsHttps_WhenSchemeIsHttps()
    {
        // Arrange
        var scheme = SchemeType.Https;

        // Act
        var value = scheme.ToSchemeString();

        // Assert
        value.Should().Be("https");
    }

    [Theory]
    [InlineData("127.0.0.1")]
    [InlineData("10.2.5.10")]
    [InlineData("172.26.6.6")]
    [InlineData("192.168.1.12")]
    [InlineData("fqdns.domain.com")]
    public void ToUriBuilder_CreatesMatchingUri_WhenSchemeIsHttp(string host)
    {
        // Arrange
        var scheme = HttpScheme.Http;

        // Act
        var uri = scheme.ToUriBuilder(host);

        // Assert
        uri.Uri.Should().Be($"http://{host}:80/");
    }

    [Theory]
    [InlineData("127.0.0.1")]
    [InlineData("10.2.5.10")]
    [InlineData("172.26.6.6")]
    [InlineData("192.168.1.12")]
    [InlineData("fqdns.domain.com")]
    public void ToUriBuilder_CreatesMatchingUri_WhenSchemeIsHttps(string host)
    {
        // Arrange
        var scheme = HttpScheme.Https;

        // Act
        var uri = scheme.ToUriBuilder(host);

        // Assert
        uri.Uri.Should().Be($"https://{host}:443/");
    }


    [Theory]
    [InlineData("127.0.0.1", 555)]
    [InlineData("10.2.5.10", 8082)]
    [InlineData("172.26.6.6", 50032)]
    [InlineData("192.168.1.12", 4756)]
    [InlineData("fqdns.domain.com", 15672)]
    public void ToUriBuilder_OverridesHttpPort_WhenPortIsProvided(string host, int port)
    {
        // Arrange
        var scheme = HttpScheme.Http;

        // Act
        var uri = scheme.ToUriBuilder(host, port: port);

        // Assert
        uri.Uri.Should().Be($"http://{host}:{port}/");
    }

    [Theory]
    [InlineData("127.0.0.1", 555)]
    [InlineData("10.2.5.10", 8082)]
    [InlineData("172.26.6.6", 50032)]
    [InlineData("192.168.1.12", 4756)]
    [InlineData("fqdns.domain.com", 15472)]
    public void ToUriBuilder_OverridesHttpsPort_WhenPortIsProvided(string host, int port)
    {
        // Arrange
        var scheme = HttpScheme.Https;

        // Act
        var uri = scheme.ToUriBuilder(host, port: port);

        // Assert
        uri.Uri.Should().Be($"https://{host}:{port}/");
    }


    [Theory]
    [InlineData("127.0.0.1", "api")]
    [InlineData("10.2.5.10", "api/v1")]
    [InlineData("172.26.6.6", "v1")]
    [InlineData("192.168.1.12", "cws")]
    [InlineData("fqdns.domain.com", "path")]
    public void ToUriBuilder_AppendsBaseHttpPath_WhenPathIsProvided(string host, string path)
    {
        // Arrange
        var scheme = HttpScheme.Http;

        // Act
        var uri = scheme.ToUriBuilder(host, path);

        // Assert
        uri.Uri.Should().Be($"http://{host}/{path}");
    }

    [Theory]
    [InlineData("127.0.0.1", "api")]
    [InlineData("10.2.5.10", "api/v1")]
    [InlineData("172.26.6.6", "v1")]
    [InlineData("192.168.1.12", "cws")]
    [InlineData("fqdns.domain.com", "path")]
    public void ToUriBuilder_AppendsBaseHttpsPath_WhenPathIsProvided(string host, string path)
    {
        // Arrange
        var scheme = HttpScheme.Https;

        // Act
        var uri = scheme.ToUriBuilder(host, path);

        // Assert
        uri.Uri.Should().Be($"https://{host}/{path}");
    }

    [Theory]
    [InlineData("127.0.0.1",555, "api")]
    [InlineData("10.2.5.10", 8082, "api/v1")]
    [InlineData("172.26.6.6", 9253, "v1")]
    [InlineData("192.168.1.12", 5236, "cws")]
    [InlineData("fqdns.domain.com", 4568, "path")]
    public void ToUriBuilder_AppendsBaseHttpPathWithPort_WhenPathIsProvided(string host, int port, string path)
    {
        // Arrange
        var scheme = HttpScheme.Http;

        // Act
        var uri = scheme.ToUriBuilder(host, path, port);

        // Assert
        uri.Uri.Should().Be($"http://{host}:{port}/{path}");
    }

    [Theory]
    [InlineData("127.0.0.1", 555, "api")]
    [InlineData("10.2.5.10", 8082, "api/v1")]
    [InlineData("172.26.6.6", 9253, "v1")]
    [InlineData("192.168.1.12", 5236, "cws")]
    [InlineData("fqdns.domain.com", 4568, "path")]
    public void ToUriBuilder_AppendsBaseHttpsPathWithPort_WhenPathIsProvided(string host, int port, string path)
    {
        // Arrange
        var scheme = HttpScheme.Https;

        // Act
        var uri = scheme.ToUriBuilder(host, path, port);

        // Assert
        uri.Uri.Should().Be($"https://{host}:{port}/{path}");
    }


}