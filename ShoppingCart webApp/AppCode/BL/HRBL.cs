using Microsoft.Data.SqlClient;
using ShoppingCart_webApp.Models;
using System.Data;
namespace ShoppingCart_webApp.AppCode.BL
{
    public class HRBL
    {
        private readonly string _connectionString;
        public HRBL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Master_Category> GetCategory()
        {
            List<Master_Category> CategoryLists = new List<Master_Category>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                SqlCommand command = new SqlCommand("Proc_GetMasterCategoryList", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Master_Category Category = new Master_Category
                    {
                        _Id = Convert.ToInt32(reader["_Id"]),
                        _CategoryName = reader["_CategoryName"].ToString() ?? "",
                        _IsActive = Convert.ToBoolean(reader["_IsActive"]),
                        _EntryDate = Convert.ToDateTime(reader["_EntryDate"])

                    };
                    CategoryLists.Add(Category);
                }				
			}
            return CategoryLists;
        }

		public object ExecuteDML(string proc, SqlParameter[] parameters)
		{
			object var = null; // Declare the object variable outside the using block

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand(proc, connection);
				cmd.CommandType = CommandType.StoredProcedure;
				foreach (SqlParameter param in parameters)
				{
					cmd.Parameters.Add(param);
				}
				connection.Open();
				var = cmd.ExecuteNonQuery(); // Assign the result to the object variable
				connection.Close();
			}

			return var;
		}

		public DataTable SelectForEdit(string proc, SqlParameter[] parameters)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand(proc, connection);
				cmd.CommandType = CommandType.StoredProcedure;
				foreach (SqlParameter param in parameters)
				{
					if (param.Value != null)
						cmd.Parameters.Add(param);
				}
				SqlDataAdapter sda = new SqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				sda.Fill(dt);
				return dt;
			}

		}


		public List<Master_Product> GetProductList()
		{
			List<Master_Product> masterProducts = new List<Master_Product>();
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{

				SqlCommand command = new SqlCommand("Proc_GetProductList", connection);
				command.CommandType = CommandType.StoredProcedure;
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					Master_Product product = new Master_Product
					{
						_Id = Convert.ToInt32(reader["_Id"]),
						_ProductName = reader["_ProductName"].ToString() ?? "",
						_ProductPrice = Convert.ToDecimal(reader["_ProductPrice"]),
						_ProductDesc = reader["_ProductDesc"].ToString() ?? "",
						_CategoryId = Convert.ToInt32(reader["_CategoryId"]),
						_IsActive = Convert.ToBoolean(reader["_IsActive"]),
						_EntryDate = Convert.ToDateTime(reader["_EntryDate"]),
						_ProductImage = reader["_ProductImage"].ToString() ?? "",
						_CategoryName = reader["_CategoryName"].ToString() ?? "",


					};
					masterProducts.Add(product);
				}
			}
			return masterProducts;
		}


		// Response-----Add and update
		public Response Execute(string proc, SqlParameter[] parameters)
		{
			Response response = new Response();
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand(proc, connection);
				cmd.CommandType = CommandType.StoredProcedure;
				foreach (SqlParameter param in parameters)
				{
					if (param.Value != null)
						cmd.Parameters.Add(param);
				}
				connection.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					// Read the first row (assuming the stored procedure returns one row)
					reader.Read();

					// Retrieve status code and message columns
					int statusCodeIndex = reader.GetOrdinal("StatusCode");
					int messageIndex = reader.GetOrdinal("Message");

					// Retrieve values
					int statusCode = reader.GetInt32(statusCodeIndex);
					string message = reader.GetString(messageIndex);

					// Set the values in the response object
					response.StatusCode = statusCode;
					response.Message = message;
				}

				reader.Close();
				connection.Close();
			}

			return response;
		}


	}
}
