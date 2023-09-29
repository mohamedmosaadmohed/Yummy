using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YummyApi.Models
{
    public class RegisterationUser
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Name is requied")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is requied")]
        public string Email { get; set; } = string.Empty;

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$",
            ErrorMessage = "Password must be at least 8 characters, with at least 1 uppercase letter, 1 lowercase letter, 1 digit, and 1 special character")]
        public string Password { get; set; } = string.Empty;
        public bool AgreeAllStatement { get; set; }
    }
}
