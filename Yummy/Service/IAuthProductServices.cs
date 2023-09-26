using DemoLibrary.Models;
using DemoModel.Models;

namespace BlazorA.Service
{
    public interface IAuthProductServices
    {
        //Task<List<StudentEntity>> GetStudents();
        //Task DeleteStudent(int studentId);
        //Task AddNewStudent(StudentEntity student);
        Task<bool> AddNewUser(RegisterationUser registerationUser);
        Task<LoginModel> CheckLogin(string Email);
        Task<bool> checkAddNewProduct(Product product);
        Task<List<Product>> GetProduct();
        Task DeleteProduct(int ProductID);
        Task UpdateProduct(Product product);
        Task<bool> AddNewChef(ChefsModel chefsmodel);
        Task<List<Product>> GetCatagoryWithName(string NameOfProduct);
        Task<List<Product>> GetProductbyId(int? Id);
    }
}
