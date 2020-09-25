using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDemo.DAL.Models
{
    public class OrderModel
    {
        //TODO: Modify as per requirement
        public int OrderId { get; set; }
        public int CustId { get; set; }
        public int ProdId { get; set; }
        public int OrderQuantity { get; set; }
    }
}
