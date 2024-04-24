using Azure.Core;
using Core.Exceptions;
using Core.Properties;
using Core.SeedWork;
using Core.SeedWork.Repository;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using Infrastructure.AggregatesModel.MasterData.Notification;
using Infrastructure.AggregatesModel.MasterData.UserAggregate;
using MasterData.Application.Commands.TransactionCommmand;
using MasterData.Application.Commands.UnitCommand;
using MasterData.Application.DTOs.Notification;
using MasterData.Application.DTOs.Transaction;
using MasterData.Application.DTOs.Unit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MasterData.Application.Queries
{
    public interface ITransactionQuery
    {
        /// <summary>
        /// Chi tiết 1 giao dịch
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TransactionResponse> GetAsync(TransactionDetailCommand command);
        /// <summary>
        /// Danh sách giao dịch
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagingResultSP<TransactionResponse>> ListAllAsync(ListTransactionCommand command);
        /// <summary>
        /// Danh sách giao dịch của khách hàng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagingResultSP<TransactionResponse>> ListUserTranAsync(ListTransactionUserCommand command);
    }

    public class TransactionQuery : ITransactionQuery
    {
        private readonly IRepository<Transaction> _tranRep;
        private readonly IRepository<User> _userRep;
        public TransactionQuery(IRepository<Transaction> tranRep, IRepository<User> userRep)
        {
            _tranRep = tranRep;
            _userRep = userRep;
        }

        public async Task<TransactionResponse> GetAsync(TransactionDetailCommand command)
        {
            var transaction = await _tranRep.FindOneAsync(e => e.Id == command.TransactionId);
            if (transaction == null)
            {
                throw new BaseException("Không tìm thấy giao dịch");
            }

            var transactionResponse = (from Transaction in _tranRep.GetQuery()
                                       join User in _userRep.GetQuery() on Transaction.UserId equals User.Id
                                       where Transaction.Id == command.TransactionId
                                       select new TransactionResponse
                                       {
                                           UserId = User.Id,
                                           TransactionId = Transaction.Id,
                                           IsSuccess = Transaction.IsSuccess,
                                           UserFullName = User.FullName,
                                           Point = Transaction.Point,
                                           CreatedDate = Transaction.CreatedDate,
                                       }).FirstOrDefaultAsync(); // hoặc SingleOrDefaultAsync()

            return await transactionResponse;
        }


        public async Task<PagingResultSP<TransactionResponse>> ListAllAsync(ListTransactionCommand request)
        {
            var transactionResponse = from Transaction in _tranRep.GetQuery()
                                               join User in _userRep.GetQuery() on Transaction.UserId equals User.Id
                                               select new TransactionResponse
                                               {
                                                   UserId = User.Id,
                                                   TransactionId = Transaction.Id,
                                                   IsSuccess = Transaction.IsSuccess,
                                                   UserFullName = User.FullName,
                                                   Point = Transaction.Point,
                                                   CreatedDate = Transaction.CreatedDate,
                                               };

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                request.SearchTerm = request.SearchTerm.ToLower().Trim();
                // Thử chuyển đổi SearchTerm sang long
                long searchTermAsLong;
                bool isNumeric = long.TryParse(request.SearchTerm, out searchTermAsLong);

                request.SearchTerm = request.SearchTerm.ToLower().Trim();
                transactionResponse = transactionResponse.Where(e =>
                    e.UserFullName.ToLower().Contains(request.SearchTerm) ||
                    e.TransactionId == searchTermAsLong || // So sánh với ID dạng long
                    (isNumeric && e.TransactionId == searchTermAsLong)
                );// Kiểm tra nếu SearchTerm có thể chuyển thành long
            }

            if (request.StartDate == null && request.EndDate != null)
            {
                transactionResponse = transactionResponse.Where(e =>
                    e.CreatedDate <= request.EndDate
                );
            }

            if (request.StartDate != null && request.EndDate == null)
            {
                transactionResponse = transactionResponse.Where(e =>
                    e.CreatedDate >= request.StartDate
                );
            }

            if (request.StartDate != null && request.EndDate != null)
            {
                transactionResponse = transactionResponse.Where(e =>
                    request.StartDate <= e.CreatedDate && e.CreatedDate <= request.EndDate
                );
            }

            if (string.IsNullOrEmpty(request.OrderBy) && string.IsNullOrEmpty(request.OrderByDesc))
            {
                transactionResponse = transactionResponse.OrderByDescending(e => e.CreatedDate);
            }
            else
            {
                transactionResponse = PagingSorting.Sorting(request, transactionResponse);
            }
            var pageIndex = request.PageSize * (request.PageIndex - 1);

            var response = await PaginatedList<TransactionResponse>.CreateAsync(transactionResponse, pageIndex, request.PageSize);

            var result = new PagingResultSP<TransactionResponse>(response, response.Total, request.PageIndex, request.PageSize);
            var i = pageIndex + 1;

            foreach (var item in result.Data)
            {
                item.Index = i++;
            }

            return result;

        }

        public async Task<PagingResultSP<TransactionResponse>> ListUserTranAsync(ListTransactionUserCommand request)
        {
            var transactionResponse = from Transaction in _tranRep.GetQuery()
                        join User in _userRep.GetQuery() on Transaction.UserId equals User.Id
                        where Transaction.UserId == request.UserId
                        select new TransactionResponse
                        {
                            UserId = User.Id,
                            TransactionId = Transaction.Id,
                            IsSuccess = Transaction.IsSuccess,
                            UserFullName = User.FullName,
                            Point = Transaction.Point,
                            CreatedDate = Transaction.CreatedDate,
                        };

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {

            }

            if (request.StartDate == null && request.EndDate != null)
            {
                transactionResponse = transactionResponse.Where(e =>
                    e.CreatedDate <= request.EndDate
                );
            }

            if (request.StartDate != null && request.EndDate == null)
            {
                transactionResponse = transactionResponse.Where(e =>
                    e.CreatedDate >= request.StartDate
                );
            }

            if (request.StartDate != null && request.EndDate != null)
            {
                transactionResponse = transactionResponse.Where(e =>
                    request.StartDate <= e.CreatedDate && e.CreatedDate <= request.EndDate
                );
            }


            if (string.IsNullOrEmpty(request.OrderBy) && string.IsNullOrEmpty(request.OrderByDesc))
            {
                transactionResponse = transactionResponse.OrderByDescending(e => e.CreatedDate);
            }
            else
            {
                transactionResponse = PagingSorting.Sorting(request, transactionResponse);
            }
            var pageIndex = request.PageSize * (request.PageIndex - 1);

            var response = await PaginatedList<TransactionResponse>.CreateAsync(transactionResponse, pageIndex, request.PageSize);
            var result = new PagingResultSP<TransactionResponse>(response, response.Total, request.PageIndex, request.PageSize);
            var i = pageIndex + 1;

            foreach (var item in result.Data)
            {
                item.Index = i++;
            }

            return result;
        }
    }
}
