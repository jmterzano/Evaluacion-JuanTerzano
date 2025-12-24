using System.Globalization;
using SensorProcessor.Application.Abstractions.Output;
using SensorProcessor.Domain.Entities;

namespace SensorProcessor.Infrastructure.Output.Csv;

public sealed class CsvSensorWriter : ISensorWriter
{
    private readonly StreamWriter _sw;
    public string Name => "CSV";

    public CsvSensorWriter(string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path) ?? ".");
        _sw = new StreamWriter(File.Open(path, FileMode.Create, FileAccess.Write, FileShare.Read));
        _sw.WriteLine("index,id,isActive,zone,value");
    }

    public Task WriteAsync(SensorRecord r, CancellationToken ct)
        => _sw.WriteLineAsync($"{r.Index},{r.Id},{r.IsActive},{r.Zone},{r.Value.ToString(CultureInfo.InvariantCulture)}");

    public async Task CompleteAsync(CancellationToken ct) => await _sw.FlushAsync();

    public ValueTask DisposeAsync() => _sw.DisposeAsync();
}
