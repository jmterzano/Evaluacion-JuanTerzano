using SensorProcessor.Domain.Entities;

namespace SensorProcessor.Application.Abstractions.Output;

public interface ISensorWriter : IAsyncDisposable
{
    string Name { get; }
    Task WriteAsync(SensorRecord record, CancellationToken ct);
    Task CompleteAsync(CancellationToken ct);
}