using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTestTask.DataAccess.Abstractions;
using NetTestTask.DataAccess.Persistence.Configurations.Base;
using NetTestTask.Domain.Daos.Main;

namespace NetTestTask.DataAccess.Persistence.Configurations.Main
{
    internal class AddressConfig: EntityBaseConfig<Address>, IEntityConfiguration
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            builder.ToTable("Addresses");
        }
    }
}
