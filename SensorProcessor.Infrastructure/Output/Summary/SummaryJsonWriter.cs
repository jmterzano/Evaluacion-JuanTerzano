using System.Text.Json;
using SensorProcessor.Application.Abstractions.Output;
using SensorProcessor.Domain.Models;

namespace SensorProcessor.Infrastructure.Output.Summary;

public sealed class SummaryJsonWriter : ISummaryWriter
{
    public async Task WriteAsync(string summaryPath, Domain.Models.Summary summary, CancellationToken ct)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(summaryPath) ?? ".");
        await using var fs = File.Create(summaryPath);

        await JsonSerializer.SerializeAsync(fs, summary, new JsonSerializerOptions
        {
            WriteIndented = true
        }, ct);
    }
}
