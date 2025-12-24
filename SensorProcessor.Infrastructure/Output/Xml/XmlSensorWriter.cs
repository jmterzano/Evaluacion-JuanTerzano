using System.Globalization;
using System.Xml;
using SensorProcessor.Application.Abstractions.Output;
using SensorProcessor.Domain.Entities;

namespace SensorProcessor.Infrastructure.Output.Xml;

public sealed class XmlSensorWriter : ISensorWriter
{
    private readonly XmlWriter _xw;
    public string Name => "XML";

    public XmlSensorWriter(string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path) ?? ".");
        var settings = new XmlWriterSettings { Async = true, Indent = true };
        _xw = XmlWriter.Create(path, settings);

        _xw.WriteStartDocument();
        _xw.WriteStartElement("sensors");
    }

    public async Task WriteAsync(SensorRecord r, CancellationToken ct)
    {
        await _xw.WriteStartElementAsync(null, "sensor", null);
        await _xw.WriteElementStringAsync(null, "index", null, r.Index.ToString(CultureInfo.InvariantCulture));
        await _xw.WriteElementStringAsync(null, "id", null, r.Id.ToString());
        await _xw.WriteElementStringAsync(null, "isActive", null, r.IsActive.ToString());
        await _xw.WriteElementStringAsync(null, "zone", null, r.Zone);
        await _xw.WriteElementStringAsync(null, "value", null, r.Value.ToString(CultureInfo.InvariantCulture));
        await _xw.WriteEndElementAsync(); 
    }

    public async Task CompleteAsync(CancellationToken ct)
    {
        await _xw.WriteEndElementAsync(); 
        await _xw.WriteEndDocumentAsync();
        await _xw.FlushAsync();
    }

    public ValueTask DisposeAsync()
    {
        _xw.Dispose();
        return ValueTask.CompletedTask;
    }
}
