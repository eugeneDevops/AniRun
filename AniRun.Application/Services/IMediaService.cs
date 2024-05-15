using AniRun.Application.Models.FormModels;
using AniRun.Application.Models.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace AniRun.Application.Services;

public interface IMediaService
{
    Task CreateBucket(CancellationToken cancellationToken = default);
    Task<ViewMedia> UploadMedia(FormMedia file, CancellationToken cancellationToken = default);
    Task<ViewMedia> UploadMedia(Stream stream, IBrowserFile file, CancellationToken cancellationToken = default);
    Task<ViewMedia> GetMedia(Guid id, CancellationToken cancellationToken = default);
    Task<ViewMedia> DeleteMedia(Guid id, CancellationToken cancellationToken = default);
    string GetUrlMedia(Guid id);
}