using System;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCompany.Models
{

    public class InsuranceContext : DbContext
    {
        public InsuranceContext(DbContextOptions<InsuranceContext> options)
            : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
       
        public DbSet<Clients> Clients { get; set; }

        public DbSet<Orders> Orders { get; set; }
            
        
    }

}
