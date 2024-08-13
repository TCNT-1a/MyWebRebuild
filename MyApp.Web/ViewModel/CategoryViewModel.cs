using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.ViewModel
{
    public class CategoryViewModel
    {
        [Required (ErrorMessage ="Field này bắt buột")]
        public string Name { get; set; }
    }
}
