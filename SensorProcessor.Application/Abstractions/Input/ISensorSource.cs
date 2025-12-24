using SensorProcessor.Domain.Entities;

namespace SensorProcessor.Application.Abstractions.Input;

public interface ISensorSource
{
    IAsyncEnumerable<SensorRecord> ReadAllAsync(string inputPath, CancellationToken ct);
}
