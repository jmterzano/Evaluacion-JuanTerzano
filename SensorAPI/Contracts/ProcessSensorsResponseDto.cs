using SensorProcessor.Domain.Models;

namespace SensorProcessor.Api.Contracts;

public sealed record ProcessSensorsResponseDto(
    Summary Summary,
    IReadOnlyList<string> GeneratedFiles
);
