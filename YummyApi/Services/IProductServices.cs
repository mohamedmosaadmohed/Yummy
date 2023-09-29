using Blazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data;
using YummyApi.Models;

namespace Blazor.Shared.Services
{
	public interface IProductServices
	{
        Task AddNewUserRegister(RegisterationUser registerationUser);
        Task<bool> CheckEmailExistOrNot(String Email);
        Task<LoginModel> GETLoginForUser(string Email);
        Task AddNewProduct(Product product);
        Task<IEnumerable<Product>> GetAllProduct();
        Task DeleteProduct(int? stdId);
        Task UpdateProduct(Product product);
        Task AddNewChef(ChefsModel chefsmodel);
        Task<IEnumerable<Product>> GetProductWithCatagory(string NameOfProduct);
        Task<IEnumerable<Product>> GetDitialsProductByID(int? Id);
        Task AddCompleteProfile(Addrees address);
        Task<IEnumerable<Addrees>> GetAddressForUser(int ID);
        Task<DataTable> GetEmptyImage();
    }
}
