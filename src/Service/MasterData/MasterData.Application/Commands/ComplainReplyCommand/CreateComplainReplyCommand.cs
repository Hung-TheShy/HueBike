using Core.Exceptions;
using Core.Interfaces.Database;
using Core.Properties;
using Core.SeedWork.Repository;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using Infrastructure.AggregatesModel.Authen;
using Infrastructure.AggregatesModel.MasterData.UserAggregate.ComplainAggregate;
using MasterData.Application.Commands.ComplainCommand;
using MasterData.Application.DTOs.Complain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Services;
using MasterData.Application.DTOs.ComplainReply;

namespace MasterData.Application.Commands.ComplainReplyCommand
{
    public class CreateComplainReplyCommand : IRequest<ComplainReplyResponse>
    {
        public long UserId { get; set; }
        public long ComplainId { get; set; }
        public string Content { get; set; }
    }

    public class CreateComplainCommandHandler : IRequestHandler<CreateComplainReplyCommand, ComplainReplyResponse>
    {
        public readonly IRepository<Complain> _compRep;
        public readonly IRepository<ComplainReply> _replyRep;
        public readonly IRepository<User> _userRep;
        public readonly IUnitOfWork _unitOfWork;

        public CreateComplainCommandHandler(IRepository<Complain> compRep, IUnitOfWork unitOfWork, IRepository<User> userRep, IRepository<ComplainReply> replyRep)
        {
            _compRep = compRep;
            _userRep = userRep;
            _unitOfWork = unitOfWork;
            _replyRep = replyRep;

        }
        public async Task<ComplainReplyResponse> Handle(CreateComplainReplyCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRep.FindOneAsync(e => e.Id == request.UserId);

            if (user == null)
            {
                throw new BaseException(ErrorsMessage.MSG_NOT_EXIST, "User");
            }

            var complain = await _compRep.FindOneAsync(e => e.Id == request.ComplainId);

            if (user == null)
            {
                throw new BaseException(ErrorsMessage.MSG_NOT_EXIST, "Complain");
            }

            if (request.Content.Length > 500)
            {
                throw new BaseException(ErrorsMessage.MSG_MAX_LENGTH, "Nội dung không quá 500 kí tự");
            }

            var complainReply = new ComplainReply(request.Content, request.ComplainId, request.UserId);

            var complainReplyResponse = new ComplainReplyResponse
            {
                ComplainId = complainReply.Id,
                SenderId = complainReply.SenderId,
                SenderUsername = user.UserName,
                Content = complainReply.Content,
                CreatedDate = complainReply.CreatedDate,
            };

            _replyRep.Add(complainReply);

            await _unitOfWork.SaveChangesAsync();

            return complainReplyResponse;
        }
    }
}
