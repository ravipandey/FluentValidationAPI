using FluentValidationAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace FluentValidationAPI
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<bool> NameExistsAsync(User user, string name, CancellationToken cancellationToken)
        {
            return await _appDbContext.Users.AnyAsync(p => p.Name == name & p.Email == user.Email, cancellationToken);
        }
    }
}
