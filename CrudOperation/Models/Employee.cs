using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudOperation.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Your First Name")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Enter Your Last Name")]
        public string LastName { get; set; }
        //public List<Employee> ShowallEmployee { get; set; }
    }
}