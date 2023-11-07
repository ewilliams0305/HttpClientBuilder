namespace ClientBuilder.Model
{
    public readonly ref struct SchemeValue
    {
        
        private string _httpScheme;
        private HttpScheme _sheme;

        private SchemeValue(HttpScheme scheme)
        {
            _scheme = scheme;
            _httpScheme = scheme.ToSchemeString();
        }

        public static SchemeValue CreateScheme(HttpScheme scheme) => new(scheme);

        public static HttpScheme Http => new(HttpScheme.Http);
        public static HttpScheme Https => new(HttpScheme.Https);

        public static implicit operator string(SchemeValue scheme) => scheme._httpScheme;
        public static implicit operator HttpScheme(SchemeValue scheme => scheme._scheme;

        public static implicit operator SchemeValue(HttpScheme scheme) => 
            scheme == global::ClientBuilder.Model.HttpScheme.Https
            ? Https()
            : Http();
    }
}