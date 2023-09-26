using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using DemoModel.Models;
using SOfCO.Helpers;
using Blazor.Shared.Models;
using DemoLibrary.Models;

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
        public async Task<LoginModel> GETLoginForUser(string Email)
        {
            string emailInDataBase="";
            string PasswordInDataBase="";
            string Role="";
            string FullName="";
            SqlCommand cmd = new SqlCommand("SP_CheckLogin");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", Email);
            var sql = _db.GetDatabase();
            var result= await sql.ExecuteQueryAsync(cmd);
            foreach( DataRow row in result.Rows)
            {
                emailInDataBase= row["Email"] as string ?? "";
                PasswordInDataBase= row["Password"] as string ?? "";
                FullName = row["Name"] as string ?? "";
                Role = row["Role"] as string ?? "";
               var loginModel = new LoginModel
                {
                    Email = emailInDataBase,
                    Password = PasswordInDataBase,
                    FullName= FullName,
                    Role = Role
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
            cmd.Parameters.AddWithValue("@@product_Id", product.ProductID);
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

        //public async Task<IEnumerable<StudentEntity>> GetAllStudent()
        //{
        //	SqlCommand cmd = new SqlCommand("GetAllStudentRecord");
        //	cmd.CommandType = CommandType.StoredProcedure;
        //	var sql = _db.GetDatabase();
        //	var result = await sql.ExecuteQueryAsync(cmd);
        //	List<StudentEntity> listOfStudent = new List<StudentEntity>();

        //	foreach (DataRow row in result.Rows)
        //	{
        //		var student = new StudentEntity();
        //		student.StudentId = Convert.ToInt32(row["StudentID"]);
        //		student.FirstName = row["FirstName"] as string ?? "";
        //		student.LastName = row["LastName"] as string ?? "";
        //		student.Email = row["Email"] as string ?? "";
        //		student.Gender = row["Gender"] as string ?? "";
        //		student.CreateOn = Convert.ToDateTime(row["CreatedOn"]);
        //		listOfStudent.Add(student);
        //	}

        //	return listOfStudent;
        //}  //done
        //public async Task AddStudent(StudentEntity student)
        //{
        //	SqlCommand cmd = new SqlCommand("AddNewStudent");
        //	cmd.CommandType = CommandType.StoredProcedure;
        //	cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
        //	cmd.Parameters.AddWithValue("@LastName", student.LastName);
        //	cmd.Parameters.AddWithValue("@Email", student.Email);
        //	cmd.Parameters.AddWithValue("@Gender", student.Gender);
        //	var sql=_db.GetDatabase();
        //	await sql.ExecuteNonQueryAsync(cmd);
        //}  //done
        //public async Task UpdateStudent(StudentEntity student)
        //{
        //	SqlCommand cmd = new SqlCommand("UpdateStudentRcord");
        //	cmd.CommandType = CommandType.StoredProcedure;
        //	cmd.Parameters.AddWithValue("@StudentID", student.StudentId);
        //	cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
        //	cmd.Parameters.AddWithValue("@LastName", student.LastName);
        //	cmd.Parameters.AddWithValue("@Email", student.Email);
        //	cmd.Parameters.AddWithValue("@Gender", student.Gender);
        //	var sql = _db.GetDatabase();
        //	await sql.ExecuteNonQueryAsync(cmd);
        //      }  
        // public async Task DeleteStudent(int? stdId)
        //{
        //	SqlCommand cmd = new SqlCommand("DeletStudentRecord");
        //	cmd.CommandType = CommandType.StoredProcedure;
        //	cmd.Parameters.AddWithValue("@StudentID",stdId);
        //	var sql = _db.GetDatabase();
        //	await sql.ExecuteNonQueryAsync(cmd);
        //      }
    }
}
