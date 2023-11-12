using System.Threading.Tasks;

namespace HttpClientBuilder;

/// <summary>
/// Dispatches the Http Request Pipeline
/// </summary>
public interface IRequest
{
    /// <summary>
    /// Starts the HTTP Request
    /// </summary>
    /// <returns>Asynchronous Task.</returns>
    Task<IResponse> Request();
}