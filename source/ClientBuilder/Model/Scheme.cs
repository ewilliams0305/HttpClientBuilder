namespace ClientBuilder.Model
{
    public readonly ref struct SchemeValue
    {
        public static HttpScheme Http => new("http");
        public static HttpScheme Https => new("https");

        private string _httpScheme { get; }

        private SchemeValue(string scheme)
        {
            _httpScheme = scheme;
        }

        public static implicit operator string(SchemeValue scheme) => scheme.HttpScheme;

        public static implicit operator SchemeValue(HttpScheme scheme) => 
            scheme == global::ClientBuilder.Model.HttpScheme.Https
            ? Https()
            : Http();
    }
}