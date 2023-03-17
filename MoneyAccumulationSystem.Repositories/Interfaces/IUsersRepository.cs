using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoneyAccumulationSystem.Database.EF.Models;

namespace MoneyAccumulationSystem.Repositories.Interfaces;

public interface IUsersRepository
{
    Task<IList<User>> GetListAsync(CancellationToken cancellationToken);
    Task<User> GetAsync(int? userId = null, string login = null, string password = null, CancellationToken cancellationToken = default);
}