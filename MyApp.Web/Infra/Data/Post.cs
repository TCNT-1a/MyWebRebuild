using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Web.Abstraction;

namespace MyApp.Web.Infra.Data
{
    public class Post: BaseEntity
    {
        [Required(ErrorMessage ="The field is required")]
        public string Title { get; set; }
        //[Required(ErrorMessage = "The field is required")]
        //[DefaultValue (0)]
        public int? PostView { get; set; }
        [Required(ErrorMessage = "The field is required")]
        public string Content { get; set; }
        public virtual User? Author { get; set; }
        public virtual List<Tag>? Tags { get; set; }
        public virtual Category? Category { get; set; }
        public virtual HeadingTag? HeadingTag { get; set; }
    }
}
