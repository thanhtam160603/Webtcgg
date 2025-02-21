using PagedList;
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
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        private ApplicationDbContext Db = new ApplicationDbContext();
        public ActionResult Index(int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Product> items = Db.Products.OrderByDescending(x => x.Id);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Add () {
            ViewBag.ProductCategory = new SelectList(Db.ProductCategories.ToList(), "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images,List<int> rDefault)
        {
            if (ModelState.IsValid) 
            {
                if(Images!=null && Images.Count > 0)
                {
                    for(int i = 0;i<Images.Count; i++)
                    {
                        if (i + 1 == rDefault[0])
                        {
                            
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = true,
                            });
                        }
                        else {
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false,
                            });
                        }
                    }
                }
                model.CreateDate = DateTime.Now;              
                model.ModifiedDate = DateTime.Now;
                if (string.IsNullOrEmpty(model.SeoTitle))
                {
                    model.SeoTitle = model.Title;
                }
                if(string.IsNullOrEmpty(model.Alias))
                    model.Alias = Webtcgg.Models.Common.Filter.FilterChar(model.Title);
                Db.Products.Add(model);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(Db.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }        
        public ActionResult Edit (int id) {
            ViewBag.ProductCategory = new SelectList(Db.ProductCategories.ToList(),"Id","Title");
            var item = Db.Products.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = Webtcgg.Models.Common.Filter.FilterChar(model.Title);
                Db.Products.Attach(model);
                Db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id) { 
         var item = Db.Products.Find(id);
            if (item != null) { 
            Db.Products.Remove(item);
                Db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = Db.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                Db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                return Json(new { success = true, isAcive = item.IsActive });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = Db.Products.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                Db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                return Json(new { success = true, IsHome = item.IsHome });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = Db.Products.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                Db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                return Json(new { success = true, IsSale = item.IsSale });
            }

            return Json(new { success = false });
        }
    }
}