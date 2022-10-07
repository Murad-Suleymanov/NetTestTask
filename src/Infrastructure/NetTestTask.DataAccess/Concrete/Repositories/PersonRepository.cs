using NetTestTask.DataAccess.Abstractions;
using NetTestTask.DataAccess.Abstractions.Repositories;
using NetTestTask.DataAccess.Persistence.Repositories;
using NetTestTask.Domain.Daos.Main;

namespace NetTestTask.DataAccess.Concrete.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(IAppDbContext appDbContext) : base(appDbContext)
        {
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
