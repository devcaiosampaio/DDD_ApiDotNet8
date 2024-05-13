using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.User.Services;

namespace Api.Service.Services
{
    public class UserService(IRepository<UserEntity> _repository) : IUserService
    {
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UserEntity?> Get(Guid id)
        {
            return await _repository.GetByIdAsync(id);    
        }

        public async Task<IEnumerable<UserEntity>?> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<UserEntity> Post(UserEntity user)
        {
            return await _repository.InsertAsync(user);
        }

        public async Task<UserEntity?> Put(UserEntity user)
        {
            return await _repository.UpdateAsync(user);  
        }
    }
}
