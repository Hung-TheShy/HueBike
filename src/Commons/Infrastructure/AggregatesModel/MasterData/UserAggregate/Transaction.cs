using Core.Models.Base;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AggregatesModel.MasterData.UserAggregate
{
    public class Transaction : BaseEntity
    {
        public long Point {  get; set; }
        public long UserId { get; set; }
        public bool IsSuccess { get; set; }

        public virtual User User { get; set; }

        public Transaction()
        {
            
        }

        public Transaction(long point, long userId, bool isSuccess)
        {
            Point = point;
            UserId = userId;
            IsSuccess = isSuccess;
            CreatedDate = DateTime.Now;
        }
    }
}
