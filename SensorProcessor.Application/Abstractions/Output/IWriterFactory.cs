namespace SensorProcessor.Application.Abstractions.Output;

public interface IWriterFactory
{
    IReadOnlyList<ISensorWriter> CreateWriters(WriterSelection selection);
}

public sealed record WriterSelection(
    string? CsvPath,
    string? XmlPath
);
