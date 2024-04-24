using Core.Exceptions;
using Core.Helpers.Cryptography;
using Core.Interfaces.Database;
using Core.Properties;
using Core.SeedWork.Repository;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using MasterData.Application.Commands.UnitCommand;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = Infrastructure.AggregatesModel.Authen.AccountAggregate.User;

namespace MasterData.Application.Commands.UserCommand
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string TimeZone { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        public readonly IRepository<User> _userRep;
        public readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IRepository<User> userRep, IUnitOfWork unitOfWork)
        {
            _userRep = userRep;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existUser = await _userRep.GetQuery(e => e.UserName == request.UserName
                                        || (!string.IsNullOrEmpty(request.Email) && e.Email == request.Email) || (!string.IsNullOrEmpty(request.PhoneNumber) && e.PhoneNumber == request.PhoneNumber))
                                        .IgnoreQueryFilters()
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(cancellationToken);

            if (existUser != null)
            {
                if (existUser.UserName == request.UserName)
                    throw new BaseException(ErrorsMessage.MSG_EXIST, "User name");

                if (existUser.Email == request.Email)
                    throw new BaseException(ErrorsMessage.MSG_EXIST, "Email");

                if (existUser.PhoneNumber == request.PhoneNumber)
                    throw new BaseException(ErrorsMessage.MSG_EXIST, "Phone number");

                throw new BaseException(ErrorsMessage.MSG_EXIST, "Account");
            }

            if (string.IsNullOrEmpty(request.FullName))
            {
                throw new BaseException(ErrorsMessage.MSG_REQUIRED, "Fullname");
            }

            if (string.IsNullOrEmpty(request.UserName))
            {
                throw new BaseException(ErrorsMessage.MSG_REQUIRED, "Username");
            }

            if (request.DateOfBirth == null)
            {
                throw new BaseException(ErrorsMessage.MSG_REQUIRED, "DateOfBirth");
            }

            if (string.IsNullOrEmpty(request.Address))
            {
                throw new BaseException(ErrorsMessage.MSG_REQUIRED, "Address");
            }

            if (string.IsNullOrEmpty(request.PhoneNumber))
            {
                throw new BaseException(ErrorsMessage.MSG_REQUIRED, "PhoneNumber");
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                throw new BaseException(ErrorsMessage.MSG_REQUIRED, "Email");
            }

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

            if (request.IsSuperAdmin == null)
            {
                request.IsSuperAdmin = false;
            }

            if (string.IsNullOrEmpty(request.TimeZone))
            {
                throw new BaseException(ErrorsMessage.MSG_REQUIRED, "TimeZone");
            }

            string password = SHACryptographyHelper.SHA256Encrypt(request.Password);

            var user = new User(request.FullName, request.UserName, request.Address, request.Email, request.PhoneNumber, password, request.TimeZone);

            user.IsActive = true;
            user.IsLock = false;
            user.DateOfBirth = request.DateOfBirth;
            user.IsSuperAdmin = request.IsSuperAdmin;


            _userRep.Add(user);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
