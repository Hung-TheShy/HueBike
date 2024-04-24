using Core.Models.Base;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.BikeAggregate;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.BikeStationAggregate;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.MapLocationAggregate;
using Infrastructure.AggregatesModel.MasterData.StatusAggregate;
using Infrastructure.AggregatesModel.MasterData.TripManagementAggregate.TripAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.StationAggregate
{
    public class Station : BaseEntity
    {
        public string StationName { get; set; }
        public int QuantityAvaiable { get; set; }
        public int NumOfSeats { get; set; }
        public long LocationId { get; set; }
        public long StatusId { get; set;}

        public virtual MapLocation Location { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<BikeStation> BikeChanges { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }


        public Station()
        {
            
        }

        public Station(string stationName, int quantityAvaiable, int numOfSeats, long locationId, long statusId, Status status)
        {
            StationName = stationName;
            QuantityAvaiable = quantityAvaiable;
            NumOfSeats = numOfSeats;
            LocationId = locationId;
            StatusId = statusId;
            Status = status;
        }

        //Add Station
        public static Station CreateStation(string stationName, int quantityAvaiable, int numOfSeats, long locationId, long statusId)
        {
            return new Station
            {
                StationName = stationName,
                QuantityAvaiable = quantityAvaiable,
                NumOfSeats = numOfSeats,
                LocationId = locationId,
                StatusId = statusId
            };
        }

    }
}
