using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestTechnique_EONIX_Api.Models
{
    [Table("Person", Schema = "dbo")]
    public class Person
    {
        [Key]
        public Guid PersonKey { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }


    }
}
