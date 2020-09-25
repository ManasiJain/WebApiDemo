using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDemo.DAL.Models
{
    public class CustomerModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string EmailId { get; set; }
        [Required]
        public long PhoneNumber { get; set; }
        [Required]
        public string CustomerCode { get; set; }
    }
}
