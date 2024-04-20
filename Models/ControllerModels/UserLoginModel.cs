﻿using System.ComponentModel.DataAnnotations;

namespace Models.ControllerModels
{
    public class UserLoginModel
    {
        public string? Email { get; set; } = null;
        public string? Password { get; set; } = null;
    }
}
