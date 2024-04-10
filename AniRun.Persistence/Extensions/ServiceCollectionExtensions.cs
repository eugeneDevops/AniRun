using AniRun.DomainServices.Repositories;
using AniRun.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AniRun.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AniDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("BaseConnection")));
        services.AddScoped<IEpisodeRepository, EpisodeRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IMediaRepository, MediaRepository>();
        services.AddScoped<IStudioRepository, StudioRepository>();
        services.AddScoped<ITitleRepository, TitleRepository>();
    }
}
