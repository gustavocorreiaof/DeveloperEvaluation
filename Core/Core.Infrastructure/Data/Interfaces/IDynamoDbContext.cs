using Core.Domain.Entities;

namespace Core.Infrastructure.Data.Interfaces
{
    public interface IDynamoDbContext
    {
        Task<User> GetUserByNameAsync(string name);
    }
}
