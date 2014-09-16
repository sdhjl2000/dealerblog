﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using DealerBlog.BLL;
using MvcPaging;
using DealerBlog.Entity;
namespace DealerBlog.Web.Web.Controllers
{

    public partial class BlogController : Controller
    {
        private readonly IPost _postbll;
        private readonly ICategory _categorybll;
        private const int pagesize = 5;

        public BlogController(IPost ipostbll, ICategory iCategory)
        {
            _postbll = ipostbll;
            _categorybll = iCategory;
        }

        //
        // GET: /Blog/
        public virtual ActionResult Index(int page = 1)
        {

            var list = _postbll.Posts(page, pagesize);
            var postcount = _postbll.TotalPosts();


            return View(new PagedList<Post>(list, page - 1, pagesize, postcount));
        }

        public virtual ActionResult Category(string category, int page = 1)
        {
            var list = _postbll.PostsForCategory(category, page, pagesize);
            var postcount = _postbll.TotalPostsForCategory(category);
            IEnumerable<SelectListItem> items = _categorybll.Find(x => x.CategoryId > 0).Select(c => new SelectListItem
            {
                Value = c.CatUrlSlug,
                Text = c.Name,
                Selected = c.CatUrlSlug == category
            });

            ViewBag.CatList = items;
            return View(new PagedList<Post>(list, page - 1, pagesize, postcount));

        }

        public virtual ActionResult Tag(string tag, int page = 1)
        {
            var list = _postbll.PostsForTag(tag, page, pagesize);
            var postcount = _postbll.TotalPostsForTag(tag);

            return View(new PagedList<Post>(list, page - 1, pagesize, postcount));

        }

        public virtual ActionResult Search(string s, int page = 1)
        {
            var list = _postbll.PostsForSearch(s, page, pagesize);
            var postcount = _postbll.TotalPostsForSearch(s);

            return View(new PagedList<Post>(list, page - 1, pagesize, postcount));

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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
