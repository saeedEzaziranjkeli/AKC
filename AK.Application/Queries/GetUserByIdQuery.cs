using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AK.Application.DTOs;

namespace AK.Application.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public Guid Id { get; set; }
        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
