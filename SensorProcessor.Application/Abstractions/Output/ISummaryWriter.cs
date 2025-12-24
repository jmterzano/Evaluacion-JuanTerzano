using SensorProcessor.Domain.Models;

namespace SensorProcessor.Application.Abstractions.Output;

public interface ISummaryWriter
{
    Task WriteAsync(string summaryPath, Summary summary, CancellationToken ct);
}

