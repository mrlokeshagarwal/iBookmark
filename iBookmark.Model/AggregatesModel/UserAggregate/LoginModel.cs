﻿namespace iBookmark.Model.AggregatesModel.UserAggregate
{
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
