using NetTestTask.Domain.Daos.Main;

namespace NetTestTask.DataAccess.Abstractions.Repositories
{
    public interface IPersonRepository: IRepository<Person>
    {
        public Task<long> Create(Person person);
    }
}
