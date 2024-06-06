
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart_webApp.AppCode.Interface;
using ShoppingCart_webApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShoppingCart_webApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApIController : ControllerBase
    {
        private IHR HRML { get; set; }
        public ApIController(IHR HRML)
        {
            this.HRML = HRML;

        }

        // Get Category--------
        // https://localhost:7056/api/ApI/GetCategory
        [HttpGet]
        public IActionResult GetCategory()
        {
            List<Master_Category> CategoryLists = new List<Master_Category>();

            try
            {
                CategoryLists = HRML.GetCategory();
                return Ok(CategoryLists);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok(CategoryLists);
            }
        }


        //https://localhost:7056/api/ApI/UpdateCategory?categoryId=2
        [HttpGet]
        public ActionResult UpdateCategory(int categoryId)
        {
            try
            {
                var data = HRML.CategoryUpdateById(categoryId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok();
            }
        }


        //https://localhost:7056/api/ApI/AddorUpdateCategory
        [HttpPost]
        public ActionResult AddorUpdateCategory(Master_Category masterCategory)
        {
            try
            {
                Response response = new Response();
                response = HRML.AddOrUpdateCategory(masterCategory);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok();
            }
        }

        //https://localhost:7056/api/ApI/DeleteCategory?itemId=3002
        [HttpPost]
        public ActionResult DeleteCategory(int itemId)
        {
            try
            {
                var data = HRML.DeleteCategoryById(itemId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok();
           
            }
        }
    }
}
