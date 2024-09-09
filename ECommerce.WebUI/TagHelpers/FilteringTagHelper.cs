using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace ECommerce.WebUI.TagHelpers
{
    [HtmlTargetElement("filtering-data")]
    public class FilteringTagHelper : TagHelper
    {
        [HtmlAttributeName("current-category")]
        public int CurrentCategory { get; set; }
        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }
        //[HtmlAttributeName("filterByName")]
        public string FilterByName { get; set; } = "a-z";
        public string NameFilter { get; set; } = "A-Z";
        //[HtmlAttributeName("filterByPrice")]
        public string FilterByPrice { get; set; } = "higher to lower";
        public string PriceFilter { get; set; } = "Higher To Lower";

        [HtmlAttributeName("orderByPrices")]
        public string? OrderByPrice { get; set; }


        [HtmlAttributeName("controllerName")]
        public string ControllerName { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            output.TagName = "section";
            var sb = new StringBuilder();

            //if(FilterByName == "a-z")
            //{
            //    FilterByName = "z-a";
            //    NameFilter = "Z-A";
            //}
            //if (FilterByName == "Z-A")
            //{
            //    FilterByName = "a-z";
            //    NameFilter = "A-Z";
            //}

            sb.Append("<ul class='pagination'>");
            //sb.AppendFormat("<li class='{0}'>", (i == CurrentPage) ? "page-item active" : "page-item");
            sb.Append("<li class='page-item'>");
            sb.AppendFormat("<a class='page-link' href='/{0}/index?page={1}&category={2}&filterByName={3}'>{4}</a>",
                                                                   ControllerName,CurrentPage, 
                                                                   CurrentCategory, FilterByName,NameFilter);
            sb.Append("</li>");
            sb.Append("<li class='page-item'>");
            sb.AppendFormat("<a class='page-link' href='/{0}/index?page={1}&category={2}&filterByPrice={3}'>{4}</a>",
                                                                ControllerName,CurrentPage, 
                                                                CurrentCategory,OrderByPrice,OrderByPrice);
            sb.Append("</li>");
            sb.Append("</ul>");


            //if (FilterByName == "a-z")
            //{
            //    FilterByName = "z-a";
            //    NameFilter = "Z-A";
            //}
            //else if (FilterByName == "z-a")
            //{
            //    FilterByName = "a-z";
            //    NameFilter = "A-Z";
            //}
            output.Content.SetHtmlContent(sb.ToString());
        }

    }
}
