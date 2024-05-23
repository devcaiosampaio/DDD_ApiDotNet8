﻿namespace Api.Domain.Dtos.User;
public record struct UserDtoCreateResult
{
    public Guid id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreateAt { get; set; }
}