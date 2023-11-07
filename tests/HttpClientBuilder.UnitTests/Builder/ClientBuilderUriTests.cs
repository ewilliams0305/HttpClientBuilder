using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using HttpClientBuilder.Model;

namespace HttpClientBuilder.UnitTests.Builder
{
    
    public class ClientBuilderUriTests
    {

        [Theory]
        [InlineData("127.0.0.1")]
        [InlineData("10.2.5.10")]
        [InlineData("172.26.6.6")]
        [InlineData("192.168.1.12")]
        [InlineData("fqdns.domain.com")]
        public void CreateClient_WithoutOptions_CreatesDefaultHttpsClient(string host)
        {
            // Arrange
            var client = ClientBuilder
                .CreateBuilder()
                .ConfigureHost(host)
                .CreateClient();

            // Act


            // Assert
            client.BaseAddress.AbsoluteUri.Should().Be($"https://{host}/");
        }
        
        [Theory]
        [InlineData("127.0.0.1")]
        [InlineData("10.2.5.10")]
        [InlineData("172.26.6.6")]
        [InlineData("192.168.1.12")]
        [InlineData("fqdns.domain.com")]
        public void CreateClient_WithoutOptions_CreatesSpecifiedHttpsClient(string host)
        {
            // Arrange
            var client = ClientBuilder
                .CreateBuilder()
                .ConfigureHost(host, SchemeType.Https)
                .CreateClient();

            // Act

            // Assert
            client.BaseAddress.AbsoluteUri.Should().Be($"https://{host}/");
        }

        [Theory]
        [InlineData("127.0.0.1")]
        [InlineData("10.2.5.10")]
        [InlineData("172.26.6.6")]
        [InlineData("192.168.1.12")]
        [InlineData("fqdns.domain.com")]
        public void CreateClient_WithoutOptions_CreatesSpecifiedHttpClient(string host)
        {
            // Arrange
            var client = ClientBuilder
                .CreateBuilder()
                .ConfigureHost(host, SchemeType.Http)
                .CreateClient();

            // Act

            // Assert
            client.BaseAddress.AbsoluteUri.Should().Be($"http://{host}/");
        }

        [Theory]
        [InlineData("127.0.0.1", 555)]
        [InlineData("10.2.5.10", 8082)]
        [InlineData("172.26.6.6", 50032)]
        [InlineData("192.168.1.12", 4756)]
        [InlineData("fqdns.domain.com", 15672)]
        public void CreateClient_WithPort_CreatesCustomHttpsClient(string host, int port)
        {
            // Arrange
            var client = ClientBuilder
                .CreateBuilder()
                .ConfigureHost(host, port: port)
                .CreateClient();

            // Act


            // Assert
            client.BaseAddress.AbsoluteUri.Should().Be($"https://{host}:{port}/");
        }

        [Theory]
        [InlineData("127.0.0.1", 555)]
        [InlineData("10.2.5.10", 8082)]
        [InlineData("172.26.6.6", 50032)]
        [InlineData("192.168.1.12", 4756)]
        [InlineData("fqdns.domain.com", 15672)]
        public void CreateClient_WithPort_CreatesSpecifiedHttpsClient(string host, int port)
        {
            // Arrange
            var client = ClientBuilder
                .CreateBuilder()
                .ConfigureHost(host, scheme:SchemeType.Https, port: port)
                .CreateClient();

            // Act

            // Assert
            client.BaseAddress.AbsoluteUri.Should().Be($"https://{host}:{port}/");
        }

        [Theory]
        [InlineData("127.0.0.1", 555)]
        [InlineData("10.2.5.10", 8082)]
        [InlineData("172.26.6.6", 50032)]
        [InlineData("192.168.1.12", 4756)]
        [InlineData("fqdns.domain.com", 15672)]
        public void CreateClient_WithPort_CreatesSpecifiedHttpClient(string host, int port)
        {
            // Arrange
            var client = ClientBuilder
                .CreateBuilder()
                .ConfigureHost(host, SchemeType.Http, port)
                .CreateClient();

            // Act

            // Assert
            client.BaseAddress.AbsoluteUri.Should().Be($"http://{host}:{port}/");
        }


        [Theory]
        [InlineData("127.0.0.1", 555, "api")]
        [InlineData("10.2.5.10", 8082, "api/v1")]
        [InlineData("172.26.6.6", 9253, "v1")]
        [InlineData("192.168.1.12", 5236, "cws")]
        [InlineData("fqdns.domain.com", 4568, "path")]
        public void CreateClient_WithPathAndPort_CreatesCustomHttpsClient(string host, int port, string path)
        {
            // Arrange
            var client = ClientBuilder
                .CreateBuilder()
                .ConfigureHost(host, port: port)
                .WithBaseRoute(path)
                .CreateClient();

            // Act


            // Assert
            client.BaseAddress.AbsoluteUri.Should().Be($"https://{host}:{port}/{path}");
        }

        [Theory]
        [InlineData("127.0.0.1", 555, "api")]
        [InlineData("10.2.5.10", 8082, "api/v1")]
        [InlineData("172.26.6.6", 9253, "v1")]
        [InlineData("192.168.1.12", 5236, "cws")]
        [InlineData("fqdns.domain.com", 4568, "path")]
        public void CreateClient_WithPathAndPort_CreatesSpecifiedHttpsClient(string host, int port, string path)
        {
            // Arrange
            var client = ClientBuilder
                .CreateBuilder()
                .ConfigureHost(host, scheme:SchemeType.Https, port: port)
                .WithBaseRoute(path)
                .CreateClient();

            // Act

            // Assert
            client.BaseAddress.AbsoluteUri.Should().Be($"https://{host}:{port}/{path}");
        }

        [Theory]
        [InlineData("127.0.0.1", 555, "api")]
        [InlineData("10.2.5.10", 8082, "api/v1")]
        [InlineData("172.26.6.6", 9253, "v1")]
        [InlineData("192.168.1.12", 5236, "cws")]
        [InlineData("fqdns.domain.com", 4568, "path")]
        public void CreateClient_WithPathAndPort_CreatesSpecifiedHttpClient(string host, int port, string path)
        {
            // Arrange
            var client = ClientBuilder
                .CreateBuilder()
                .ConfigureHost(host, SchemeType.Http, port)
                .WithBaseRoute(path)
                .CreateClient();

            // Act

            // Assert
            client.BaseAddress.AbsoluteUri.Should().Be($"http://{host}:{port}/{path}");
        }
    }
}
