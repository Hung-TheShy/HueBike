using Core.Exceptions;
using Core.Interfaces.Database;
using Core.Properties;
using Core.SeedWork.Repository;
using Infrastructure.AggregatesModel.Authen;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using Infrastructure.AggregatesModel.MasterData.Notification;
using Infrastructure.Services;
using MasterData.Application.Commands.UnitCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterData.Application.Commands.NotificationCommand
{
    public class CreateNotificationCommand : IRequest<long>
    {
        public long UserId { get; set; }
        public string Title { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Content { get; set; }
    }

    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, long>
    {
        public readonly IRepository<User> _userRep;
        public readonly IRepository<Notification> _notiRep;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AuthenMedia> _media;
        private readonly IFileService _fileService;

        public CreateNotificationCommandHandler(IRepository<User> userRep, IUnitOfWork unitOfWork, IRepository<Notification> notiRep, IRepository<AuthenMedia> media, IFileService fileService)
        {
            _userRep = userRep;
            _notiRep = notiRep;
            _unitOfWork = unitOfWork;
            _media = media;
            _fileService = fileService;
        }
        public async Task<long> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRep.FindOneAsync(e => e.Id == request.UserId);

            if (user == null)
            {
                throw new BaseException(ErrorsMessage.MSG_NOT_EXIST, "User");
            }

            if (request.Title.Length > 120)
            {
                throw new BaseException(ErrorsMessage.MSG_NOT_VALIDATE, "Title");
            }

            if (request.Content.Length > 500)
            {
                throw new BaseException(ErrorsMessage.MSG_NOT_VALIDATE, "Content");
            }

            // Upload file avatar
            var image = _fileService.UploadFile(request.ImageFile, "", false, 300, false);

            //Thêm authenMedia mới vào database
            var newmedia = new AuthenMedia
            {
                FileName = image.Result.FileName,
                FileNameUpload = image.Result.FileNameUpload,
                StatusCode = image.Result.StatusCode,
                Extension = image.Result.Extension,
                Type = image.Result.Type,
                FilePath = image.Result.FilePath,
                FilePathThumbnail = image.Result.FilePathThumbnail,

            };


            _media.Add(newmedia);

            var notification = new Notification(request.Title, newmedia.FilePath, request.Content, request.UserId);

            _notiRep.Add(notification);

            await _unitOfWork.SaveChangesAsync();

            return notification.Id;
        }
    }
}
