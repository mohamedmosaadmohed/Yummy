using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using SOfCO.Helpers;
using YummyApi.Models;
using Blazor.Shared.Models;

namespace Blazor.Shared.Services
{
    public class ProductServices:IProductServices
	{
		public readonly GetConnectionDB _db;
		public ProductServices()
		{
			_db = new GetConnectionDB();
		}
        public async Task AddNewUserRegister(RegisterationUser registerationUser)
        {
            var EncreptionPassword = PasswordHelper.EncryptPassword(registerationUser.Password);
            SqlCommand cmd = new SqlCommand("SP_Registeration");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", registerationUser.Name);
            cmd.Parameters.AddWithValue("@Email", registerationUser.Email);
            cmd.Parameters.AddWithValue("@Password", EncreptionPassword);
            cmd.Parameters.AddWithValue("@AgreeAllStatement", registerationUser.AgreeAllStatement);
            var sql = _db.GetDatabase();
            await sql.ExecuteNonQueryAsync(cmd);
        }
        public async Task<bool> CheckEmailExistOrNot(String Email)
        {
            SqlCommand cmd = new SqlCommand("SP_CheckEmailExist");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", Email);
            SqlParameter outputParameter = new SqlParameter();
            outputParameter.ParameterName = "@EmailExists";
            outputParameter.SqlDbType = SqlDbType.Bit;
            outputParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outputParameter);
            var sql = _db.GetDatabase();
            await sql.ExecuteNonQueryAsync(cmd);
            bool emailExists = (bool)outputParameter.Value;
            return emailExists;
        }
        public async Task<YummyApi.Models.LoginModel> GETLoginForUser(string Email)
        {
            string emailInDataBase="";
            string PasswordInDataBase="";
            string Role="";
            string FullName="";
            int Id;
            SqlCommand cmd = new SqlCommand("SP_CheckLogin");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", Email);
            var sql = _db.GetDatabase();
            var result= await sql.ExecuteQueryAsync(cmd);
            foreach( DataRow row in result.Rows)
            {
                Id= (int)row["ID"];
                emailInDataBase= row["Email"] as string ?? "";
                PasswordInDataBase= row["Password"] as string ?? "";
                FullName = row["Name"] as string ?? "";
                Role = row["Role"] as string ?? "";
               var loginModel = new LoginModel
                {
                    Email = emailInDataBase,
                    Password = PasswordInDataBase,
                    FullName= FullName,
                    Role = Role,
                    User_Id = Id
               };
                return loginModel;
            }
            return null;
        }
        public async Task AddNewProduct(Product product)
        {
            SqlCommand cmd = new SqlCommand("SP_Product");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("@Image", product.ImageData);
            cmd.Parameters.AddWithValue("@Category_name", product.Category);
            var sql = _db.GetDatabase();
            await sql.ExecuteNonQueryAsync(cmd);
        }
        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            SqlCommand cmd = new SqlCommand("SP_GetAllProduct");
            cmd.CommandType = CommandType.StoredProcedure;
            var sql = _db.GetDatabase();
            var result = await sql.ExecuteQueryAsync(cmd);
            List<Product> listOfProduct = new List<Product>();

            foreach (DataRow row in result.Rows)
            {
                var product = new Product();
                product.ProductID = Convert.ToInt32(row["Id_Product"]);
                product.Name = row["Name"] as string ?? "";
                product.Price = row["Price"] as string ?? "";
                product.Description = row["Description"] as string ?? "";
                product.ImageData = (byte[])row["Image"];
                product.Category = row["Category_name"] as string ?? "";
                listOfProduct.Add(product);
            }
            return listOfProduct;
        }
        public async Task DeleteProduct(int? stdId)
        {
            SqlCommand cmd = new SqlCommand("SP_DeleteProduct");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", stdId);
            var sql = _db.GetDatabase();
            await sql.ExecuteNonQueryAsync(cmd);
        }
        public async Task UpdateProduct(Product product)
        {
            SqlCommand cmd = new SqlCommand("SP_UpdateProduct");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@product_Id", product.ProductID);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("@Image", product.ImageData);
            cmd.Parameters.AddWithValue("@Category_Name", product.Category);
            var sql = _db.GetDatabase();
            await sql.ExecuteNonQueryAsync(cmd);
        }
        public async Task AddNewChef(ChefsModel chefsmodel)
        {
            SqlCommand cmd = new SqlCommand("SP_AddChef");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Chef_Name", chefsmodel.Chef_Name);
            cmd.Parameters.AddWithValue("@Chef_Job", chefsmodel.Chef_Job);
            cmd.Parameters.AddWithValue("@Image", chefsmodel.ImageChef);
            cmd.Parameters.AddWithValue("@Chef_Description", chefsmodel.Chef_Description);
            var sql = _db.GetDatabase();
            await sql.ExecuteNonQueryAsync(cmd);
        }
        public async Task<IEnumerable<Product>> GetProductWithCatagory(string NameOfProduct)
        {
            SqlCommand cmd = new SqlCommand("SP_GetProductWithCatagory");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Category_name", NameOfProduct);
            var sql = _db.GetDatabase();
            var result = await sql.ExecuteQueryAsync(cmd);
            List<Product> listOfProduct = new List<Product>();

            foreach (DataRow row in result.Rows)
            {
                var product = new Product();
                product.ProductID = Convert.ToInt32(row["Id_Product"]);
                product.Name = row["Name"] as string ?? "";
                product.Price = row["Price"] as string ?? "";
                product.Description = row["Description"] as string ?? "";
                product.ImageData = (byte[])row["Image"];
                listOfProduct.Add(product);
            }
            return listOfProduct;
        }
        public async Task<IEnumerable<Product>> GetDitialsProductByID(int? Id)
        {
            SqlCommand cmd = new SqlCommand("SP_GetProductById");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", Id);
            var sql = _db.GetDatabase();
            var result= await sql.ExecuteQueryAsync(cmd);
            List<Product> listOfProduct = new List<Product>();

            foreach (DataRow row in result.Rows)
            {
                var product = new Product();
                product.Name = row["Name"] as string ?? "";
                product.Price = row["Price"] as string ?? "";
                product.Description = row["Description"] as string ?? "";
                product.ImageData = (byte[])row["Image"];
                listOfProduct.Add(product);
            }
            return listOfProduct;
        }
        public async Task AddCompleteProfile(Addrees address)
        {
            SqlCommand cmd = new SqlCommand("SP_CompeleteProfile");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Image",address.ImageData);
            cmd.Parameters.AddWithValue("@Country", address.Country);
            cmd.Parameters.AddWithValue("@Street", address.Street);
            cmd.Parameters.AddWithValue("@ZipCode", address.ZipCode);
            cmd.Parameters.AddWithValue("@User_Id", address.User_ID);
            var sql = _db.GetDatabase();
            await sql.ExecuteNonQueryAsync(cmd);
        }
        public async Task<IEnumerable<Addrees>> GetAddressForUser(int ID)
        {
            SqlCommand cmd = new SqlCommand("SP_GetAddressForUser");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id_user", ID);
            var sql = _db.GetDatabase();
            var result = await sql.ExecuteQueryAsync(cmd);
            List<Addrees> addresses = new List<Addrees>();

            foreach (DataRow row in result.Rows)
            {
                Addrees addr = new Addrees();
                addr.AddreesID = Convert.ToInt32(row["Id"]);
                addr.Country = row["Country"] as string ?? "";
                addr.Street = row["Street"] as string ?? "";
                addr.ZipCode = row["ZipCode"] as string ?? "";
                addr.ImageData = (byte[])row["Image"];

                addresses.Add(addr);
            }
            return addresses;
        }
        public async Task<DataTable> GetEmptyImage()
        {
            SqlCommand cmd = new SqlCommand("SP_GetImage");
            cmd.CommandType = CommandType.StoredProcedure;
            var sql = _db.GetDatabase();
            var result= await sql.ExecuteQueryAsync(cmd);
            return result;
        }
        public async Task UpdateCompleteProfile(Addrees addrees)
        {
            SqlCommand cmd = new SqlCommand("SP_UpdateCompleteProfile");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Image", addrees.ImageData);
            cmd.Parameters.AddWithValue("@Country", addrees.Country);
            cmd.Parameters.AddWithValue("@Street", addrees.Street);
            cmd.Parameters.AddWithValue("@ZipCode", addrees.ZipCode);
            cmd.Parameters.AddWithValue("@User_Id", addrees.User_ID);
            var sql = _db.GetDatabase();
            await sql.ExecuteNonQueryAsync(cmd);
        }
        public async Task<IEnumerable<Product>> FilterByPrice(Product products)
        {
            SqlCommand cmd = new SqlCommand("FilterByPrice");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Price", products.Price);
            cmd.Parameters.AddWithValue("@Category_name", products.Category);
            var sql = _db.GetDatabase();
            var result = await sql.ExecuteQueryAsync(cmd);
            List<Product> listOfProduct = new List<Product>();

            foreach (DataRow row in result.Rows)
            {
                var product = new Product();
                product.ProductID = Convert.ToInt32(row["Id_Product"]);
                product.Name = row["Name"] as string ?? "";
                product.Price = row["Price"] as string ?? "";
                product.Description = row["Description"] as string ?? "";
                product.ImageData = (byte[])row["Image"];
                listOfProduct.Add(product);
            }
            return listOfProduct;
        }
    }
}
