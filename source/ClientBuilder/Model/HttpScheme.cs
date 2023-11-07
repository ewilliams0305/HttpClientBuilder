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
    }
}