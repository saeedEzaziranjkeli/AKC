using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using AK.Application.DTOs;
using AK.Application.Queries.User;
using AK.Domain.Interfaces;

namespace AK.Application.Handlers.Queries.User
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repo;
        public GetUserByIdQueryHandler(IMapper mapper, IUserRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Domain.Models.User>(request);
            var result = await _repo.GetByIdGu(model.Id);
            var modelDto = _mapper.Map<UserDto>(result);
            return modelDto;
        }
    }
}
