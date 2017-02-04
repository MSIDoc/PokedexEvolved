using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Pokedex.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    //[HtmlTargetElement("tag-name")]
    public class PagerTagHelper : TagHelper
    {

        public int StartPage = 1;        
        public int TotalPages { get; set; }
        public bool HasNextPage = true;
        public bool HasPreviousPage = false;
        public bool AddActiveClassToActive = true;


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.AppendHtml(@"<ul class=""pagination"">");

            if (HasPreviousPage)
                output.Content.AppendHtml(@"<li><a href=""#""></li>");

            for (int i = StartPage; i <= TotalPages; i++)
            {
                output.Content.AppendHtml($@"<li><a href=""#"">{i}</a></li>");
            }

            if (HasNextPage)
                output.Content.AppendHtml(@"<li><a href=""#""></li>");

            output.Content.AppendHtml(@"</ul>");
        }
    }
}
