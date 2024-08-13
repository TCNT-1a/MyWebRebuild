﻿//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using MyApp.Web.Abstraction;

namespace MyApp.Web.Infra.Data
{
    [Table("User")]
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [Required]
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        [DefaultValue("user")]
        public string Role { get; set; }
    }
}
