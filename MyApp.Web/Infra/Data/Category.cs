using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using MyApp.Web.Abstraction;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyApp.Web.Infra.Data
{
    public class Category : BlogEntity
    {
        public string Name { get; set; }
        public HeadingTag HeadingTag { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}
