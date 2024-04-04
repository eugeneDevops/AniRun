using AniRun.Application.Models.FormModels;
using AniRun.Application.Models.ViewModels;
using AniRun.Domain.Models;
using AniRun.DomainServices.Repositories;
using AutoMapper;

namespace AniRun.Application.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;
    private readonly IMapper _mapper;
    
    public GenreService(IGenreRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<List<ViewGenre>> GetGenres()
    {
        var result = new List<ViewGenre>();
        var genres = await _repository.FindAll();
        result = _mapper.Map<List<Genre>, List<ViewGenre>>(genres);
        return result;
    }

    public async Task<ViewGenre> GetGenre(Guid id, CancellationToken cancellationToken = default)
    {
        var result = new ViewGenre();
        var genre = await _repository.FindById(id, cancellationToken);
        if (genre == null)
            return result;
        result = _mapper.Map<ViewGenre>(genre);
        return result;
    }

    public async Task<ViewGenre> CreateGenre(FormGenre formGenre, CancellationToken cancellationToken = default)
    {
        var result = new ViewGenre();
        if (formGenre == null)
            return result;
        var genre = _mapper.Map<Genre>(formGenre);
        genre = await _repository.AddAsnyc(genre, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        result = _mapper.Map<ViewGenre>(genre);
        return result;
    }

    public async Task<ViewGenre> UpdateGenre(Guid id, FormGenre formGenre, CancellationToken cancellationToken = default)
    {
        var result = new ViewGenre();
        if (formGenre == null)
            return result;
        var genreDb = await _repository.FindById(id, cancellationToken);
        if (genreDb == null)
            return result;
        var genre = _mapper.Map<Genre>(formGenre);
        genre = await  _repository.UpdateAsnyc(id, genre, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        result = _mapper.Map<ViewGenre>(genre);
        return result;
    }

    public async Task<ViewGenre> DeleteGenre(Guid id, CancellationToken cancellationToken = default)
    {
        var result = new ViewGenre();
        var genre = await _repository.FindById(id, cancellationToken);
        if (genre == null)
            return result;
        genre = await _repository.DeleteAsync(id, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        result = _mapper.Map<ViewGenre>(genre);
        return result;
    }
}