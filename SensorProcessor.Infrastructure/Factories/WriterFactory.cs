using SensorProcessor.Application.Abstractions.Output;
using SensorProcessor.Infrastructure.Output.Csv;
using SensorProcessor.Infrastructure.Output.Xml;

namespace SensorProcessor.Infrastructure.Factories;

public sealed class WriterFactory : IWriterFactory
{
    public IReadOnlyList<ISensorWriter> CreateWriters(WriterSelection selection)
    {
        var writers = new List<ISensorWriter>();

        if (!string.IsNullOrWhiteSpace(selection.CsvPath))
            writers.Add(new CsvSensorWriter(selection.CsvPath!));

        if (!string.IsNullOrWhiteSpace(selection.XmlPath))
            writers.Add(new XmlSensorWriter(selection.XmlPath!));

        return writers;
    }
}
