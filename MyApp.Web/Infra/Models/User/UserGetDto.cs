﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Web.Infra.Models.User
{
    public class UserGetDto
    {
        public int Id { get; set; } 
        public string FullName { get; set; }

        public string UserName { get; set; }
        
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }
    }
}
