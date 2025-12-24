namespace SensorProcessor.Domain.Models;

public sealed record Summary(
    Guid MaxSensorId,
    decimal MeanValue,
    IReadOnlyDictionary<string, decimal> MeanByZone,
    IReadOnlyDictionary<string, int> ActiveCountByZone
);
