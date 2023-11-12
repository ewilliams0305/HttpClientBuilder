namespace HttpClientBuilder.IntegrationTests.Utils;

public record ErrorResponse(DateTime Data, string Message, int Code, IEnumerable<string> Errors);