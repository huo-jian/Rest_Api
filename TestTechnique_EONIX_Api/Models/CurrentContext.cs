using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTechnique_EONIX_Api.Models
{
    public class CurrentContext : DbContext
    {

        public CurrentContext(DbContextOptions<CurrentContext> options)
         : base(options)
        {
        }

        // Tables
        public virtual DbSet<Person> Persons { get; set; }
    }
}
