using Microsoft.AspNetCore.Mvc;
using ShoppingCart_webApp.AppCode.Interface;
using ShoppingCart_webApp.Models;
using System.Diagnostics;

namespace ShoppingCart_webApp.Controllers
{
    public class HomeController : Controller
    {

        private IHR HRML { get; set; }
        public HomeController(IHR HRML)
        {
            this.HRML = HRML;

        }
        // Get Category--------
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PrtCategoryList()
        {
            try
            {
                List<Master_Category> CategoryLists = HRML.GetCategory();
                return PartialView(CategoryLists);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        // Insert and Update-------

        [HttpPost]
        public ActionResult AddorUpdateCategory(Master_Category masterCategory)
        {
            try
            {
                Response response = new Response();
                response = HRML.AddOrUpdateCategory(masterCategory);
                return Json(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //Update Category---------
        public ActionResult UpdateCategory(int categoryId)
        {
            try
            {
                var data = HRML.CategoryUpdateById(categoryId);
                return Json(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //Delete Category---------
        public ActionResult DeleteCategory(int itemId)
        {
            try
            {
                var data = HRML.DeleteCategoryById(itemId);
                return Json(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //Product---------
        public IActionResult ProductPage()
        {
            List<Master_Product> masterProducts = HRML.GetProductList();
            return View(masterProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
