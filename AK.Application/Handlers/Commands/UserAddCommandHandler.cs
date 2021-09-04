﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using AK.Application.Commands.User;
using AK.Application.DTOs;
using AK.Domain.Interfaces;
using AK.Domain.Models;

namespace AK.Application.Handlers.Commands
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
            var model = _mapper.Map<User>(request);
            var result = await _repo.AddAsync(model);
            return _mapper.Map<UserDto>(result);
        }
    }
}
