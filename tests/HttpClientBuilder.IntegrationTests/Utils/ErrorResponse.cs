namespace HttpClientBuilder.IntegrationTests.Utils;

internal record ErrorResponse(DateTime Data, string Message, int Code, IEnumerable<string> Errors);