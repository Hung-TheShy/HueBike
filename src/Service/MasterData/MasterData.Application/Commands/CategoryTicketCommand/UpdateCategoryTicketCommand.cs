using Core.Exceptions;
using Core.Interfaces.Database;
using Core.Properties;
using Core.SeedWork.Repository;
using GoogleApi.Entities.Interfaces;
using Infrastructure.AggregatesModel.Authen.AccountAggregate;
using Infrastructure.AggregatesModel.Authen;
using Infrastructure.AggregatesModel.MasterData.TripManagementAggregate.TicketAggregate;
using MasterData.Application.DTOs.CategoryTicket;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterData.Application.Commands.CategoryTicketCommand
{
    public class UpdateCategoryTicketCommand : IRequest<CategoryticketResponse>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public long Price { get; set; }
    }

    public class UpdateCategoryTicketCommandHandler : IRequestHandler<UpdateCategoryTicketCommand, CategoryticketResponse>
    {
        public readonly IRepository<CategoryTicket> _cateRep;
        public readonly IRepository<User> _userRep;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AuthenMedia> _media;

        public UpdateCategoryTicketCommandHandler(IRepository<CategoryTicket> cateRep, IUnitOfWork unitOfWork, IRepository<AuthenMedia> media, IRepository<User> userRep)
        {
            _cateRep = cateRep;
            _userRep = userRep;
            _unitOfWork = unitOfWork;
            _media = media;
        }
        public async Task<CategoryticketResponse> Handle(UpdateCategoryTicketCommand request, CancellationToken cancellationToken)
        {
            var categoryTicket = await _cateRep.FindOneAsync(e => e.Id == request.Id);
            if(categoryTicket == null)
            {
                throw new BaseException("Không tìm thấy loại vé này!");
            }

            var isEXName = await _cateRep.GetAny(e => e.CategoryTicketName == request.Name);

            if (isEXName)
            {
                throw new BaseException(ErrorsMessage.MSG_EXIST, "Tên loại vé");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                throw new BaseException(ErrorsMessage.MSG_NOT_EXIST, "Tên loại vé");
            }

            if (request.Price < 0)
            {
                throw new BaseException("Giá loại vé không hợp lệ");
            }

            if (request.Price == 0)
            {
                throw new BaseException("Giá loại vé không được để trống");
            }

            categoryTicket.CategoryTicketName = request.Name;
            categoryTicket.Description = request.Description;
            categoryTicket.Price = request.Price;
            categoryTicket.UpdatedDate = DateTime.UtcNow;

            _cateRep.Update(categoryTicket);

            await _unitOfWork.SaveChangesAsync();


            var result = new CategoryticketResponse
            {
                CategoryTicketId = categoryTicket.Id,
                CategoryTicketName = categoryTicket.CategoryTicketName,
                Description = categoryTicket.Description,
                Price = categoryTicket.Price,
                CreatedDate = categoryTicket.CreatedDate,
                UpdatedDate = categoryTicket.UpdatedDate,
            };

            return result;
        }
    }
}
