using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealerBlog.BLL;
using DealerBlog.Web.Web.Models;

namespace DealerBlog.Web.Web.Controllers
{

    public partial class BlogController : Controller
    {
        private readonly IPost _postbll;


        public BlogController(IPost ipostbll)
        {
            _postbll = ipostbll;
        }

        //
        // GET: /Blog/
        public virtual ActionResult Index(int page = 1)
        {
            var list= _postbll.Posts(page, 5);
            var postcount = _postbll.TotalPosts();
            
            var postvm = new PostVm() { Posts = list, TotalPosts=postcount,PageSize = 5, PageNumer = page};
            return View(postvm);
        }

        public virtual ActionResult Category(string category, int page = 1)
        {
            var list = _postbll.PostsForCategory(category, page, 5);
            var postcount = _postbll.TotalPostsForCategory(category);

            var postvm = new PostVm() { Posts = list, TotalPosts = postcount, PageSize = 5, PageNumer = page };
            return View(postvm);
        }

        public virtual ActionResult Tag(string tag,int page = 1)
        {
            var list = _postbll.PostsForTag(tag,page, 5);
            var postcount = _postbll.TotalPostsForTag(tag);

            var postvm = new PostVm() { Posts = list, TotalPosts = postcount, PageSize = 5, PageNumer = page };
            return View(postvm);
        }

        //
        // GET: /Blog/Details/5
        public virtual ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Blog/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Blog/Create
        [HttpPost]
        public virtual ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Blog/Edit/5
        public virtual ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Blog/Edit/5
        [HttpPost]
        public virtual ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Blog/Delete/5
        public virtual ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Blog/Delete/5
        [HttpPost]
        public virtual ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
