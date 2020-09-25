using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDemo.UI.Models
{
    public class CustomerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string EmailId { get; set; }
        public long PhoneNumber { get; set; }
        public string CustomerCode { get; set; }
    }
}
