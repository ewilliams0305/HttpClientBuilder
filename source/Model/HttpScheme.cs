using System;

namespace ClientBuilder.Model
{
    /// <summary>
    /// The HttpScheme struct provide strongly typed data around the scheme used by an Http Client.
    /// The http scheme can only ever be Http or Https.
    /// The http scheme is used internally by the client builder when constructing URIs <seealso cref="IClientBuilder"/>
    /// </summary>
    public readonly ref struct HttpScheme
    {
        /// <summary>
        /// True when the scheme was created as Https
        /// </summary>
        public bool IsHttps => _scheme == SchemeType.Https;

        /// <summary>
        /// True when the scheme was created as Http
        /// </summary>
        public bool IsHttp => _scheme == SchemeType.Http;

        private readonly string _value;
        private readonly SchemeType _scheme;

        private HttpScheme(SchemeType scheme)
        {
            _scheme = scheme;
            _value = scheme.ToSchemeString();
        }

        /// <summary>
        /// Creates a new Http Scheme
        /// </summary>
        /// <param name="scheme">Type of scheme</param>
        /// <returns>new Http or Https scheme</returns>
        public static HttpScheme CreateScheme(SchemeType scheme) => new(scheme);

        /// <summary>
        /// Returns a new Http Scheme.
        /// </summary>
        public static HttpScheme Http => new(SchemeType.Http);

        /// <summary>
        /// Returns a new Https scheme.
        /// </summary>
        public static HttpScheme Https => new(SchemeType.Https);

        /// <summary>
        /// Converts a scheme to a string representing the scheme or
        /// either http or https.
        /// </summary>
        /// <param name="scheme">The instance of the http scheme</param>
        public static implicit operator string(HttpScheme scheme) => scheme._value;
        
        /// <summary>
        /// Creates a new http scheme from the type of scheme provided.
        /// <seealso cref="SchemeType"/>
        /// </summary>
        /// <param name="scheme">Type of HTTP(s) scheme</param>
        public static implicit operator HttpScheme(SchemeType scheme) => 
            scheme == SchemeType.Https
            ? Https
            : Http;
    }
}