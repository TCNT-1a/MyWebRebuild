//using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyApp.Web.Abstraction;

namespace MyApp.Web.Infra.Data
{
    [Table("Blog")]
    public class Blog: BaseEntity
    {
        public string Url { get; set; }
        public string Content { get; set; }
    }
}
