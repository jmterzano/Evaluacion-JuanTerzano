using Microsoft.Extensions.DependencyInjection;
using SensorProcessor.Application.Abstractions.Input;
using SensorProcessor.Application.Abstractions.Output;
using SensorProcessor.Infrastructure.Factories;
using SensorProcessor.Infrastructure.Input.Json;
using SensorProcessor.Infrastructure.Output.Summary;

namespace SensorProcessor.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ISensorSource, JsonSensorSource>();
        services.AddSingleton<IWriterFactory, WriterFactory>();
        services.AddSingleton<ISummaryWriter, SummaryJsonWriter>();
        return services;
    }
}
