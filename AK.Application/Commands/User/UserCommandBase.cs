using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AK.Application.DTOs;

namespace AK.Application.Commands.User
{
    public class UserCommandBase : IRequest<UserDto>
    {
        public string Name { get; set; }
        public string Firstname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserCommandBase(UserDto model)
        {
            Name = model.Name;
            Firstname = model.Firstname;
            Username = model.Username;
            Password = model.Password;
        }

        public UserCommandBase()
        {
            
        }
    }
}
