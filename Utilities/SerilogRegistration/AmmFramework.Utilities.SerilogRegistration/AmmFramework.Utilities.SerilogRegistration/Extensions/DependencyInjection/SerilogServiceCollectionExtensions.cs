using AmmFramework.Utilities.SerilogRegistration.Enrichers;
using AmmFramework.Utilities.SerilogRegistration.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Enrichers.Span;
using Serilog.Exceptions;
using LoggerEnrichmentConfigurationExtensions = Serilog.Enrichers.Span.LoggerEnrichmentConfigurationExtensions;

namespace AmmFramework.Utilities.SerilogRegistration.Extensions.DependencyInjection;

public static class SerilogServiceCollectionExtensions
{
    public static WebApplicationBuilder AddFrameworkSerilog(this WebApplicationBuilder builder, IConfiguration configuration, params Type[] enrichersType)
    {

        builder.Services.Configure<SerilogApplicationEnricherOptions>(configuration);
        return AddServices(builder, enrichersType);
    }

    public static WebApplicationBuilder AddFrameworkSerilog(this WebApplicationBuilder builder, IConfiguration configuration, string sectionName, params Type[] enrichersType)
    {
        return builder.AddFrameworkSerilog(configuration.GetSection(sectionName), enrichersType);
    }

    public static WebApplicationBuilder AddFrameworkSerilog(this WebApplicationBuilder builder, Action<SerilogApplicationEnricherOptions> setupAction, params Type[] enrichersType)
    {
        builder.Services.Configure(setupAction);
        return AddServices(builder, enrichersType);
    }

    private static WebApplicationBuilder AddServices(WebApplicationBuilder builder, params Type[] enrichersType)
    {

        List<ILogEventEnricher> logEventEnrichers = new();

        builder.Services.AddTransient<FrameworkUserInfoEnricher>();
        builder.Services.AddTransient<FrameworkApplicationEnricher>();
        foreach (var enricherType in enrichersType)
        {
            builder.Services.AddTransient(enricherType);
        }

        builder.Host.UseSerilog((ctx, services, lc) => {
            logEventEnrichers.Add(services.GetRequiredService<FrameworkUserInfoEnricher>());
            logEventEnrichers.Add(services.GetRequiredService<FrameworkApplicationEnricher>());
            foreach (var enricherType in enrichersType)
            {
                logEventEnrichers.Add(services.GetRequiredService(enricherType) as ILogEventEnricher);
            }

            lc
            //.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
            .Enrich.FromLogContext()
            .Enrich.With([.. logEventEnrichers])
            .Enrich.WithExceptionDetails()
            .Enrich.WithSpan()
            .ReadFrom.Configuration(ctx.Configuration);
        });
        return builder;
    }
}