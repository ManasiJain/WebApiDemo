using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDemo.DAL.Models;

namespace WebApiDemo.DAL
{
    public static class Mapper
    {
        public static CustomerModel MapCustomerEntityToModel(Customer customer)
        {
            CustomerModel result = new CustomerModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                BirthDate = customer.BirthDate,
                EmailId = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                CustomerCode = customer.CustomerCode
            };
            return result;
        }

        public static ProductModel MapProductEntityToModel(Product product)
        {
            ProductModel result = new ProductModel
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = product.AvailableQty,
                ProductCode = product.ProductCode
            };
            return result;
        }

        public static OrderModel MapOrderEntityToModel(Orders order)
        {
            OrderModel result = new OrderModel
            {
                OrderId = order.OrderId,
                CustId = order.CustId,
                ProdId = order.ProdId,
                OrderQuantity = order.OrderQuantity
            };
            return result;
        }

        public static Customer MapCustomerModelToEntity(CustomerModel customer)
        {
            Customer result = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                BirthDate = customer.BirthDate,
                Email = customer.EmailId,
                PhoneNumber = customer.PhoneNumber,
                CustomerCode = customer.CustomerCode
            };
            return result;
        }

        public static Product MapProductModelToEntity(ProductModel product)
        {
            Product result = new Product
            {
                ProductName = product.ProductName,
                Price = product.Price,
                AvailableQty = product.Quantity,
                ProductCode = product.ProductCode
            };
            return result;
        }

        public static Orders MapOrderModelToEntity(OrderModel order)
        {
            Orders result = new Orders
            {
                OrderId = order.OrderId,
                CustId = order.CustId,
                ProdId = order.ProdId,
                OrderQuantity = order.OrderQuantity
            };
            return result;
        }
    }
}
