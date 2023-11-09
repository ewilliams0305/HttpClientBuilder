using System;
using System.Net.Http;

namespace HttpClientBuilder
{
    /// <summary>
    /// Advances the pipeline to the optional phase of the <seealso cref="ClientBuilder"/> build.
    /// The optional phase includes additional headers and http handlers.
    /// </summary>
    public interface IOptionsBuilder: IHandlerBuilder, IHttpHeaderBuilder, IClientBuilder
    {
        
    }

    

    
}