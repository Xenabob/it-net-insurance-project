using System.Collections.Generic;
using InsuranceCompany.Models;

namespace InsuranceCompany.ViewModels
{
    public class ClientDetailsViewModel
    {
        public Clients Client { get; set; }
        public List<Orders> Orders { get; set; }
    }
}
