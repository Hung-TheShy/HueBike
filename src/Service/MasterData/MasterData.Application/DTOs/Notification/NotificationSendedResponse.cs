﻿using Core.SeedWork.ExtendEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterData.Application.DTOs.Notification
{
    public class NotificationSendedResponse : BaseExtendEntities
    {
        public int Index { get; set; }
        public long NotificationId { get; set; }
        public long SenderId { get; set; }
        public string SenderUsername { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
