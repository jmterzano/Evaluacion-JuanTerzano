namespace SensorProcessor.Api.Contracts;

public sealed record ProcessSensorsRequestDto(
    string? InputJsonPath,
    string? SummaryJsonPath,
    string? CsvOutputPath,
    string? XmlOutputPath
);
