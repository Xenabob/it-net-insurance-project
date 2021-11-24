using System.ComponentModel.DataAnnotations;

namespace InsuranceCompany.Models
{
    public class Clients
    {
        [Display(Name = "Client")]

        public int ID { get; set; }


        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string FIRST_NAME { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string LAST_NAME { get; set; }



        [Required]
        [DataType(DataType.Date)]
        public string Birth{ get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Adress { get; set; }


        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string PASSPORT { get; set; }

        [Phone]
        [Required]
        public string PHONE { get; set; }

        [EmailAddress]
        [Required]
        public string EMAIL { get; set; }
    
    }
}
