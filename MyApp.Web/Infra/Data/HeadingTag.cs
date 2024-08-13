using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Web.Abstraction;

namespace MyApp.Web.Infra.Data
{
    public class HeadingTag : BaseEntity
    {
        public string Title { get; set; }
        public bool NoIndex { get; set; } 
        public string Canonical { get; set; }
        public string MetaDescription { get; set; }
    }
}
