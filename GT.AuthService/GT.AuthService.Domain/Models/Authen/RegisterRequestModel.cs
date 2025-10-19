﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.AuthService.Domain.Models.Authen
{
    public class RegisterRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string? Email { get; set; }
        public string? BankAccount { get; set; }
        public string? BankName { get; set; }
        public string Role { get; set; }
    }
}
