using Blazor.Shared.Models;
using Blazor.Shared.Services;
using DemoLibrary.Models;
using DemoModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Blazor.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IProductServices _productServices;
        public AuthController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> RegisterationForUser(RegisterationUser registerationUser)
        {
            try
            {
                bool emailCheckResult = await _productServices.CheckEmailExistOrNot(registerationUser.Email);

                if (emailCheckResult)
                {
                    return BadRequest();
                }
                await _productServices.AddNewUserRegister(registerationUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Checklogin{Email}")]
        public async Task<ActionResult<LoginModel>> LoginForUser(string Email)
        {
            try
            {
                var login = new LoginModel();
                login= await _productServices.GETLoginForUser(Email);
                return Ok(login);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("product")]
        public async Task<ActionResult> NewProduct(Product product)
        {
            try
            {
                await _productServices.AddNewProduct(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> GetListOfProduct()
        {
            try
            {
                List<Product> products = new List<Product>();
                products = (List<Product>)await _productServices.GetAllProduct();
                return Ok(products);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("Delete/{ProductID}")]
        public async Task<ActionResult> DeleteStudent(int ProductID)
        {
            await _productServices.DeleteProduct(ProductID);
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> UpdateStudent(Product product)
        {
            try
            {
                await _productServices.UpdateProduct(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("Chef")]
        public async Task<ActionResult> NewChef(ChefsModel chefsmodel)
        {
            try
            {
                await _productServices.AddNewChef(chefsmodel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("Catagory-Name{NameOfProduct}")]
        public async Task<ActionResult> GetProductWithCatagory(string NameOfProduct)
        {
            try
            {
                List<Product> productCatagory = new List<Product>();
                productCatagory = (List<Product>)await _productServices.GetProductWithCatagory(NameOfProduct);
                return Ok(productCatagory);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("Catagory-Ditials{Id}")]
        public async Task<ActionResult> GetProductbyId(int? Id)
        {
            try
            {
                List<Product> productCatagory = new List<Product>();
                productCatagory = (List<Product>)await _productServices.GetDitialsProductByID(Id);
                return Ok(productCatagory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpGet]
        //[Route("list")]
        //public async Task<ActionResult> GetStudentsList()
        //{
        //   List<StudentEntity> students = new List<StudentEntity>();
        //    students= (List<StudentEntity>)await _studentServices.GetAllStudent();
        //    return Ok(students);
        //}
        //[HttpPost]
        //[Route("add")]
        //public async Task<ActionResult> AddNewStudent(StudentEntity studentEntity)
        //{

        //    try
        //    {
        //        await _studentServices.AddStudent(studentEntity);
        //        return Ok();
        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpDelete]
        //[Route("Delete/{StudentID}")]
        //public async Task<ActionResult> DeleteStudent(int StudentID) 
        //{
        //    await _studentServices.DeleteStudent(StudentID);
        //    return Ok();
        //}


        //[HttpPut]
        //[Route("Update")]
        //public async Task<ActionResult> UpdateStudent(StudentEntity studentEntity)
        //{
        //    try
        //    {
        //       await _studentServices.UpdateStudent(studentEntity);
        //        return Ok();
        //    }
        //    catch(Exception ex) 
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

    }
}
