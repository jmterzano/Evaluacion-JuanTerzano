using System.Globalization;
using System.Text.Json;
using SensorProcessor.Application.Abstractions.Input;
using SensorProcessor.Domain.Entities;

namespace SensorProcessor.Infrastructure.Input.Json;

public sealed class JsonSensorSource : ISensorSource
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async IAsyncEnumerable<SensorRecord> ReadAllAsync(
        string inputPath,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken ct)
    {
        await using var fs = File.OpenRead(inputPath);

        await foreach (var item in JsonSerializer.DeserializeAsyncEnumerable<JsonSensorDto>(fs, Options, ct))
        {
            if (item is null) continue;

            yield return new SensorRecord(
                Index: item.index,
                Id: Guid.Parse(item.id),
                IsActive: item.isActive,
                Zone: item.zone,
                Value: decimal.Parse(item.value, CultureInfo.InvariantCulture)
            );
        }
    }
}
