using Microsoft.AspNetCore.Mvc;
using SensorProcessor.Api.Contracts;
using SensorProcessor.Application.UseCases.ProcessSensors;

namespace SensorProcessor.Api.Controllers;

[ApiController]
[Route("api/sensors")]
public sealed class SensorsProcessingController : ControllerBase
{
    private readonly ProcessSensorsHandler _handler;

    public SensorsProcessingController(ProcessSensorsHandler handler)
    {
        _handler = handler;
    }

    [HttpPost("process")]
    [ProducesResponseType(typeof(ProcessSensorsResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Process([FromBody] ProcessSensorsRequestDto dto, CancellationToken ct)
    {
        string defaultInputPath = Path.Combine(AppContext.BaseDirectory, "Data", "sensores.json");
        string inputPath = string.IsNullOrWhiteSpace(dto.InputJsonPath) ? defaultInputPath : dto.InputJsonPath;
        string outDir = Path.Combine(AppContext.BaseDirectory, "out");

        Directory.CreateDirectory(outDir);

        string summaryPath = string.IsNullOrWhiteSpace(dto.SummaryJsonPath)
            ? Path.Combine(outDir, "summary.json")
            : dto.SummaryJsonPath;

        if (!System.IO.File.Exists(inputPath))
            return BadRequest($"Input JSON not found at '{inputPath}'.");

        ProcessSensorsResult result = await _handler.HandleAsync(new ProcessSensorsRequest(
            InputJsonPath: inputPath,
            SummaryJsonPath: summaryPath,
            CsvOutputPath: dto.CsvOutputPath,
            XmlOutputPath: dto.XmlOutputPath
        ), ct);

        return Ok(new ProcessSensorsResponseDto(result.Summary, result.GeneratedFiles));
    }
}
