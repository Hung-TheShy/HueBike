using Azure.Core;
using Core.SeedWork;
using Core.SeedWork.Repository;
using Infrastructure.AggregatesModel.Authen;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using Infrastructure.AggregatesModel.MasterData.Notification;
using Infrastructure.AggregatesModel.MasterData.UserAggregate.ComplainAggregate;
using MasterData.Application.Commands.ComplainCommand;
using MasterData.Application.Commands.NotificationCommand;
using MasterData.Application.DTOs.Complain;
using MasterData.Application.DTOs.Notification;
using MasterData.Application.DTOs.Unit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MasterData.Application.Queries
{
    public interface INotificationQuery
    {
        /// <summary>
        /// Chi tiết 1 thông báo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<NotificationDetailResponse> GetAsync(NotificationDetailCommand command);

        /// <summary>
        /// Danh sách danh sách thông báo nhận được
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagingResultSP<NotificationReceivedResponse>> ReceivedListAsync(ListNotificationReceivedCommand request);

        /// <summary>
        /// Danh sách danh sách thông báo gửi đi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagingResultSP<NotificationSendedResponse>> SendedListAsync(ListNotificationSendedCommand request);
    }

    public class NotificationQuery : INotificationQuery
    {
        private readonly IRepository<Notification> _notiRep;
        private readonly IRepository<UserNotification> _uNotiRep;
        private readonly IRepository<User> _userRep;
        private readonly IRepository<AuthenMedia> _media;
        public NotificationQuery(IRepository<Notification> notiRep, IRepository<UserNotification> uNotiRep, IRepository<User> userRep, IRepository<AuthenMedia> media)
        {
            _notiRep = notiRep;
            _userRep = userRep;
            _uNotiRep = uNotiRep;
            _media = media;
        }

        public async Task<NotificationDetailResponse> GetAsync(NotificationDetailCommand command)
        {
            var notification = await _notiRep.FindOneAsync(e => e.Id == command.NotificationId);
            var user = await _userRep.FindOneAsync(e => e.Id == notification.UserId);
            var senderAvatar = await _media.FindOneAsync(e => e.Id == user.AvatarId);

            if(senderAvatar == null)
            {
                // Lấy thông tin về complaint
                return await _notiRep.GetQuery(e => e.Id == command.NotificationId)
                    .Select(k => new NotificationDetailResponse
                    {
                        NotificationId = command.NotificationId,
                        SenderId = k.UserId,
                        SenderUsername = user.UserName,
                        SenderAvatar = null,
                        Title = k.Title,
                        Image = k.Image,
                        Content = k.Content,
                        CreatedDate = k.CreatedDate,
                    }).FirstOrDefaultAsync();
            }

            // Lấy thông tin về complaint
            return await _notiRep.GetQuery(e => e.Id == command.NotificationId)
                .Select(k => new NotificationDetailResponse
                {
                    NotificationId = command.NotificationId,
                    SenderId = k.UserId,
                    SenderUsername = user.UserName,
                    SenderAvatar = senderAvatar.FilePath,
                    Title = k.Title,
                    Image = k.Image,
                    Content = k.Content,
                    CreatedDate = k.CreatedDate,
                }).FirstOrDefaultAsync();

        }

        public async Task<PagingResultSP<NotificationReceivedResponse>> ReceivedListAsync(ListNotificationReceivedCommand request)
        {
            var notificationReceivedResponse = from Notification in _notiRep.GetQuery()
                        join UserNotification in _uNotiRep.GetQuery() on Notification.Id equals UserNotification.NotificationId
                        where UserNotification.UserId == request.UserId
                        select new NotificationReceivedResponse
                        {
                            NotificationId = Notification.Id,
                            Title = Notification.Title,
                            Image = Notification.Image,
                            Content = Notification.Content,
                            CreatedDate = Notification.CreatedDate,
                        };

            if (string.IsNullOrEmpty(request.OrderBy) && string.IsNullOrEmpty(request.OrderByDesc))
            {
                notificationReceivedResponse = notificationReceivedResponse.OrderByDescending(e => e.CreatedDate);
            }
            else
            {
                notificationReceivedResponse = PagingSorting.Sorting(request, notificationReceivedResponse);
            }
            var pageIndex = request.PageSize * (request.PageIndex - 1);

            var response = await PaginatedList<NotificationReceivedResponse>.CreateAsync(notificationReceivedResponse, pageIndex, request.PageSize);

            var result = new PagingResultSP<NotificationReceivedResponse>(response, response.Total, request.PageIndex, request.PageSize);
            var i = pageIndex + 1;

            foreach (var item in result.Data)
            {
                item.Index = i++;
            }

            return result;
        }

        public async Task<PagingResultSP<NotificationSendedResponse>> SendedListAsync(ListNotificationSendedCommand request)
        {
            var notificationSenderResponse = from notification in _notiRep.GetQuery()
                                             join user in _userRep.GetQuery() on notification.UserId equals user.Id
                                             select new NotificationSendedResponse
                                             {
                                                 NotificationId = notification.Id,
                                                 SenderId = notification.UserId,
                                                 SenderUsername = user.UserName,
                                                 Title = notification.Title,
                                                 Image = notification.Image,
                                                 Content = notification.Content,
                                                 CreatedDate = notification.CreatedDate,
                                             };

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                request.SearchTerm = request.SearchTerm.ToLower().Trim();
                // Thử chuyển đổi SearchTerm sang long
                long searchTermAsLong;
                bool isNumeric = long.TryParse(request.SearchTerm, out searchTermAsLong);

                notificationSenderResponse = notificationSenderResponse.Where(e =>
                    e.NotificationId == searchTermAsLong || // So sánh với ID dạng long
                    e.SenderId == searchTermAsLong || // So sánh với ID dạng long
                    (isNumeric && e.NotificationId == searchTermAsLong) // Kiểm tra nếu SearchTerm có thể chuyển thành long
                );
            }


            if (string.IsNullOrEmpty(request.OrderBy) && string.IsNullOrEmpty(request.OrderByDesc))
            {
                notificationSenderResponse = notificationSenderResponse.OrderByDescending(e => e.CreatedDate);
            }
            else
            {
                notificationSenderResponse = PagingSorting.Sorting(request, notificationSenderResponse);
            }
            var pageIndex = request.PageSize * (request.PageIndex - 1);

            var response = await PaginatedList<NotificationSendedResponse>.CreateAsync(notificationSenderResponse, pageIndex, request.PageSize);

            var result = new PagingResultSP<NotificationSendedResponse>(response, response.Total, request.PageIndex, request.PageSize);
            var i = pageIndex + 1;

            foreach (var item in result.Data)
            {
                item.Index = i++;
            }

            return result;
        }
    }
}
