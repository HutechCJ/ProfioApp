//using MediatR;
//using Profio.Domain.Specifications;
//using Profio.Infrastructure.CQRS.Models;

//namespace Profio.Infrastructure.CQRS.Events.Queries;

//public record GetPagingQueryBase<T>(ISpecification<T> Specification) : IRequest<ListResultModel<T>>
//  where T : BaseModel;