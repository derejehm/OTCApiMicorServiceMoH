﻿namespace MoH_Microservice.Models.Form
{
    public class ChangePassword
    {
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
