namespace ClientBuilder.Model
{
    public readonly ref struct SchemeValue
    {
        public string HttpScheme { get; }

        private SchemeValue(string scheme)
        {
            HttpScheme = scheme;
        }

        public static SchemeValue Http() => new("http");
        public static SchemeValue Https() => new("https");

        public static implicit operator string(SchemeValue scheme) => scheme.HttpScheme;

        public static implicit operator SchemeValue(HttpScheme scheme) => 
            scheme == global::ClientBuilder.Model.HttpScheme.Https
            ? Https()
            : Http();
    }
}