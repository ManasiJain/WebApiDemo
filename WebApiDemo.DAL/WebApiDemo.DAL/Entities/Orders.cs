﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDemo.DAL
{
    public class Orders
    {
        public int OrderId { get; set; }
        public int CustId { get; set; }
        public int ProdId { get; set; }
        public int OrderQuantity { get; set; }
    }
}
