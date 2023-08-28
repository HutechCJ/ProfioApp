using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profio.Application.CQRS.Models;
using Profio.Infrastructure.Identity;

namespace Profio.Application.Users.Commands.Register;

public record RegisterCommand(string UserName, string Email, string Password, string FullName) : IRequest<AccountDTO>;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResultModel<AccountDTO>>
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IMapper _mapper;

  public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper) => (_userManager, _mapper) = (userManager, mapper);

  public async Task<ResultModel<AccountDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken)
  {
    return ResultModel<AccountDTO>.Create(_mapper.Map<AccountDTO>(user));
  }
}
