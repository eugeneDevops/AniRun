using AniRun.Application.Models.FormModels;
using AniRun.Application.Models.ViewModels;

namespace AniRun.Application.Services;

public interface ITitleService
{
    Task<List<ViewTitle>> GetTitles(CancellationToken cancellationToken = default);
    Task<ViewTitle> GetTitle(Guid id, CancellationToken cancellationToken = default);
    Task<ViewTitle> CreateTitle(FormTitle formTitle, CancellationToken cancellationToken = default);
    Task<ViewTitle> UpdateTitle(Guid id, FormTitle formTitle, CancellationToken cancellationToken = default);
    Task<ViewTitle> DeleteTitle(Guid id, CancellationToken cancellationToken = default);
    Task<ViewEpisode> AddEpisode(Guid id, FormEpisode formEpisode, CancellationToken cancellationToken = default);
    Task<ViewEpisode> DeleteEpisode(Guid id, CancellationToken cancellationToken = default);

}