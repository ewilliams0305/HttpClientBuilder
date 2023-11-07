namespace ClientBuilder.Model
{
    public enum HttpScheme
    {
        Http,
        Https
    }

    public static class HttpSchemeExtensions
    {
        public static Scheme ToScheme(this HttpScheme scheme)=>
            scheme == HttpScheme.Https
            ? Scheme.Https
            : Scheme.Http;

        public static int GetDefaultPort(this HttpScheme scheme)=>
            scheme == HttpScheme.Https
            ? 443
            : 80;
        
        public static string ToSchemeString(this HttpScheme scheme) =>
            scheme == HttpScheme.Https
            ? "https"
            : "http";

        public static UriBuilder ToUriBuilder(this SchemeValue scheme, string host, int? port) =>
            new UriBuilder
            {
                Host = host,
                Scheme = scheme,
                Port = port?? scheme.ToPort()
            };
    }

    
}