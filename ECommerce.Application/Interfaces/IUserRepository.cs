using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
    Task<bool> EmailExistsAsync(string email);
}