using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Profio.Application.CQRS.Events.Queries;
using Profio.Application.CQRS.Handlers.Queries;
using Profio.Application.CQRS.Validators;
using Profio.Domain.Entities;

namespace Profio.Application.Hubs.Queries
{
    public record GetHubByIdQuery(object Id) : GetByIdQueryBase<HubDto>(Id);

    public class GetHubByIdQueryHandler : GetByIdQueryHandlerBase<GetHubByIdQuery, HubDto, Hub>
    {
        public GetHubByIdQueryHandler(IRepositoryFactory unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
    public class GetHubByIdQueryValidator : GetByIdQueryValidatorBase<GetHubByIdQuery, HubDto> { }

}