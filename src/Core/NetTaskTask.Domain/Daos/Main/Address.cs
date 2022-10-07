using NetTestTask.Domain.Daos.Base;
using NetTestTask.Domain.Abstraction.DataAccess;

namespace NetTestTask.Domain.Daos.Main
{
    public class Address: EntityBase, IEntity
    {
        public string City { get; set; }
        public string AddressLine { get; set; }
    }
}
