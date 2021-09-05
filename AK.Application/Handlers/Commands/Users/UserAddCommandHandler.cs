using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using AK.Application.Commands.User;
using AK.Application.DTOs;
using AK.Domain.Interfaces;

namespace AK.Application.Handlers.Commands.User
{
    public class UserAddCommandHandler : IRequestHandler<UserAddCommand, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repo;

        public UserAddCommandHandler(IMapper mapper, IUserRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<UserDto> Handle(UserAddCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return null;
            var model = _mapper.Map<Domain.Models.User>(request);
            var result = await _repo.AddAsync(model);
            return _mapper.Map<UserDto>(result);
        }
    }
}
