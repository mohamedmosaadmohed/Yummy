using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YummyApi.Models
{
    public class ChefsModel
    {
        public int Chef_Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Chef_Name { get; set; } = string.Empty;

        public byte[] ImageChef { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(4000, ErrorMessage = "Description cannot exceed 4000 characters")]
        public string Chef_Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Job is required")]
        public string Chef_Job { get; set; } = string.Empty;
    }
}
