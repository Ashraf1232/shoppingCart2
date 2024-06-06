using ShoppingCart_webApp.Models;

namespace ShoppingCart_webApp.AppCode.Interface
{
    public interface IHR
    {
        public List<Master_Category> GetCategory();
		public Response AddOrUpdateCategory(Master_Category masterCategory);
		public Master_Category CategoryUpdateById(int categoryId);
		public object DeleteCategoryById(int itemId);
		public List<Master_Product> GetProductList();
	}
}
