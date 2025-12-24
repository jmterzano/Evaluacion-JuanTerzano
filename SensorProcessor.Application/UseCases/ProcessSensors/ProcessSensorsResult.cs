using SensorProcessor.Domain.Models;

namespace SensorProcessor.Application.UseCases.ProcessSensors;

public sealed record ProcessSensorsResult(
    Summary Summary,
    IReadOnlyList<string> GeneratedFiles
);
