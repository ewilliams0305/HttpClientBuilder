using System.Data;

namespace HttpClientBuilder.Server.Models;

internal record ErrorResponse(DateTime Data, string Message, int Code, IEnumerable<string> Errors);