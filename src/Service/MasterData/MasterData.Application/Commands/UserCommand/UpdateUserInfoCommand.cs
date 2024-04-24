using Core.Exceptions;
using Core.Helpers.Cryptography;
using Core.Interfaces.Database;
using Core.Properties;
using Core.SeedWork.Repository;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterData.Application.Commands.UserCommand
{
    public class UpdateUserInfoCommand : IRequest<bool>
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsSuperAdmin { get; set; }
    }

    public class UpdateUserInfoCommandHandler : IRequestHandler<UpdateUserInfoCommand, bool>
    {
        public readonly IRepository<User> _userRep;
        public readonly IUnitOfWork _unitOfWork;

        public UpdateUserInfoCommandHandler(IRepository<User> userRep, IUnitOfWork unitOfWork)
        {
            _userRep = userRep;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRep.FindOneAsync(e => e.Id == request.UserId);
            if (user == null)
            {
                throw new BaseException("Không tìm thấy người dùng!");
            }

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

            if (request.IsSuperAdmin == null)
            {
                request.IsSuperAdmin = false;
            }

            user.FullName = request.FullName;
            user.DateOfBirth = request.DateOfBirth;
            user.UserName = request.UserName;
            user.Address = request.Address;
            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;
            user.IsSuperAdmin = request.IsSuperAdmin;
            user.UpdatedDate = DateTime.UtcNow;

            _userRep.Update(user);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
