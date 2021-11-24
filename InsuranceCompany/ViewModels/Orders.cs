using System.Collections.Generic;
using InsuranceCompany.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InsuranceCompany.ViewModels
{
    public class OrdersCreateViewModel
    {
        public Orders Order { get; set; }
        public List<SelectListItem> Products { get; set; }
        public List<SelectListItem> Clients { get; set; }
    }
}
