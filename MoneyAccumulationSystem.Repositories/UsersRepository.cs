using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyAccumulationSystem.CrossCutting.Auth;
using MoneyAccumulationSystem.CrossCutting.Helpers;
using MoneyAccumulationSystem.Database.EF;
using MoneyAccumulationSystem.Database.EF.Models;
using MoneyAccumulationSystem.Repositories.Interfaces;

namespace MoneyAccumulationSystem.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly MasDbContext dbContext;
    private readonly AuthUser authUser;

    public UsersRepository(MasDbContext dbContext, AuthUser authUser)
    {
        this.dbContext = dbContext;
        this.authUser = authUser;
    }
    
    public async Task<IList<User>> GetListAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Users.ToListAsync(cancellationToken);
    }

    public Task<User> GetAsync(
        int? userId = null,
        string login = null,
        string password = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<User> userQuery = dbContext.Users;

        if (userId != null)
        {
            userQuery = userQuery.Where(x => x.Id == userId);
        }
        else
        {
            if (login != null && password != null)
            {
                var hashedPassword = PasswordHelper.GetHashedPassword(password);
                userQuery = userQuery.Where(x => x.Login == login && x.HashedPassword == hashedPassword);
            }
        }

        return userQuery.FirstOrDefaultAsync(cancellationToken);
    }
}