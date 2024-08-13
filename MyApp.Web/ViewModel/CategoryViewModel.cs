using MyApp.Web.Abstraction;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.ViewModel
{
    public class CategoryViewModel
    {
        [Required (ErrorMessage ="Field này bắt buột")]
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool isPublished { get; set; }
    }
}
