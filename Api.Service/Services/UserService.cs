using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.User.Services;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService(IRepository<UserEntity> _repository, IMapper _mapper) : IUserService
    {
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UserDto?> Get(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var model = _mapper.Map<UserModel>(entity);
            return _mapper.Map<UserDto>(model);
        }

        public async Task<IEnumerable<UserDto>?> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            var models = _mapper.Map<IEnumerable<UserModel>>(entities);
            return _mapper.Map<IEnumerable<UserDto>>(models);
        }

        public async Task<UserDtoCreateResult> Post(UserDtoCreateUpdate user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var entityCreated = await _repository.InsertAsync(entity);
            return _mapper.Map<UserDtoCreateResult>(entityCreated);
        }

        public async Task<UserDtoUpdateResult?> Put(UserDtoCreateUpdate user, Guid id)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            entity.Id = id;
            var entityUpdated = await _repository.UpdateAsync(entity);
            return _mapper.Map<UserDtoUpdateResult>(entityUpdated);
        }
    }
}
