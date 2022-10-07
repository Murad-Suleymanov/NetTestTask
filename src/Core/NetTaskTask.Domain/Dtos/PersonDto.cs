using NetTestTask.Domain.Daos.Main;

namespace NetTestTask.Domain.Dtos
{
    public class PersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual AddressDto Address { get; set; }
    }
}
