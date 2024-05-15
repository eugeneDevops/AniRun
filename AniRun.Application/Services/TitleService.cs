using AniRun.Application.Models.FormModels;
using AniRun.Application.Models.ViewModels;
using AniRun.Domain.Aggregates;
using AniRun.Domain.Specifications;
using AniRun.DomainServices.Repositories;
using Ardalis.Specification;
using AutoMapper;

namespace AniRun.Application.Services;

public class TitleService : ITitleService
{
    private readonly ITitleRepository _repository;
    private readonly IMapper _mapper;
    private readonly IMediaService _mediaService;
    private readonly IMediaRepository _mediaRepository;
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IGenreRepository _genreRepository;
    
    public TitleService(ITitleRepository repository, IMapper mapper, IMediaService mediaService, IEpisodeRepository
        episodeRepository, IGenreRepository genreRepository, IMediaRepository mediaRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _mediaService = mediaService;
        _episodeRepository = episodeRepository;
        _genreRepository = genreRepository;
        _mediaRepository = mediaRepository;
    }
    
    public async Task<List<ViewTitle>> GetTitles(CancellationToken cancellationToken = default)
    {
        GetTitlesSpecifcation spec = new GetTitlesSpecifcation();
        var result = new List<ViewTitle>();
        var titles = await _repository.FindAll(spec, cancellationToken);
        result = _mapper.Map<List<ViewTitle>>(titles);
        foreach (var title in result)
            if (title.PictureId != null)
                title.Picture.Url = _mediaService.GetUrlMedia(title.PictureId.Value);
        return result;
    }

    public async Task<ViewTitle> GetTitle(Guid id, CancellationToken cancellationToken = default)
    {
        GetTitlesSpecifcation spec = new GetTitlesSpecifcation();
        var result = new ViewTitle();
        var title = await _repository.FindById(id, spec, cancellationToken);
        result = _mapper.Map<ViewTitle>(title);
        result.Picture.Url = _mediaService.GetUrlMedia(title.PictureId.Value);
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
            title.Picture = null;
        }

        var ids = title.Genres.Select(genre => genre.Id).ToList();
        title.Genres = await _genreRepository.FindByIds(ids);
        
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
        var picture = new ViewMedia();
        if (formTitle.Picture != null && formTitle.PictureId != titleDb.PictureId)
        {
            picture = await _mediaService.UploadMedia(formTitle.Picture, cancellationToken);
            formTitle.PictureId = picture.Id;
            formTitle.Picture = null;
        }
        _mapper.Map(formTitle, titleDb);
        await _repository.SaveChangesAsync(cancellationToken);
        result = _mapper.Map<ViewTitle>(titleDb);
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
        await _repository.SaveChangesAsync(cancellationToken);
        result = _mapper.Map<ViewTitle>(title);
        return result;
    }

    public async Task<ViewEpisode> AddEpisode(Guid id, FormEpisode formEpisode, CancellationToken cancellationToken = default)
    {
        var result = new ViewEpisode();
        var title = await _repository.FindById(id, cancellationToken);
        if (title == null)
            return result;
        var episode = _mapper.Map<Episode>(formEpisode);
        var video = await _mediaRepository.FindById(formEpisode.VideoId, cancellationToken);
        episode.VideoId = video.Id;
        episode.Video = null;
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