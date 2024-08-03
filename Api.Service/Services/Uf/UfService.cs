
using Api.Domain.Dtos.Uf;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Uf.Services;
using AutoMapper;

namespace Api.Service.Services.Uf;

public class UfService(IRepository<UfEntity> _repository, IMapper _mapper): IUfService
{
    public async Task<UfDto> Get(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<UfDto>(entity);
    }

    public async Task<IEnumerable<UfDto>> GetAll()
    {
        var listEntity = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<UfDto>>(listEntity);
    }
}

