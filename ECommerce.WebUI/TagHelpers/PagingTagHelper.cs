using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Security.Principal;
using System.Text;

namespace ECommerce.WebUI.TagHelpers
{
    [HtmlTargetElement("product-list-pager")]
    public class PagingTagHelper : TagHelper
    {
        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; }
        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }
        [HtmlAttributeName("current-category")]
        public int CurrentCategory { get; set; }
        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }

        [HtmlAttributeName("controllerName")]
        public string ControllerName { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            output.TagName = "section";
            var sb = new StringBuilder();
            if (PageCount > 1)
            {
                sb.Append("<ul class='pagination'>");

                if (CurrentPage > 1)
                {
                    sb.Append("<li class='page-item'>");
                    sb.AppendFormat("<a class='page-link' href='/{0}/index?page={1}&category={2}'>Previous</a>",
                                                      ControllerName,CurrentPage - 1, CurrentCategory);
                    sb.Append("</li>");
                }
                for (int i = 1; i <= PageCount; i++)
                {
                    sb.AppendFormat("<li class='{0}'>", (i == CurrentPage) ? "page-item active" : "page-item");
                    sb.AppendFormat("<a class='page-link' href='/{0}/index?page={1}&category={2}'>{3}</a>",
                                                                            ControllerName, i, CurrentCategory, i);
                    sb.Append("</li>");
                }

                if (CurrentPage < PageCount)
                {
                    sb.Append("<li class='page-item'>");
                    sb.AppendFormat("<a class='page-link' href='/{0}/index?page={1}&category={2}'>Next</a>",
                                                                 ControllerName,CurrentPage + 1, CurrentCategory);
                    sb.Append("</li>");
                }
                sb.Append("</ul>");
            }
            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
