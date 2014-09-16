
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Routing;
using DealerBlog.Entity;
using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MvcPaging;

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
  public static class PagerOptionsBuilderExtensions
  {
      public static PagerOptionsBuilder AddFromQueryString(
          this PagerOptionsBuilder builder,
          HttpRequestBase request
      )
      {
          foreach (string item in request.QueryString)
          {
              if (item != "page")
              {
                  builder.AddRouteValue(item, request.QueryString[item]);
              }
          }
          return builder;
      }
  }
  public static class ViewContextExtensions
  {

      /// <summary>

      /// Builds a RouteValueDictionary that combines the request route values, the querystring parameters,

      /// and the passed newRouteValues. Values from newRouteValues override request route values and querystring

      /// parameters having the same key.

      /// </summary>

      public static RouteValueDictionary GetCombinedRouteValues(this ViewContext viewContext, object newRouteValues)
      {

          RouteValueDictionary combinedRouteValues = new RouteValueDictionary(viewContext.RouteData.Values);



          NameValueCollection queryString = viewContext.RequestContext.HttpContext.Request.QueryString;

          foreach (string key in queryString.AllKeys.Where(key => key != null))

              combinedRouteValues[key] = queryString[key];



          if (newRouteValues != null)
          {

              foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(newRouteValues))

                  combinedRouteValues[descriptor.Name] = descriptor.GetValue(newRouteValues);

          }



          return combinedRouteValues;

      }

  }

}