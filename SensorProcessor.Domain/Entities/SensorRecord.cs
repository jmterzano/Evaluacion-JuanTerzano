namespace SensorProcessor.Domain.Entities;

public sealed record SensorRecord(
    int Index,
    Guid Id,
    bool IsActive,
    string Zone,
    decimal Value
);
