using Core.Infrastructure.Controllers;
using Core.Models;
using Core.Properties;
using MasterData.Application.Commands.CategoryTicketCommand;
using MasterData.Application.Commands.NotificationCommand;
using MasterData.Application.Commands.TransactionCommmand;
using MasterData.Application.DTOs.CategoryTicket;
using MasterData.Application.DTOs.Notification;
using MasterData.Application.DTOs.Transaction;
using MasterData.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static MasterData.API.Commons.Routes;

namespace MasterData.API.Controllers
{
    [Route(TransactionRoutes.Prefix)]
    public class TransactionController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ITransactionQuery _query;

        public TransactionController(IMediator mediator, ITransactionQuery query)
        {
            _mediator = mediator;
            _query = query;
        }

        /// <summary>
        /// Danh sách tất cả giao dịch
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet(TransactionRoutes.ListAll)]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<TransactionResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAllTransaction([FromQuery] ListTransactionCommand command)
        {
            var response = await _query.ListAllAsync(command);

            return Ok(new ApiSuccessResult<IList<TransactionResponse>>
            {
                Data = response.Data,
                Paging = response.Paging,
                Message = null
            });
        }

        /// <summary>
        /// Lịch sử giao dịch của người dùng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet(TransactionRoutes.ListUser)]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<TransactionResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListTransactionUser([FromQuery] ListTransactionUserCommand command)
        {
            var response = await _query.ListUserTranAsync(command);

            return Ok(new ApiSuccessResult<IList<TransactionResponse>>
            {
                Data = response.Data,
                Paging = response.Paging,
                Message = null
            });
        }

        /// <summary>
        /// Thông tin chi tiết 1 giao dịch
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet(TransactionRoutes.Detail)]
        [ProducesResponseType(typeof(ApiSuccessResult<TransactionResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotificationDetail([FromQuery] TransactionDetailCommand command)
        {
            var response = await _query.GetAsync(command);

            return Ok(new ApiSuccessResult<TransactionResponse>(
                data: response));
        }

        /// <summary>
        /// Giao dịch nạp tiền
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost(TransactionRoutes.Recharge)]
        [ProducesResponseType(typeof(ApiSuccessResult<TransactionResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Recharge([FromForm] RechargeCommmand command)
        {
            var response = await _mediator.Send(command);

            return Ok(new ApiSuccessResult<TransactionResponse>(
                data: response,
                message: string.Format("Nạp tiền thành công!")));
        }
    }
}
