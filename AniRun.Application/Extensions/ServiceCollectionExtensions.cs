using Amazon.S3;
using AniRun.Application.Mappings;
using AniRun.Application.Models;
using AniRun.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AniRun.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(AnimeProfile).Assembly;
        services.AddScoped<ITitleService, TitleService>();
        services.AddScoped<IStudioService, StudioService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IMediaService, MediaService>();
        services.Configure<CredentialsStorage>(configuration.GetSection(CredentialsStorage.SectionConfiguration));
        
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(currentAssembly);
        });
    }
}
