using MyApp.Web.Abstraction;
using MyApp.Web.Infra.Data;

namespace MyApp.Web.Models
{
    public class PostViewModel: BlogEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int PostView { get; set; }
        public HeadingTag HeadingTag { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
