using AniRun.Application.Models.FormModels;
using AniRun.Application.Models.ViewModels;

namespace AniRun.Application.Services;

public interface IStudioService
{
    Task<List<ViewStudio>> GetStudios();
    Task<ViewStudio> GetStudio(Guid id, CancellationToken cancellationToken = default);
    Task<ViewStudio> CreateStuido(FormStudio formStudio, CancellationToken cancellationToken = default);
    Task<ViewStudio> UpdateStudio(Guid id, FormStudio formStudio, CancellationToken cancellationToken = default);
    Task<ViewStudio> DeleteStudio(Guid id, CancellationToken cancellationToken = default);
}