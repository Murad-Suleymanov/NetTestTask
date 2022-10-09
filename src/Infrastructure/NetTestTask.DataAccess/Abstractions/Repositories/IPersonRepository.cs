using NetTestTask.Domain.Daos.Main;
using System.Linq.Expressions;

namespace NetTestTask.DataAccess.Abstractions.Repositories
{
    public interface IPersonRepository: IRepository<Person>
    {
        public Task<long> Create(Person person);
        public new Task<IEnumerable<Person>> GetAllAsync(Expression<Func<Person, bool>> expression);
    }
}
