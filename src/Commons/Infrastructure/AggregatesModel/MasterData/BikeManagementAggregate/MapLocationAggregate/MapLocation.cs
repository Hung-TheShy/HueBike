using Core.Models.Base;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.BikeAggregate;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.StationAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.MapLocationAggregate
{
    public class MapLocation : BaseEntity
    {
        public string LocationName { get; set; }  
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public virtual Bike Bike { get; set; }
        public virtual Station Station { get; set; }

        public MapLocation()
        {
            
        }

        public MapLocation(string locationName, string longitude, string latitude)
        {
            LocationName = locationName;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
