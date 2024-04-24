using Core.Exceptions;
using Core.Interfaces.Database;
using Core.Properties;
using Core.SeedWork.Repository;
using GoogleApi.Entities.Interfaces;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using Infrastructure.AggregatesModel.MasterData.UserAggregate;
using MasterData.Application.Commands.StatusCommand;
using MasterData.Application.DTOs.Status;
using MasterData.Application.DTOs.Transaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterData.Application.Commands.TransactionCommmand
{
    public class RechargeCommmand : IRequest<TransactionResponse>
    {
        public long UserId { get; set; }
        public long Point { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class RechargeCommmandHandler : IRequestHandler<RechargeCommmand, TransactionResponse>
    {
        public readonly IRepository<Transaction> _tranRep;
        public readonly IRepository<User> _userRep;
        public readonly IUnitOfWork _unitOfWork;

        public RechargeCommmandHandler(IRepository<Transaction> tranRep, IRepository<User> userRep, IUnitOfWork unitOfWork)
        {
            _tranRep = tranRep;
            _userRep = userRep;
            _unitOfWork = unitOfWork;
        }
        public async Task<TransactionResponse> Handle(RechargeCommmand request, CancellationToken cancellationToken)
        {
            var user = await _userRep.FindOneAsync(e => e.Id == request.UserId);

            if (user == null)
            {
                throw new BaseException(ErrorsMessage.MSG_NOT_EXIST, "Người dùng");
            }

            if(request.IsSuccess == true)
            {
                var transaction = new Transaction(request.Point, request.UserId, true);
                _tranRep.Add(transaction);

                user.Point = request.Point;
                _userRep.Update(user);

                await _unitOfWork.SaveChangesAsync();

                var result = new TransactionResponse
                {
                    UserId = user.Id,
                    TransactionId = transaction.Id,
                    IsSuccess = true,
                    UserFullName = user.FullName,
                    Point = request.Point,
                    CreatedDate = transaction.CreatedDate,
                };
                return result;
                throw new BaseException("Nạp tiền thành công!");
            }
            else
            {
                var transaction = new Transaction(request.Point, request.UserId, false);
                _tranRep.Add(transaction);

                await _unitOfWork.SaveChangesAsync();

                throw new BaseException("Nạp tiền thất bại!");
            }
        }
    }
}
