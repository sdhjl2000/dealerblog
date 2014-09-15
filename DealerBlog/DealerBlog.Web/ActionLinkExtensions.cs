
using System.Collections.Specialized;
using System.Linq;
using System.Web.Routing;
using DealerBlog.Entity;
using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DealerBlog.Web.Web
{
  public static class ActionLinkExtensions
  {
    public static MvcHtmlString PostLink(this HtmlHelper helper, Post post)
    {
      return helper.ActionLink(post.Title, "Index", "Blog", new { year = post.PostedOn.Year, month = post.PostedOn.Month, title = post.UrlSlug }, new { title = post.Title });
    }

    public static MvcHtmlString CategoryLink(this HtmlHelper helper, Category category)
    {
        if (category != null)
        {
            return helper.ActionLink(category.Name, "Category", "Blog", new {category = category.CatUrlSlug},
                new {title = String.Format("See all posts in {0}", category.Name)});
        }
        return null;
    }

    public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag)
    {
      return helper.ActionLink(tag.TagName, "Tag", "Blog", new { tag = tag.TagUrlSlug }, new { title = String.Format("See all posts in {0}", tag.TagName) });
    }

    private static RouteValueDictionary CreateRouteToCurrentPage(this HtmlHelper html)
    {
        var routeValues= new RouteValueDictionary(html.ViewContext.RouteData.Values);

        NameValueCollection queryString
             = html.ViewContext.HttpContext.Request.QueryString;

        foreach (string key in queryString.Cast<string>())
        {
            routeValues[key] = queryString[key];
        }

        return routeValues;
    }
  }
}