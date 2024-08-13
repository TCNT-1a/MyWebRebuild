using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Web.Abstraction;

namespace MyApp.Web.Infra.Data
{
    public class Tag: BlogEntity
    {
        public string Name { get; set; }
        public HeadingTag HeadingTag { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}
