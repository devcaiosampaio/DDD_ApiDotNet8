﻿using Api.Domain.Entities;

namespace Api.Domain.Interfaces.User.Services;
public interface IUserService
{
    Task<UserEntity?> Get(Guid id);
    Task<IEnumerable<UserEntity>?> GetAll();
    Task<UserEntity> Post(UserEntity user);
    Task<UserEntity?> Put(UserEntity user);
    Task<bool> Delete(Guid id);
}
