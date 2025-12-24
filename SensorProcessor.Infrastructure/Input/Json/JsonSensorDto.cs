namespace SensorProcessor.Infrastructure.Input.Json;

internal sealed class JsonSensorDto
{
    public int index { get; set; }
    public string id { get; set; } = "";
    public bool isActive { get; set; }
    public string zone { get; set; } = "";
    public string value { get; set; } = "0";
}
