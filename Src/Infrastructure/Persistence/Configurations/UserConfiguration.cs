using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FactorioProductionCells.Domain.Entities;
using FactorioProductionCells.Application.Common.Interfaces;

namespace FactorioProductionCells.Infrastructure.Persistence.Configurations
{
    //public class UserConfiguration : AuditableEntityConfiguration<User>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private readonly IDateTimeService _dateTimeService;

        public UserConfiguration(
            IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }
        
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(r => r.Id);
            // Columns
            //builder.Property(r => r.Id).HasDefaultValueSql("uuid_generate_v4()").ValueGeneratedOnAdd().IsRequired();
            builder.Property(r => r.Id).HasDefaultValueSql("uuid_generate_v4()").ValueGeneratedOnAdd();
            builder.Property(r => r.UserName).HasMaxLength(User.UserNameLength).IsRequired();

            // TODO: Can we create and ensure the persistance of data for certain users (e.g. the ones that the ModUpdateScheduler and ModUpdateWorker) will use?
            /*
            builder.HasData(Enum.GetValues(typeof(User))
                .Select(u => new User()
                {
                    UserName = "ModUpdateScheduler"
                })
            );
            */

            /*
            User modUpdateSchedulerUser = new User("ModUpdateScheduler");
            //modUpdateSchedulerUser.AddedByUser = modUpdateSchedulerUser;
            modUpdateSchedulerUser.AddedBy = modUpdateSchedulerUser.Id;
            modUpdateSchedulerUser.AddedDate = _dateTimeService.GetCurrentTime();

            User modUpdateWorkerUser = new User("ModUpdateWorker");
            //modUpdateWorkerUser.AddedByUser = modUpdateWorkerUser;
            modUpdateWorkerUser.AddedBy = modUpdateWorkerUser.Id;
            modUpdateWorkerUser.AddedDate = _dateTimeService.GetCurrentTime();

            builder.HasData(
                modUpdateSchedulerUser,
                modUpdateWorkerUser
            );
            */

            //base.Configure(builder);
        }
    }
}
