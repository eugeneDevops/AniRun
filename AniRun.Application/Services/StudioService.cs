using AniRun.Application.Models.FormModels;
using AniRun.Application.Models.ViewModels;
using AniRun.Domain.Aggregates;
using AniRun.DomainServices.Repositories;
using AutoMapper;

namespace AniRun.Application.Services;

public class StudioService : IStudioService
{
    private readonly IStudioRepository _repository;
    private readonly IMapper _mapper;
    
    public StudioService(IStudioRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<List<ViewStudio>> GetStudios()
    {
        var result = new List<ViewStudio>();
        var studios = await _repository.FindAll();
        result = _mapper.Map<List<ViewStudio>>(studios);
        return result;
    }

    public async Task<ViewStudio> GetStudio(Guid id, CancellationToken cancellationToken = default)
    {
        var result = new ViewStudio();
        var studio = await _repository.FindById(id, cancellationToken);
        if (studio == null)
            return result;
        result = _mapper.Map<ViewStudio>(studio);
        return result;
    }

    public async Task<ViewStudio> CreateStuido(FormStudio formStudio, CancellationToken cancellationToken = default)
    {
        var result = new ViewStudio();
        if (formStudio == null)
            return result;
        var studio = _mapper.Map<Studio>(formStudio);
        studio = await _repository.AddAsnyc(studio, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        result = _mapper.Map<ViewStudio>(studio);
        return result;
    }

    public async Task<ViewStudio> UpdateStudio(Guid id, FormStudio formStudio, CancellationToken cancellationToken = default)
    {
        var result = new ViewStudio();
        if (formStudio == null)
            return result;
        var studioDb = await _repository.FindById(id, cancellationToken);
        if (studioDb == null)
            return result;
        var studio = _mapper.Map<Studio>(formStudio);
        studio = await  _repository.UpdateAsnyc(id, studio, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        result = _mapper.Map<ViewStudio>(studio);
        return result;
    }

    public async Task<ViewStudio> DeleteStudio(Guid id, CancellationToken cancellationToken = default)
    {
        var result = new ViewStudio();
        var studio = await _repository.FindById(id, cancellationToken);
        if (studio == null)
            return result;
        studio = await _repository.DeleteAsync(id, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        result = _mapper.Map<ViewStudio>(studio);
        return result;
    }
}