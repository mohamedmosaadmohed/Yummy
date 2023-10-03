using Blazor.Shared.Models;
using Blazor.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YummyApi.Models;

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
        [HttpPost]
        [Route("profile")]
        public async Task<ActionResult> CompleteProfile(Addrees addrees)
        {
            try
            {
                await _productServices.AddCompleteProfile(addrees);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("Address{Id}")]
        public async Task<ActionResult> GetAddress(int Id)
        {
            try
            {
                List<Addrees> addresses = new List<Addrees>();
                addresses = (List<Addrees>)await _productServices.GetAddressForUser(Id);
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("Update-profile")]
        public async Task<ActionResult> UpdateCompleteProfile(Addrees addrees)
        {
            try
            {
                await _productServices.UpdateCompleteProfile(addrees);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
