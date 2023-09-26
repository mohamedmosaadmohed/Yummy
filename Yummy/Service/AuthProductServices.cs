using DemoLibrary.Models;
using DemoModel.Models;
using Microsoft.AspNetCore.Identity;
using SOfCO.Helpers;

namespace BlazorA.Service
{
    public class AuthProductServices : IAuthProductServices
    {
        private readonly HttpClient httpClient;
        

        public AuthProductServices(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<bool> AddNewUser(RegisterationUser registerationUser)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/Auth/add", registerationUser);

            if (response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {

                return true;
            }
        }
        public async Task<LoginModel> CheckLogin(string Email)
        {
            var response = new LoginModel();
            response = await httpClient.GetFromJsonAsync<LoginModel>($"api/Auth/Checklogin{Email}");
            if (response != null)
            {
                return response;
            }
            else
            {
                throw new Exception("Failed to retrieve data from the API.");
            }
        }
        public async Task<bool> checkAddNewProduct(Product product)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/Auth/product", product);

            if (response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<List<Product>> GetProduct()
        {
          var  response = await httpClient.GetFromJsonAsync<List<Product>>("api/Auth/List");
            if (response != null)
            {
                return response;
            }
            else
            {
                throw new Exception("Failed to retrieve data from the API.");
            }
        }
        public async Task DeleteProduct(int ProductID)
        {
            await httpClient.DeleteAsync($"api/Auth/Delete/{ProductID}");
        }
        public async Task UpdateProduct(Product product)
        {
            await httpClient.PutAsJsonAsync("api/Auth/Update", product);
        }
        public async Task<bool> AddNewChef(ChefsModel chefsmodel)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/Auth/Chef", chefsmodel);

            if (response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<List<Product>> GetCatagoryWithName(string NameOfProduct)
        {
            var response = await httpClient.GetFromJsonAsync<List<Product>>($"api/Auth/Catagory-Name{NameOfProduct}");

            if (response != null)
            {
                return response;
            }
            else
            {
                throw new Exception("Failed to retrieve data from the API.");
            }
        }
        public async Task<List<Product>> GetProductbyId(int? Id)
        {
            var response = await httpClient.GetFromJsonAsync<List<Product>>($"api/Auth/Catagory-Ditials{Id}");

            if (response != null)
            {
                return response;
            }
            else
            {
                throw new Exception("Failed to retrieve data from the API.");
            }
        }

        //public async Task<List<StudentEntity>> GetStudents()
        //{
        //    return await httpClient.GetFromJsonAsync<List<StudentEntity>>("api/StudentOperation/list");
        //}
        //public async Task DeleteStudent(int studentId)
        //{
        //    await httpClient.DeleteAsync($"api/StudentOperation/Delete/{studentId}");
        //}
        //public async Task AddNewStudent(StudentEntity student)
        //{
        //    await httpClient.PostAsJsonAsync("api/StudentOperation/Add", student);
        //}
    }
}