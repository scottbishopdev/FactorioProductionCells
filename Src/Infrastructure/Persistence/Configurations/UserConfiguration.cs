using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Entities;

namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : AuditableEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(r => r.Id);
            // Columns
            builder.Property(r => r.Id).HasDefaultValueSql("uuid_generate_v4()").ValueGeneratedOnAdd().IsRequired();
            builder.Property(r => r.Username).HasMaxLength(User.UserNameLength).IsRequired();

            // TODO: Can we create and ensure the persistance of data for certain users (e.g. the ones that the ModUpdateScheduler and ModUpdateWorker) will use?
            /*
            builder.HasData(Enum.GetValues(typeof(User))
                .Select(u => new User()
                {
                    UserName = "ModUpdateScheduler"
                })
            );
            */

            base.Configure(builder);
        }
    }
}
