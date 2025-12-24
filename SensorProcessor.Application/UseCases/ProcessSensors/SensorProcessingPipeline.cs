using System.Threading.Channels;
using SensorProcessor.Application.Abstractions.Input;
using SensorProcessor.Application.Abstractions.Output;
using SensorProcessor.Domain.Aggregates;
using SensorProcessor.Domain.Entities;
using SensorProcessor.Domain.Models;

namespace SensorProcessor.Application.UseCases.ProcessSensors;

public sealed class SensorProcessingPipeline
{
    private readonly ISensorSource _source;

    public SensorProcessingPipeline(ISensorSource source)
    {
        _source = source;
    }

    public async Task<Summary> RunAsync(
        string inputPath,
        IReadOnlyList<ISensorWriter> writers,
        CancellationToken ct)
    {
        MetricsAggregator agg = new();


        var channels = writers
            .Select(_ => Channel.CreateBounded<SensorRecord>(
                new BoundedChannelOptions(capacity: 2_000)
                {
                    FullMode = BoundedChannelFullMode.Wait,
                    SingleReader = true,
                    SingleWriter = true
                }))
            .ToArray();

        var consumerTasks = writers
            .Select((w, i) => ConsumeAsync(w, channels[i].Reader, ct))
            .ToArray();

        await foreach (SensorRecord record in _source.ReadAllAsync(inputPath, ct))
        {
            agg.Add(record);

            for (int i = 0; i < channels.Length; i++)
            {
                await channels[i].Writer.WriteAsync(record, ct);
            }
        }

        foreach (Channel<SensorRecord> ch in channels)
            ch.Writer.TryComplete();

        await Task.WhenAll(consumerTasks);

        return agg.BuildSummary();
    }

    private static async Task ConsumeAsync(ISensorWriter writer, ChannelReader<SensorRecord> reader, CancellationToken ct)
    {
        try
        {
            await foreach (SensorRecord r in reader.ReadAllAsync(ct))
                await writer.WriteAsync(r, ct);

            await writer.CompleteAsync(ct);
        }
        catch
        {
            try { await writer.CompleteAsync(ct); } catch { /* ignore */ }
        }
        finally
        {
            await writer.DisposeAsync();
        }
    }
}
