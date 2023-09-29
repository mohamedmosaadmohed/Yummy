using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yummy.Models
{
    public class LoginModel
    {
        public int User_Id { get; set; }
        [Required(ErrorMessage = "Email is requied")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is requied")]
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; }
        public string FullName { get; set; }

        public static implicit operator List<object>(LoginModel v)
        {
            throw new NotImplementedException();
        }
    }
}
