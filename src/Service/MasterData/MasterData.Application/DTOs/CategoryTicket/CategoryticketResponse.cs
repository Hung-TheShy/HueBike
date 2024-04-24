using Core.SeedWork.ExtendEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterData.Application.DTOs.CategoryTicket
{
    public class CategoryticketResponse : BaseExtendEntities
    {
        public int Index { get; set; }
        public long CategoryTicketId { get; set; }
        public string CategoryTicketName { get; set; }
        public string? Description { get; set; }
        public long Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
