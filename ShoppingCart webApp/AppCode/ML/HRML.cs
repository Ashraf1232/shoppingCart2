using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ShoppingCart_webApp.AppCode.BL;
using ShoppingCart_webApp.AppCode.Interface;
using ShoppingCart_webApp.Models;
using System.Data;

namespace ShoppingCart_webApp.AppCode.ML
{
	public class HRML : IHR
	{
		private readonly HRBL BL;
		private readonly IWebHostEnvironment _webHostEnvironment;


		public HRML(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
			BL = new HRBL(configuration);
		}

		public List<Master_Category> GetCategory()
		{
			List<Master_Category> dl = BL.GetCategory();
			return dl;
		}

		//Add Or Update Category 
		public Response AddOrUpdateCategory(Master_Category masterCategory)
		{
			Response response = new Response();
			SqlParameter[] parameters =
				{
				new SqlParameter("@CategoryId", SqlDbType.Int) {Value = masterCategory._Id},
				new SqlParameter("@CategoryName", SqlDbType.VarChar, 50) {Value = masterCategory._CategoryName},
				new SqlParameter("@IsActive", SqlDbType.Bit) {Value = masterCategory._IsActive},
				};

			object result = BL.Execute("Proc_AddOrUpdateCategory", parameters);
			if (result is Response)
			{
				response = (Response)result;
			}

			return response;

		}

		// Update Record

		public Master_Category CategoryUpdateById(int categoryId)
		{
			DataTable dt = BL.SelectForEdit("Proc_GetCategoryToUpdate", new SqlParameter[] {
				new SqlParameter("@Id", categoryId),
			});

			if (dt.Rows.Count > 0)
			{
				return new Master_Category()
				{
					_Id = Convert.ToInt32(dt.Rows[0][0]),
					_CategoryName = dt.Rows[0][1].ToString(),
					_IsActive = Convert.ToBoolean(dt.Rows[0][2]),


				};
			}
			else
			{
				return null; // or throw an exception, depending on your requirements
			}
		}

		//Delete Category
		public object DeleteCategoryById(int itemId)
		{
			object r = BL.ExecuteDML("Proc_DeleteCategoryById", new SqlParameter[] {
				new SqlParameter("@Id", itemId),
			});
			return r;
		}

		//product Add

		public List<Master_Product> GetProductList()
		{
			List<Master_Product> masterProducts = BL.GetProductList();
			return masterProducts;
		}
	}
}
