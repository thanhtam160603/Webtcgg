using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webtcgg.Models;
using Webtcgg.Models.EF;

namespace Webtcgg.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/News
        public ActionResult Index(string SearchText, int? page)
        {
            var pageSize = 5;
            if(page == null)
            {
                page = 1;
            }
            IEnumerable<News> items = db.News.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
               items = items.Where(x => x.Alias.Contains(SearchText) || x.Title.Contains(SearchText)).ToList();
            }
            var pageIndex = page.HasValue?Convert.ToInt32(page):1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Add() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News model) 
        {
            if (ModelState.IsValid) {
                model.CreateDate = DateTime.Now;
                model.CategoryID = 9;
                model.ModifiedDate = DateTime.Now;
                model.Alias = Webtcgg.Models.Common.Filter.FilterChar(model.Title);
                db.News.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id) 
        { 
            var item = db.News.Find(id);
            return View(item);  
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News model)
        {
            if (ModelState.IsValid)
            {
                db.News.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.Alias = Webtcgg.Models.Common.Filter.FilterChar(model.Title);
                db.Entry(model).Property(x => x.Title).IsModified = true;
                db.Entry(model).Property(x => x.Detail).IsModified = true;
                db.Entry(model).Property(x => x.Image).IsModified = true;
                db.Entry(model).Property(x => x.Description).IsModified = true;
                db.Entry(model).Property(x => x.Alias).IsModified = true;
                db.Entry(model).Property(x => x.SeoDescripition).IsModified = true;
                db.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
                db.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                db.Entry(model).Property(x => x.ModifiedDate).IsModified = true;
                db.Entry(model).Property(x => x.ModifierBy).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                //var DeleteItem = db.Categories.Attach(item);
                db.News.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if(items!=null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.News.Find(Convert.ToInt64(item));
                        db.News.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}