using Core.Models.Base;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AggregatesModel.MasterData.Notification
{
    public class Notification : BaseEntity
    {

        public string Title { get;  set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public long UserId { get; set; }

        public virtual ICollection<UserNotification> UserNotifications { get; set; }
         

        public Notification()
        {
            
        }

        public Notification(string title, string image, string content, long senderId)
        {
            Title = title;
            Image = image;
            Content = content;
            UserId = senderId;
        }



        //factory
        //Tạo thông báo mới
        public static Notification CreateNotification(string title, string image, string content, long senderId)
        {
            //TODO: implement valIdation, error handling stratgies, error notification stratgies
            return new Notification()
            {
                Title = title,
                Image = image,
                Content = content,
                UserId = senderId,
            };
        }
    }
}
