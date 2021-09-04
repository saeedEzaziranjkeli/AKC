using System;
using System.Collections.Generic;
using System.Text;
using AK.Domain.Models;

namespace AK.Application.DTOs
{
    public class UserDto
    {
        public UserDto()
        {
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public DateTime Birthday { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] PasswordByte { get; set; }
    }
}
