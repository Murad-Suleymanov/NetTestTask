using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTestTask.DataAccess.Abstractions;
using NetTestTask.DataAccess.Persistence.Configurations.Base;
using NetTestTask.Domain.Daos.Main;

namespace NetTestTask.DataAccess.Persistence.Configurations.Main
{
    internal class PersonConfig: EntityBaseConfig<Person>, IEntityConfiguration
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            base.Configure(builder);

            builder.HasOne(p => p.Address)
                .WithMany()
                .HasForeignKey(p => p.AddressId);

            builder.ToTable("Persons");
        }
    }
}
