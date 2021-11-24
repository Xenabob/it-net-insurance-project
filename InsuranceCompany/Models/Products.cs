using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InsuranceCompany.Models
{
    public class Products
    {
        [Display(Name = "Products")]
        public int ID { get; set; }
        
        [Required]
        public  string Policy { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
         public int VALIDITY_PERIOD { get; set; }
    
    }
}
