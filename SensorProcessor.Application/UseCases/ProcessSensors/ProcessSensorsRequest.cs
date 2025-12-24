namespace SensorProcessor.Application.UseCases.ProcessSensors;

public sealed record ProcessSensorsRequest(
    string InputJsonPath,
    string SummaryJsonPath,
    string? CsvOutputPath,
    string? XmlOutputPath
);
