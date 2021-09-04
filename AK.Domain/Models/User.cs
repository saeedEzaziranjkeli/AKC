using System;
using System.Collections.Generic;
using System.Text;

namespace AK.Domain.Models
{
    public class User
    {
        public User()
        {
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public DateTime Birthday { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
