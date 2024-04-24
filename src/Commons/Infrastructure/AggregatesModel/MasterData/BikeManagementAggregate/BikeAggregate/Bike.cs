using Core.Models.Base;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.BikeStationAggregate;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.MapLocationAggregate;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.StationAggregate;
using Infrastructure.AggregatesModel.MasterData.StatusAggregate;
using Infrastructure.AggregatesModel.MasterData.TripManagementAggregate.TicketAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.BikeAggregate
{
    public class Bike : BaseEntity
    {
        public string BikeName { get; set; }
        public long? LocationId { get; set; }
        public long? LockId { get; set; }
        public long? StatusId { get; set;}

        public virtual Status Status { get; set; }
        public virtual MapLocation Location { get; set; }
        public virtual BikeLock BikeLock { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<BikeStation> StationChanges { get; set; }

        public Bike()
        {
            
        }

        public Bike(string bikeName, long locationId, long lockId, long statusId)
        {
            BikeName = bikeName;
            LocationId = locationId;
            LockId = lockId;
            StatusId = statusId;
        }

        public static Bike CreateBike(string bikeName, long locationId, long lockId, long statusId)
        {
            return new Bike
            {
                BikeName = bikeName,
                LocationId = locationId,
                LockId = lockId,
                StatusId = statusId
            };
        }

        //station change: Sự thay đổi của xe tại các trạm
        public void StationChange(BikeStation newChange)
        {
            var stations = new List<Station>();
            foreach(var item in stations)
            {
                if(newChange.StationId == item.Id && item.QuantityAvaiable < item.NumOfSeats)
                {
                    item.QuantityAvaiable += 1;
                }
            }
            StationChanges.Add(newChange);
        }

        //Thêm khóa mới cho xe
        public void AddLock(BikeLock newLock)
        {
            LockId = newLock.Id;
        }
    }
}
