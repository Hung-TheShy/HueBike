using Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.BikeAggregate
{
    public class BikeLock : BaseEntity
    {
        public string LockName { get; set; }
        public string PathQr { get; set; }
        public int Power { get; set; }
        public bool IsUsed { get; set; }

        public virtual Bike Bike { get; set; }

        public BikeLock()
        {
            
        }

        public BikeLock(string lockName, string pathQr, int power, bool isUsed)
        {
            LockName = lockName;
            PathQr = pathQr;
            Power = power;
            IsUsed = isUsed;
        }
    }
}
