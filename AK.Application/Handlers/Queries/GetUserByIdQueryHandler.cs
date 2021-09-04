using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using AK.Application.DTOs;
using AK.Application.Queries;
using AK.Domain.Interfaces;
using AK.Domain.Models;

namespace AK.Application.Handlers.Queries
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
            var model = _mapper.Map<User>(request);
            var result = await _repo.GetByIdGu(model.Id);
            var modelDto = _mapper.Map<UserDto>(result);
            return modelDto;
        }
    }
}
