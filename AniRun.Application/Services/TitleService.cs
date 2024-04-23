using AniRun.Application.Models.FormModels;
using AniRun.Application.Models.ViewModels;
using AniRun.Domain.Aggregates;
using AniRun.DomainServices.Repositories;
using AutoMapper;

namespace AniRun.Application.Services;

public class TitleService : ITitleService
{
    private readonly ITitleRepository _repository;
    private readonly IMapper _mapper;
    private readonly IMediaService _mediaService;
    private readonly IEpisodeRepository _episodeRepository;
    
    public TitleService(ITitleRepository repository, IMapper mapper, IMediaService mediaService, IEpisodeRepository episodeRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _mediaService = mediaService;
        _episodeRepository = episodeRepository;
    }
    
    public async Task<List<ViewTitle>> GetTitles(CancellationToken cancellationToken = default)
    {
        var result = new List<ViewTitle>();
        var titles = await _repository.FindAll(cancellationToken);
        result = _mapper.Map<List<ViewTitle>>(titles);
        return result;
    }

    public async Task<ViewTitle> GetTitle(Guid id, CancellationToken cancellationToken = default)
    {
        var result = new ViewTitle();
        var title = await _repository.FindById(id, cancellationToken);
        result = _mapper.Map<ViewTitle>(title);
        return result;
    }

    public async Task<ViewTitle> CreateTitle(FormTitle formTitle, CancellationToken cancellationToken = default)
    {
        var result = new ViewTitle();
        var title = _mapper.Map<Title>(formTitle);
        var picture = new ViewMedia();
        if (formTitle.Picture != null)
        {
            picture = await _mediaService.UploadMedia(formTitle.Picture, cancellationToken);
            title.PictureId = picture.Id;
        }

        title = await _repository.AddAsnyc(title, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        result = _mapper.Map<ViewTitle>(title);
        result.Picture = picture;
        return result;
    }

    public async Task<ViewTitle> UpdateTitle(Guid id, FormTitle formTitle, CancellationToken cancellationToken = default)
    {
        var result = new ViewTitle();
        var titleDb = await _repository.FindById(id, cancellationToken);
        if (titleDb == null)
            return result;
        var title = _mapper.Map<Title>(formTitle);
        var picture = new ViewMedia();
        if (formTitle.Picture != null)
        {
            picture = await _mediaService.UploadMedia(formTitle.Picture, cancellationToken);
            title.PictureId = picture.Id;
        }
        title = await _repository.UpdateAsnyc(id, title, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        result = _mapper.Map<ViewTitle>(title);
        result.Picture = picture;
        return result;
    }

    public async Task<ViewTitle> DeleteTitle(Guid id, CancellationToken cancellationToken = default)
    {
        var result = new ViewTitle();
        var title = await _repository.FindById(id, cancellationToken);
        if (title == null)
            return result;
        title = await _repository.DeleteAsync(id, cancellationToken);
        result = _mapper.Map<ViewTitle>(title);
        return result;
    }

    public async Task<ViewEpisode> AddEpisode(Guid id, FormEpisode formEpisode, CancellationToken cancellationToken = default)
    {
        var result = new ViewEpisode();
        var title = await _repository.FindById(id, cancellationToken);
        if (title == null)
            return result;
        var media = new ViewMedia();
        if (formEpisode.Video != null)
            media = await _mediaService.UploadMedia(formEpisode.Video, cancellationToken);
        var episode = _mapper.Map<Episode>(formEpisode);
        title.Episodes.Add(episode);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<ViewEpisode> DeleteEpisode(Guid id, CancellationToken cancellationToken = default)
    {
        var result = new ViewEpisode();
        var episode = await _episodeRepository.FindById(id, cancellationToken);
        if (episode == null)
            return result; 
        await _mediaService.DeleteMedia(episode.Video.Id, cancellationToken);
        episode = await _episodeRepository.DeleteAsync(id, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        result = _mapper.Map<ViewEpisode>(episode);
        return result;
        
    }
}