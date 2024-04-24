using Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AggregatesModel.MasterData.TripManagementAggregate.TicketAggregate
{
    public class CategoryTicket : BaseEntity
    {
        public string CategoryTicketName { get; set; }
        public string? Description { get; set; }
        public long Price { get; set; }

        //Lấy danh sách vé có loại vé x
        public virtual ICollection<Ticket> Tickets { get; set; }

        public CategoryTicket()
        {
            
        }

        public CategoryTicket(string categoryTicketName, string description, long price)
        {
            CategoryTicketName = categoryTicketName;
            Description = description;
            Price = price;
        }

        //add CategoryTicket
        public static CategoryTicket CreateCategoryTicket(string categoryTicketName, string description, long price )
        {
            return new CategoryTicket
            {
                CategoryTicketName = categoryTicketName,
                Description = description,
                Price = price
            };
        }

        //change ticket price
        public void ChangePrice(long price)
        {
            Price = price;
        }

        //remove ticket
        public void RemoveTicket(Ticket removeTicket)
        {
            Tickets.Remove(removeTicket);
        }
    }
}
