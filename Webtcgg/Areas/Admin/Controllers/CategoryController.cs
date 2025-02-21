using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Webtcgg.Models;
using Webtcgg.Models.EF;

namespace Webtcgg.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index(int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var items = db.Categories.OrderByDescending(x => x.Id).ToPagedList(pageIndex, pageSize);
            return View(items);
        }
        public ActionResult Add() { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            if (ModelState.IsValid) {
                model.CreateDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = Webtcgg.Models.Common.Filter.FilterChar(model.Title);
                db.Categories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);

        }
        public ActionResult Edit(int Id) 
        {
            var item = db.Categories.Find(Id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.Alias = Webtcgg.Models.Common.Filter.FilterChar(model.Title);
                db.Entry(model).Property(x=>x.Title).IsModified = true;
                db.Entry(model).Property(x => x.Description).IsModified = true;
                db.Entry(model).Property(x => x.Alias).IsModified = true;
                db.Entry(model).Property(x => x.SeoDescripition).IsModified = true;
                db.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
                db.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                db.Entry(model).Property(x => x.Position).IsModified = true;
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
            var item = db.Categories.Find(id);
            if (item != null)
            {
                //var DeleteItem = db.Categories.Attach(item);
                db.Categories.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}