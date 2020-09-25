﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDemo.DAL.Models
{
    public class ProductModel
    {
        [Required]
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [Required]
        public string ProductCode { get; set; }
    }
}
