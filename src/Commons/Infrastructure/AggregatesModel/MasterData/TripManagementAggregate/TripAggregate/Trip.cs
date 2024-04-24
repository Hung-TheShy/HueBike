using Core.Models.Base;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.StationAggregate;
using Infrastructure.AggregatesModel.MasterData.TripManagementAggregate.RateAggregate;
using Infrastructure.AggregatesModel.MasterData.TripManagementAggregate.TicketAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AggregatesModel.MasterData.TripManagementAggregate.TripAggregate
{
    public class Trip : BaseEntity
    {
        public string TripName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool Status { get; set; }
        public long StationId { get; set;}
        public long TicketId { get; set;}

        public virtual Station EndStation { get; set; }    
        public virtual Ticket Ticket { get; set; }
        public virtual Rate Rate { get; set; }

        public Trip()
        {
            
        }

        public Trip(string tripName, long stationId, long ticketId)
        {
            TripName = tripName;
            StartDate = DateTime.UtcNow;
            EndDate = DateTime.UtcNow;
            StartTime = DateTime.UtcNow.TimeOfDay;
            EndTime = DateTime.UtcNow.TimeOfDay;
            Status = true;
            StationId = stationId;
            TicketId = ticketId;
        }

        //create a trip
        public static Trip CreateTrip(string tripName, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime, long stationId, long ticketId)
        {
            return new Trip
            {
                TripName = tripName,
                StartDate = startDate,
                EndDate = endDate,
                StartTime = startTime,
                EndTime = endTime,
                Status = true,
                StationId = stationId,
                TicketId = ticketId
            };
        }
    }
}
