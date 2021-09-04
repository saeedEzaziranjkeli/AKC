using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AK.Application.Commands.User;
using AK.Application.DTOs;

namespace AK.Application.Commands.User
{
    public class UserRegisterCommand : UserCommandBase
    {
        public UserRegisterCommand(UserDto model) : base(model)
        {
        }
    }
}
