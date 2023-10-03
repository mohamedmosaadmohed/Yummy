using DemoLibrary.Models;
using DemoModel.Models;
using Microsoft.AspNetCore.Identity;
using SOfCO.Helpers;
using Yummy.Models;

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
            var response = await httpClient.GetFromJsonAsync<LoginModel>($"api/Auth/Checklogin{Email}");
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
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
        public async Task<bool> Completeprofile(Address address)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/Auth/profile", address);

            if (response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {

                return true;
            }
        }
        public async Task<List<Address>> GetAddressforuser(int Id)
        {
            var response = await httpClient.GetFromJsonAsync<List<Address>>($"api/Auth/Address{Id}");

            if (response != null && response.Count > 0)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
        public async Task UpdateProfile(Address address)
        {
            await httpClient.PutAsJsonAsync("api/Auth/Update-profile", address);
        }
        public async Task<List<Product>> Filterbyprice(Product products)
        {
            var response = await httpClient.GetFromJsonAsync<List<Product>>($"api/Auth/price");

            if (response != null)
            {
                return response;
            }
            else
            {
                throw new Exception("Failed to retrieve data from the API.");
            }
        }
    }
}