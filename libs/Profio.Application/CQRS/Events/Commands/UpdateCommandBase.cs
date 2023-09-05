using MediatR;
using Profio.Domain.Interfaces;
using Profio.Domain.Models;

namespace Profio.Application.CQRS.Events.Commands;

public record UpdateCommandBase(object Id) : IRequest<ResultModel<object>>, ITxRequest;
