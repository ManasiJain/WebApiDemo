using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using WebApiDemo.DAL.Models;
using Xunit;

namespace WebApiDemo.DAL.Test
{
    public class DALTest
    {
        public string ConnectionString { get; set; }
        public DALTest()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\")), "config.json"));
            var root = builder.Build();
            ConnectionString = root.GetConnectionString("connString");

        }
        [Fact]
        public void Test_GetCustomers()
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            using (DAL dalObject = new DAL(ConnectionString))
                customers = dalObject.GetCustomers();
            Assert.Equal(5, customers.Count);
        }

        [Fact]
        public void Test_GetProducts()
        {
            List<ProductModel> products = new List<ProductModel>();
            using (DAL dalObject = new DAL(ConnectionString))
                products = dalObject.GetProducts();
            Assert.Equal(4, products.Count);
        }

        [Fact]
        public void Test_GetOrders()
        {
            List<OrderModel> orders = new List<OrderModel>();
            using (DAL dalObject = new DAL(ConnectionString))
                orders = dalObject.GetOrders();
            Assert.Empty(orders);
        }

        [Theory]
        [InlineData("Vaani", "Kankaria", "2019-08-12", "vaani@abc.com", 4568972225, "CUSTCODE01")]
        [InlineData("Ashish", "Kankaria", "1987-01-13", "ashish@abc.com", 7945632165, "CUSTCODE02")]
        [InlineData("Manasi", "Jain", "1988-01-05", "manasi@abc.com", 4456972123, "CUSTCODE03")]
        [InlineData("Ravi", "Jain", "1958-01-12", "ravi@abc.com", 3468975425, "CUSTCODE04")]
        public void Test_GetCustomerByCode(string fName, string lName, string DOB, string emailId, long phoneNum, string custCode)
        {
            CustomerModel customer = new CustomerModel();
            using (DAL dalObject = new DAL(ConnectionString))
                customer = dalObject.GetCustomerByCode(custCode);
            Assert.Equal(fName, customer.FirstName);
            Assert.Equal(lName, customer.LastName);
            Assert.Equal(DOB, customer.BirthDate.ToString("yyyy-MM-dd"));
            Assert.Equal(emailId, customer.EmailId);
            Assert.Equal(phoneNum, customer.PhoneNumber);
            Assert.Equal(custCode, customer.CustomerCode);
        }

        [Theory]
        [InlineData("Skirt", 500, 15, "PCODE01")]
        [InlineData("Tshirt", 250, 20, "PCODE02")]
        [InlineData("Jeans", 1000, 15, "PCODE03")]
        [InlineData("Frock", 800, 15, "PCODE04")]
        public void Test_GetProductById(string pName, decimal price, int qty, string prodCode)
        {
            ProductModel product = new ProductModel();
            using (DAL dalObject = new DAL(ConnectionString))
                product = dalObject.GetProductByCode(prodCode);
            Assert.Equal(pName, product.ProductName);
            Assert.Equal(price, product.Price);
            Assert.Equal(qty, product.Quantity);
            Assert.Equal(prodCode, product.ProductCode);
        }

        [Fact]
        public void Test_AddNewCustomerAndDelete()
        {
            string custCode = "";
            int numOfCustomerBefore, numOfCustomerAfter;
            using (DAL dalObject = new DAL(ConnectionString))
            {
                numOfCustomerBefore = dalObject.GetTotalNumberOfCustomers();
                CustomerModel model = new CustomerModel
                {
                    FirstName = "Test",
                    LastName = "Dummy",
                    BirthDate = new DateTime(1999, 11, 14),
                    EmailId = "sandhya_jain@gmail.com",
                    PhoneNumber = 123456659
                };
                custCode = dalObject.AddNewCustomer(model);
                numOfCustomerAfter = dalObject.GetTotalNumberOfCustomers();
            }
            Assert.Equal(numOfCustomerBefore, numOfCustomerAfter - 1);

            using (DAL dalObject = new DAL(ConnectionString))
            {
                numOfCustomerBefore = dalObject.GetTotalNumberOfCustomers();
                CustomerModel model = new CustomerModel
                {
                    FirstName = "Sandhya",
                    LastName = "Jain",
                    BirthDate = new DateTime(1962, 1, 31),
                    EmailId = "sandhya_jain@gmail.com",
                    PhoneNumber = 123456659
                };
                dalObject.DeleteCustomer(custCode);
                numOfCustomerAfter = dalObject.GetTotalNumberOfCustomers();

            }
            Assert.Equal(numOfCustomerBefore, numOfCustomerAfter + 1);
        }

        [Fact]
        public void Test_UpdateCustomer()
        {
            int numRowsEffected = 0;
            string custCode = "CUSTCODE03";
            CustomerModel customer = new CustomerModel();
            using (DAL dalObject = new DAL(ConnectionString))
                customer = dalObject.GetCustomerByCode(custCode);
            CustomerModel model = new CustomerModel
            {
                FirstName = "Test",
                LastName = "Dummy",
                BirthDate = new DateTime(2000, 10, 12),
                EmailId = "test_dummy@gmail.com",
                PhoneNumber = 324234234
            };
            using (DAL dalObject = new DAL(ConnectionString))
                numRowsEffected = dalObject.UpdateCustomer(custCode, model);
            Assert.Equal(1, numRowsEffected);
            using (DAL dalObject = new DAL(ConnectionString))
                model = dalObject.GetCustomerByCode(custCode);
            Assert.NotEqual(customer.FirstName, model.FirstName);
            Assert.NotEqual(customer.LastName, model.LastName);
            Assert.NotEqual(customer.BirthDate, model.BirthDate);
            Assert.NotEqual(customer.EmailId, model.EmailId);
            Assert.NotEqual(customer.PhoneNumber, model.PhoneNumber);

            using (DAL dalObject = new DAL(ConnectionString))
                numRowsEffected = dalObject.UpdateCustomer(custCode, customer);
            Assert.Equal(1, numRowsEffected);
            using (DAL dalObject = new DAL(ConnectionString))
                model = dalObject.GetCustomerByCode(custCode);
            Assert.Equal(customer.FirstName, model.FirstName);
            Assert.Equal(customer.LastName, model.LastName);
            Assert.Equal(customer.BirthDate, model.BirthDate);
            Assert.Equal(customer.EmailId, model.EmailId);
            Assert.Equal(customer.PhoneNumber, model.PhoneNumber);
        }
    }
}
