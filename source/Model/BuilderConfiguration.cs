using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HttpClientBuilder.Model
{
    internal sealed class BuilderConfiguration
    {
        public string Host { get; set; } = string.Empty;

        public string? BasePath { get; set; }

        public int? Port { get; set; }

        public SchemeType Scheme { get; set; }

        public AuthenticationHeaderValue? Authentication { get; set; }

        public HttpClientHandler? Handler { get; set; }

        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }
}