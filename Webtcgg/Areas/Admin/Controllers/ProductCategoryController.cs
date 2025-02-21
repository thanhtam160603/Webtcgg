using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Webtcgg.Models;
using Webtcgg.Models.EF;

namespace Webtcgg.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            var items = db.ProductCategories;
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory model)
        {
            if(ModelState.IsValid)
            {
                model.CreateDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = Webtcgg.Models.Common.Filter.FilterChar(model.Title);
                db.ProductCategories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = db.ProductCategories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                db.ProductCategories.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.Alias = Webtcgg.Models.Common.Filter.FilterChar(model.Title);
                db.Entry(model).Property(x => x.Title).IsModified = true;                
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
            var item = db.ProductCategories.Find(id);
            if (item != null)
            {
                //var DeleteItem = db.Categories.Attach(item);
                db.ProductCategories.Remove(item);
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
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.ProductCategories.Find(Convert.ToInt64(item));
                        db.ProductCategories.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}