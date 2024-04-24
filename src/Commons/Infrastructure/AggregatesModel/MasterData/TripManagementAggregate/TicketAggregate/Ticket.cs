using Core.Models.Base;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using Infrastructure.AggregatesModel.MasterData.BikeManagementAggregate.BikeAggregate;
using Infrastructure.AggregatesModel.MasterData.TripManagementAggregate.TripAggregate;
using MediatR.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AggregatesModel.MasterData.TripManagementAggregate.TicketAggregate
{
    public class Ticket : BaseEntity
    {
        public string TicketName { get; set; } = string.Empty;
        public string? PathQr {  get; set; }
        public DateTime? BookingDate { get; set; }
        public TimeSpan? BookingTime { get; set; }
        public bool IsUsed { get; set; }
        public long CategoryTicketId { get; set; }
        public long UserId { get; set;}
        public long BikeId { get; set; }

        public virtual User User { get; set; }
        public virtual Bike Bike { get; set; }
        public virtual CategoryTicket CategoryTicket { get; set; }
        public virtual Trip Trip { get; set; }

        public Ticket()
        {
            
        }

        public Ticket(string ticketName, string pathQr, DateTime bookingDate, TimeSpan bookingTime, bool isUsed, long categoryTicketId, long userId, long bikeId)
        {
            TicketName = ticketName;
            PathQr = pathQr;
            BookingDate = bookingDate;
            BookingTime = bookingTime;
            IsUsed = isUsed;
            CategoryTicketId = categoryTicketId;
            UserId = userId;
            BikeId = bikeId;
        }

        //Thay đổi thời gian đặt vé
        public void ChangeOrderTime(DateTime newBookingDate, TimeSpan newBookingTime)
        {
            BookingDate = newBookingDate;
            BookingTime = newBookingTime;
        }
    }
}
