using Core.Exceptions;
using Core.Helpers.Cryptography;
using Core.Interfaces.Database;
using Core.Properties;
using Core.SeedWork.Repository;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = Infrastructure.AggregatesModel.Authen.AccountAggregate.User;

namespace MasterData.Application.Commands.UserCommand
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        public readonly IRepository<User> _userRep;
        public readonly IUnitOfWork _unitOfWork;

        public ChangePasswordCommandHandler(IRepository<User> userRep, IUnitOfWork unitOfWork)
        {
            _userRep = userRep;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRep.FindOneAsync(e => e.Id == request.Id);
            if (user == null)
            {
                throw new BaseException("Không tìm thấy người dùng!");
            }

            string password = SHACryptographyHelper.SHA256Encrypt(request.Password);

            if (string.IsNullOrEmpty(request.Password))
            {
                throw new BaseException(ErrorsMessage.MSG_REQUIRED, "Password");
            }

            if (string.IsNullOrEmpty(request.PasswordConfirm))
            {
                throw new BaseException(ErrorsMessage.MSG_REQUIRED, "PasswordConfirm");
            }

            if (request.Password != request.PasswordConfirm)
            {
                throw new BaseException(ErrorsMessage.MSG_FAILED, "Mật khẩu xác nhận không trùng khớp, ");
            }

            
            user.Password = password;
            user.UpdatedDate = DateTime.UtcNow;


            User.ResetPassword(ref user, password);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
