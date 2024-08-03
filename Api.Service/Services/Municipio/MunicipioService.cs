
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Municipio.Repositories;
using Api.Domain.Interfaces.Municipio.Services;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services.Municipio;

public class MunicipioService(
    IMunicipioRepository _repository,
    IMapper _mapper)
    : IMunicipioService
{
    public async Task<bool> Delete(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<MunicipioDto> Get(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<MunicipioDto>(entity);
    }

    public async Task<IEnumerable<MunicipioDtoCompleto>> GetAll()
    {
        var listMunicipio = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<MunicipioDtoCompleto>>(listMunicipio);
    }

    public async Task<MunicipioDtoCompleto> GetCompleteByIBGE(int codIBGE)
    {
        var entity = await _repository.GetCompleteByIBGE(codIBGE);
        return _mapper.Map<MunicipioDtoCompleto>(entity);
    }

    public async Task<MunicipioDtoCompleto> GetCompletebyid(Guid id)
    {
        var entity = await _repository.GetCompleteById(id);
        return _mapper.Map<MunicipioDtoCompleto>(entity);
    }

    public async Task<MunicipioDtoCreateResult> Post(MunicipioDtoCreateUpdate municipio)
    {
        var model = _mapper.Map<MunicipioModel>(municipio);
        var entity = _mapper.Map<MunicipioEntity>(model);
        var entityCreated = await _repository.InsertAsync(entity);
        return _mapper.Map<MunicipioDtoCreateResult>(entityCreated);
    }

    public async Task<MunicipioDtoUpdateResult> Put(MunicipioDtoCreateUpdate municipio, Guid id)
    {
        var model = _mapper.Map<MunicipioModel>(municipio);
        var entity = _mapper.Map<MunicipioEntity>(model);
        entity.Id = id;
        var entityCreated = await _repository.UpdateAsync(entity);
        return _mapper.Map<MunicipioDtoUpdateResult>(entityCreated);
    }
}

