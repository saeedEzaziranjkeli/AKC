using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AK.Application.DTOs;

namespace AK.Application.Commands.User
{
    public class UserAddCommand : UserCommandBase
    {
        public UserAddCommand() : base()
        {
            
        }
        public UserAddCommand(UserDto model) : base(model)
        {
        }
    }
}
