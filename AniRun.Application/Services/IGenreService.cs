using AniRun.Application.Models.FormModels;
using AniRun.Application.Models.ViewModels;

namespace AniRun.Application.Services;

public interface IGenreService
{
    Task<List<ViewGenre>> GetGenres();
    Task<ViewGenre> GetGenre(Guid id, CancellationToken cancellationToken = default);
    Task<ViewGenre> CreateGenre(FormGenre formGenre, CancellationToken cancellationToken = default);
    Task<ViewGenre> UpdateGenre(Guid id, FormGenre formGenre, CancellationToken cancellationToken = default);
    Task<ViewGenre> DeleteGenre(Guid id, CancellationToken cancellationToken = default);
}