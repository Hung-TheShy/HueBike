using Core.Exceptions;
using Core.Interfaces.Database;
using Core.Properties;
using Core.SeedWork.Repository;
using Infrastructure.AggregatesModel.Authen;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using Infrastructure.AggregatesModel.MasterData.UserAggregate.ComplainAggregate;
using Infrastructure.Services;
using MasterData.Application.Commands.UnitCommand;
using MasterData.Application.DTOs.Complain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterData.Application.Commands.ComplainCommand
{
    public class CreateComplainCommand : IRequest<ComplainResponse>
    {
        public long UserId { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Content { get; set; }
    }

    public class CreateComplainCommandHandler : IRequestHandler<CreateComplainCommand, ComplainResponse>
    {
        public readonly IRepository<Complain> _compRep;
        public readonly IRepository<User> _userRep;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AuthenMedia> _media;
        private readonly IFileService _fileService;

        public CreateComplainCommandHandler(IRepository<Complain> compRep, IUnitOfWork unitOfWork, IRepository<AuthenMedia> media, IRepository<User> userRep, IFileService fileService)
        {
            _compRep = compRep;
            _userRep = userRep;
            _unitOfWork = unitOfWork;
            _media = media;
            _fileService = fileService;
        }
        public async Task<ComplainResponse> Handle(CreateComplainCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRep.FindOneAsync(e => e.Id == request.UserId);

            if (user == null)
            {
                throw new BaseException(ErrorsMessage.MSG_NOT_EXIST, "User");
            }

            if (request.Content.Length > 500)
            {
                throw new BaseException(ErrorsMessage.MSG_MAX_LENGTH, "Nội dung không quá 500 kí tự");
            }

            // Upload file avatar
            var image = _fileService.UploadFile(request.ImageFile, "", false, 300, false);

            //Thêm authenMedia mới vào database
            var newmedia = new AuthenMedia
            {
                User = user,
                FileName = image.Result.FileName,
                FileNameUpload = image.Result.FileNameUpload,
                StatusCode = image.Result.StatusCode,
                Extension = image.Result.Extension,
                Type = image.Result.Type,
                FilePath = image.Result.FilePath,
                FilePathThumbnail = image.Result.FilePathThumbnail,

            };


            _media.Add(newmedia);

            var complain = new Complain(request.Content, newmedia.FilePath,request.UserId);

            var complainResponse = new ComplainResponse
            {
                ComplainId  = complain.Id,
                SenderId = complain.SenderId,
                SenderUsername = user.UserName,
                Image = complain.Image,
                Content = complain.Content,
                CreatedDate = complain.CreatedDate,
            };

            _compRep.Add(complain);

            await _unitOfWork.SaveChangesAsync();

            return complainResponse;
        }
    }
}
