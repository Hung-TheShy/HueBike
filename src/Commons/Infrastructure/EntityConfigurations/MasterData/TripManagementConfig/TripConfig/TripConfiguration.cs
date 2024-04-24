using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using Infrastructure.AggregatesModel.MasterData.TripManagementAggregate.TripAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfigurations.MasterData.TripManagementConfig.TripConfig
{
    public class TripConfiguration : BaseConfiguration, IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.ToTable("Trip");
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.TripName).HasMaxLength(20).IsRequired();
            builder.Property(x => x.StartDate).HasMaxLength(50).IsRequired();
            builder.Property(x => x.EndDate).HasMaxLength(50).IsRequired();
            builder.Property(x => x.StartTime).HasMaxLength(50).IsRequired();
            builder.Property(x => x.EndTime).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Status).HasMaxLength(20).IsRequired();
            builder.Property(x => x.StationId).HasMaxLength(20).IsRequired();
            builder.Property(x => x.TicketId).HasMaxLength(20).IsRequired();

            builder.HasIndex(x => x.TicketId).IsUnique();

            builder
                .HasOne(x => x.EndStation)
                .WithMany(y => y.Trips)
                .HasForeignKey(x => x.StationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Ticket)
                .WithOne(y => y.Trip)
                .HasForeignKey<Trip>(x => x.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            ConfigureBase(builder);
        }
    }
}
