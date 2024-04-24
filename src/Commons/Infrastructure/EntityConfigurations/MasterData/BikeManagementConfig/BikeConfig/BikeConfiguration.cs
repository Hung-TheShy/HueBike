using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.BikeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfigurations.MasterData.BikeManagementConfig.BikeConfig
{
    public class BikeConfiguration : BaseConfiguration, IEntityTypeConfiguration<Bike>
    {
        public void Configure(EntityTypeBuilder<Bike> builder)
        {
            builder.ToTable("Bike");
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.BikeName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LocationId).HasMaxLength(20).IsRequired();
            builder.Property(x => x.LockId).HasMaxLength(20);
            builder.Property(x => x.StatusId).HasMaxLength(20).IsRequired();

            builder.HasIndex(x => x.LockId).IsUnique();

            builder
                .HasOne(x => x.BikeLock)
                .WithOne(y => y.Bike)
                .HasForeignKey<Bike>(x => x.LockId);

            builder
                .HasOne(x => x.Location)
                .WithOne(y => y.Bike)
                .HasForeignKey<Bike>(x => x.LocationId);

            builder
                .HasOne(x => x.Status)
                .WithMany(y => y.Bikes)
                .HasForeignKey(x => x.StatusId);

            ConfigureBase(builder);
        }
    }
}
