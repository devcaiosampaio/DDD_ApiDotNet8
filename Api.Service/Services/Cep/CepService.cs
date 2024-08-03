
using Api.Domain.Dtos.Cep;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Cep.Repositories;
using Api.Domain.Interfaces.Cep.Services;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services.Cep;

public class CepService(ICepRepository _repository, IMapper _mapper) : ICepService
{
    public async Task<bool> Delete(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<CepDto> Get(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<CepDto>(entity);
    }

    public async Task<CepDto> Get(string cep)
    {
        var entity = await _repository.GetByCep(cep);
        return _mapper.Map<CepDto>(entity);
    }

    public async Task<CepDtoCreateResult> Post(CepDtoCreateUpdate cep)
    {
        var model = _mapper.Map<CepModel>(cep);
        var entity = _mapper.Map<CepEntity>(model);
        var entityCreated = await _repository.InsertAsync(entity);
        return _mapper.Map<CepDtoCreateResult>(entityCreated);
    }

    public async Task<CepDtoUpdateResult> Put(CepDtoCreateUpdate cep, Guid id)
    {
        var model = _mapper.Map<CepModel>(cep);
        var entity = _mapper.Map<CepEntity>(model);
        entity.Id = id;
        var entityCreated = await _repository.UpdateAsync(entity);
        return _mapper.Map<CepDtoUpdateResult>(entityCreated);
    }
}

