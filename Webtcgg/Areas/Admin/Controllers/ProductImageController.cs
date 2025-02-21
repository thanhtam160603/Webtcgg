using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webtcgg.Models;
using Webtcgg.Models.EF;

namespace Webtcgg.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductImage
        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            var items = db.ProductImage.Where(x => x.ProductId == id).ToList();
            return View(items);
        }

        [HttpPost]
        public ActionResult AddImage(int productId, string url)
        {
            db.ProductImage.Add(new ProductImage
            {
                ProductId = productId,
                Image = url,
                IsDefault = false
            });
            db.SaveChanges();
            return Json(new { Success = true });
        }
        [HttpPost]
        public ActionResult Delete(int id) {
            var item = db.ProductImage.Find(id);
            db.ProductImage.Remove(item);
            db.SaveChanges();
            return Json("Index");
         }

    }
}