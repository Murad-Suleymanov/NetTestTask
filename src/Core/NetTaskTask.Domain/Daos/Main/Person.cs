using NetTestTask.Domain.Abstraction.DataAccess;
using NetTestTask.Domain.Daos.Base;

namespace NetTestTask.Domain.Daos.Main
{
    public class Person : EntityBase, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
