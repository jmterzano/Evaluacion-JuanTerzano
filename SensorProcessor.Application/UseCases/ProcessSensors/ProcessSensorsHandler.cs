using SensorProcessor.Application.Abstractions.Output;
using SensorProcessor.Domain.Models;

namespace SensorProcessor.Application.UseCases.ProcessSensors;

public sealed class ProcessSensorsHandler
{
    private readonly IWriterFactory _writerFactory;
    private readonly ISummaryWriter _summaryWriter;
    private readonly SensorProcessingPipeline _pipeline;

    public ProcessSensorsHandler(
        IWriterFactory writerFactory,
        ISummaryWriter summaryWriter,
        SensorProcessingPipeline pipeline)
    {
        _writerFactory = writerFactory;
        _summaryWriter = summaryWriter;
        _pipeline = pipeline;
    }

    public async Task<ProcessSensorsResult> HandleAsync(ProcessSensorsRequest request, CancellationToken ct)
    {
        var writers = _writerFactory.CreateWriters(new WriterSelection(
            CsvPath: request.CsvOutputPath,
            XmlPath: request.XmlOutputPath));

        Summary summary = await _pipeline.RunAsync(request.InputJsonPath, writers, ct);

        await _summaryWriter.WriteAsync(request.SummaryJsonPath, summary, ct);

        var generated = new List<string> { request.SummaryJsonPath };
        if (!string.IsNullOrWhiteSpace(request.CsvOutputPath)) generated.Add(request.CsvOutputPath!);
        if (!string.IsNullOrWhiteSpace(request.XmlOutputPath)) generated.Add(request.XmlOutputPath!);

        return new ProcessSensorsResult(summary, generated);
    }
}
