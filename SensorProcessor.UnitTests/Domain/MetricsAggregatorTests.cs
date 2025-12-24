using FluentAssertions;
using SensorProcessor.Domain.Aggregates;
using SensorProcessor.Domain.Entities;
using Xunit;

namespace SensorProcessor.UnitTests.Domain;

public sealed class MetricsAggregatorTests
{
    [Fact]
    public void BuildSummary_ComputesExpectedMetrics()
    {
        MetricsAggregator a = new();

        SensorRecord r1 = new(0, Guid.NewGuid(), true, "Z01", 10m);
        SensorRecord r2 = new(1, Guid.NewGuid(), false, "Z01", 30m);
        SensorRecord r3 = new(2, Guid.NewGuid(), true, "Z02", 100m);

        a.Add(r1);
        a.Add(r2);
        a.Add(r3);

        var s = a.BuildSummary();

        s.MaxSensorId.Should().Be(r3.Id);
        s.MeanValue.Should().Be((10m + 30m + 100m) / 3m);

        s.MeanByZone["Z01"].Should().Be((10m + 30m) / 2m);
        s.MeanByZone["Z02"].Should().Be(100m);

        s.ActiveCountByZone["Z01"].Should().Be(1);
        s.ActiveCountByZone["Z02"].Should().Be(1);
    }
}
