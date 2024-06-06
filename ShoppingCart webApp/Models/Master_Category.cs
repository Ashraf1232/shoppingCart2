namespace ShoppingCart_webApp.Models
{
    public class Master_Category
    {
        public int _Id { get; set; }
        public string _CategoryName { get; set; }
        public bool _IsActive { get; set; }
        public DateTime _EntryDate { get; set; }
    }

	public class Response
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
	}
}


