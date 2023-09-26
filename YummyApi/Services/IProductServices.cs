using Blazor.Shared.Models;
using DemoLibrary.Models;
using DemoModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared.Services
{
	public interface IProductServices
	{
        //Task<IEnumerable<StudentEntity>> GetAllStudent();
        //Task AddStudent(StudentEntity student);
        //Task UpdateStudent(StudentEntity student);
        //Task DeleteStudent(int? stdId);
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
    }
}
