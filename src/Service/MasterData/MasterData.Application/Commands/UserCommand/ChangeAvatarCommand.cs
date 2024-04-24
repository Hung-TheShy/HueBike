using Core.Exceptions;
using Core.Interfaces.Database;
using Core.Models.Settings;
using Core.Properties;
using Core.SeedWork.Repository;
using Infrastructure.AggregatesModel.Authen;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MasterData.Application.Commands.AccountCommand;
using MasterData.Application.Commands.UserCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileService = Core.Infrastructure.Services.IFileService;

namespace MasterData.Application.Commands.UserCommand
{
    public class ChangeAvatarCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public IFormFile AvatarFile { get; set; }
    }

    public class ChangeUserAvatarCommandHandle : IRequestHandler<ChangeAvatarCommand, bool>
    {
        private readonly IRepository<User> _userRep;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IRepository<AuthenMedia> _media;

        public ChangeUserAvatarCommandHandle(IRepository<User> userRep, IUnitOfWork unitOfWork, IFileService fileService, IRepository<AuthenMedia> media)
        {
            _userRep = userRep;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _media = media;
        }

        public async Task<bool> Handle(ChangeAvatarCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRep.FindOneAsync(e => e.Id == request.Id);

            if (user == null)
            {
                throw new BaseException(ErrorsMessage.MSG_NOT_EXIST, "User");
            }

            // Upload file avatar
            var image = _fileService.UploadFile(request.AvatarFile,"",false,300,false);

            if (image == null)
            {
                throw new BaseException(ErrorsMessage.MSG_NOT_EXIST, "Image");
            }

            // Cập nhật thông tin avatar cho người dùng
            user.AvatarId = image.Id;

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

            // Lưu thông tin người dùng đã cập nhật vào cơ sở dữ liệu
            _userRep.Update(user);

            await _unitOfWork.SaveChangesAsync();

            // Trả về true để biểu thị rằng quá trình thay đổi avatar thành công
            return true;
        }
    }
}
