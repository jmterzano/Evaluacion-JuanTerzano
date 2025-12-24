using Microsoft.Extensions.DependencyInjection;
using SensorProcessor.Application.UseCases.ProcessSensors;

namespace SensorProcessor.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<SensorProcessingPipeline>();
        services.AddTransient<ProcessSensorsHandler>();
        return services;
    }
}
