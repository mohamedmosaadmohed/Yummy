using System.ComponentModel.DataAnnotations;

namespace Yummy.Models
{
    public class Address
    {
        public int AddreesID { get; set; }
        public byte[] ImageData { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "ZipCode is required")]
        public string ZipCode { get; set; } = string.Empty;
        public int User_ID { get; set; }
    }
}
