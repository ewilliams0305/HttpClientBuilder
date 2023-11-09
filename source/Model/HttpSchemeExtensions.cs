using System;

namespace HttpClientBuilder.Model
{
    internal static class SchemeExtensions
    {
        public static int GetDefaultPort(this HttpScheme scheme)=>
            scheme.IsHttps ? 443 : 80;
        
        public static string ToSchemeString(this SchemeType scheme) =>
            scheme == SchemeType.Https
                ? "https"
                : "http";

        public static UriBuilder ToUriBuilder(this HttpScheme scheme, string host, string? basePath = null, int? port = null) =>
            basePath == null
            ? new UriBuilder()
                {
                    Host = host,
                    Scheme = scheme,
                    Port = port?? scheme.GetDefaultPort()
                }
            : new UriBuilder()
                {
                    Path = basePath,
                    Host = host,
                    Scheme = scheme,
                    Port = port ?? scheme.GetDefaultPort()
                };
    }
}