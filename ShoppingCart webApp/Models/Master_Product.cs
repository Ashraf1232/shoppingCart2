namespace ShoppingCart_webApp.Models
{
	public class Master_Product
	{
		public int _Id { get; set; }
		public string _ProductName { get; set; }
		public decimal _ProductPrice { get; set; }
		public string _ProductDesc { get; set; }
		public int _CategoryId { get; set; }
		public bool _IsActive { get; set; }
		public DateTime _EntryDate { get; set; }
		public string _ProductImage { get; set; }
		public string _CategoryName { get; set; }

	}
}
