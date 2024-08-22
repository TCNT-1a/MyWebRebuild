using Markdig;

namespace MyApp.Web.Core.Service
{
    public interface IMarkdownService
    {
        string ConvertToHtml(string markdown);
    }

    public class MarkdownService : IMarkdownService
    {
        public string ConvertToHtml(string markdown)
        {
            //var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions() // Sử dụng các phần mở rộng nâng cao
            .UsePipeTables()         // Hỗ trợ bảng với ký tự |
            .UseEmphasisExtras()     // Hỗ trợ các ký tự nhấn mạnh bổ sung
            .Build();
            return Markdown.ToHtml(markdown, pipeline);
        }
    }
}
