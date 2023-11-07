using System;

namespace ClientBuilder.Model
{
    public static class SchemeExtensions
    {
        public static int GetDefaultPort(this HttpScheme scheme)=>
            scheme.IsHttps ? 443 : 80;
        
        public static string ToSchemeString(this SchemeType scheme) =>
            scheme == SchemeType.Https
                ? "https"
                : "http";

        public static UriBuilder ToUriBuilder(this HttpScheme scheme, string host, int? port) =>
            new()
            {
                Host = host,
                Scheme = scheme,
                Port = port?? scheme.GetDefaultPort()
            };
    }
}