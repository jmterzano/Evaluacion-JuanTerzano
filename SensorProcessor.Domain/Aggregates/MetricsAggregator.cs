using SensorProcessor.Domain.Entities;
using SensorProcessor.Domain.Models;

namespace SensorProcessor.Domain.Aggregates;

public sealed class MetricsAggregator
{
    private decimal _sum;
    private long _count;

    private decimal _maxValue = decimal.MinValue;
    private Guid _maxSensorId = Guid.Empty;

    private readonly Dictionary<string, (decimal sum, long count)> _zoneSumCount = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _activeByZone = new(StringComparer.OrdinalIgnoreCase);

    public void Add(SensorRecord r)
    {
        _sum += r.Value;
        _count++;

        if (r.Value > _maxValue)
        {
            _maxValue = r.Value;
            _maxSensorId = r.Id;
        }

        if (_zoneSumCount.TryGetValue(r.Zone, out var acc))
            _zoneSumCount[r.Zone] = (acc.sum + r.Value, acc.count + 1);
        else
            _zoneSumCount[r.Zone] = (r.Value, 1);

        if (!_activeByZone.ContainsKey(r.Zone))
            _activeByZone[r.Zone] = 0;

        if (r.IsActive)
            _activeByZone[r.Zone] += 1;
    }

    public Summary BuildSummary()
    {
        decimal mean = _count == 0 ? 0 : _sum / _count;

        var meanByZone = _zoneSumCount.ToDictionary(
            k => k.Key,
            v => v.Value.count == 0 ? 0 : v.Value.sum / v.Value.count,
            StringComparer.OrdinalIgnoreCase
        );

        return new Summary(
            MaxSensorId: _maxSensorId,
            MeanValue: mean,
            MeanByZone: meanByZone,
            ActiveCountByZone: new Dictionary<string, int>(_activeByZone, StringComparer.OrdinalIgnoreCase)
        );
    }
}
