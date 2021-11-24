using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InsuranceCompany.Models
{
    public class Orders
    {
        [Display(Name = "Orders")]
        public int ID { get; set; }

        [Required]
        public string PRODUCT_ID { get; set; }

        [Required]
        public string CLIENT_ID { get; set; }

        [Required]
        public string VALIDITY_FROM { get; set; }

        [Required]
        public string VALIDITY_TO { get; set; }
    }
}
