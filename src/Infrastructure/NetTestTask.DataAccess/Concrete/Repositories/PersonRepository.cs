using Microsoft.EntityFrameworkCore;
using NetTestTask.DataAccess.Abstractions;
using NetTestTask.DataAccess.Abstractions.Repositories;
using NetTestTask.DataAccess.Persistence.DBContexts;
using NetTestTask.DataAccess.Persistence.Repositories;
using NetTestTask.Domain.Daos.Main;
using System.Linq.Expressions;

namespace NetTestTask.DataAccess.Concrete.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }


        public new async Task<IEnumerable<Person>> GetAllAsync(Expression<Func<Person, bool>> expression)
        {
            return await _appDbContext.GetDbSet<Person>().Where(expression).Include(x => x.Address).ToListAsync();
        }
        public Task<long> Create(Person person)
        {
            return Task<long>.Run(() =>
            {
                return (long)1;
            });
        }
    }
}
