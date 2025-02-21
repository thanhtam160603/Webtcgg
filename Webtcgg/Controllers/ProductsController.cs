using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webtcgg.Models;

namespace Webtcgg.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Partial_ItemsByCateId() {
            var items = db.Products.Where(x => x.IsHome).Take(12).ToList();
            return PartialView(items);
        }
    }
}