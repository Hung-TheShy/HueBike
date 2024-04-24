using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.BikeStationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfigurations.MasterData.BikeManagementConfig.BikeStationConfig
{
    public class BikeStationConfiguration : BaseConfiguration, IEntityTypeConfiguration<BikeStation>
    {
        public void Configure(EntityTypeBuilder<BikeStation> builder)
        {
            builder.ToTable("BikeStation");
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.BikeId).HasMaxLength(20).IsRequired();
            builder.Property(x => x.StationId).HasMaxLength(20).IsRequired();


            builder
                .HasOne(x => x.Bike)
                .WithMany(y => y.StationChanges)
                .HasForeignKey(x => x.BikeId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder
                .HasOne(x => x.Station)
                .WithMany(y => y.BikeChanges)
                .HasForeignKey(x => x.StationId)
                .OnDelete(DeleteBehavior.Restrict); 


            ConfigureBase(builder);
        }
    }
}
