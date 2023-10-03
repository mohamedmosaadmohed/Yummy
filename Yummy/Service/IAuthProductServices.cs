using DemoLibrary.Models;
using DemoModel.Models;
using Yummy.Models;

namespace BlazorA.Service
{
    public interface IAuthProductServices
    {
        Task<bool> AddNewUser(RegisterationUser registerationUser);
        Task<LoginModel> CheckLogin(string Email);
        Task<bool> checkAddNewProduct(Product product);
        Task<List<Product>> GetProduct();
        Task DeleteProduct(int ProductID);
        Task UpdateProduct(Product product);
        Task<bool> AddNewChef(ChefsModel chefsmodel);
        Task<List<Product>> GetCatagoryWithName(string NameOfProduct);
        Task<List<Product>> GetProductbyId(int? Id);
        Task<bool> Completeprofile(Address address);
        Task<List<Address>> GetAddressforuser(int Id);
        Task UpdateProfile(Address address);
        Task<List<Product>> Filterbyprice(Product products);
    }
}
