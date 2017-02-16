using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SHUBooks
{
    public static class HtmlExtension
    {
        public static IHtmlString Image(this HtmlHelper helper, string name, string url)
        {

            return Image(helper, name, url, null);

        }



        public static IHtmlString Image(this HtmlHelper helper, string name, string url, object htmlAttributes)
        {

            var tagBuilder = new TagBuilder("img");

            tagBuilder.GenerateId(name);
            if (url == null)
            {
                url = "~/uploads/ads1.png";
            }
            tagBuilder.Attributes["src"] = new UrlHelper(helper.ViewContext.RequestContext).Content(url);
            //tagBuilder.MergeAttribute("src", url);
            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            //return tagBuilder.ToString();


            //var builder = new TagBuilder("img");

            // Create valid id
            // builder.GenerateId(name);

            // Add attributes
            //builder.MergeAttribute("src", url);
            // builder.MergeAttribute("alt", alternateText);
            //builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Render tag
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));

        }
    }
}