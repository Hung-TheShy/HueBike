using Core.SeedWork.ExtendEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterData.Application.DTOs.Transaction
{
    public class TransactionResponse : BaseExtendEntities
    {
        public int Index { get; set; }
        public long UserId { get; set; }
        public long TransactionId { get; set; }
        public bool IsSuccess { get; set; }
        public string UserFullName { get; set; }
        public long Point {  get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
