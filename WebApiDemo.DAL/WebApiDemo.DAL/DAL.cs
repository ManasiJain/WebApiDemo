using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using WebApiDemo.DAL.Models;

namespace WebApiDemo.DAL
{
    public class DAL : IDisposable
    {
        private SqlConnection connection;

        public DAL(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.ConnectionString = connectionString;
            EstablishConnection();
        }

        public void EstablishConnection()
        {
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public List<CustomerModel> GetCustomers()
        {
            List<CustomerModel> result = new List<CustomerModel>();
            List<Customer> customers = new List<Customer>();
            using (SqlCommand command = new SqlCommand("Select * from Customer", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string fName = reader.GetString(1);
                    string lName = reader.GetString(2);
                    DateTime birthDate = reader.GetDateTime(3);
                    string email = reader.GetString(4);
                    long ph = reader.GetInt64(5);
                    string custCode = reader.GetString(6);
                    customers.Add(new Customer()
                    {
                        CustomerId = id,
                        FirstName = fName,
                        LastName = lName,
                        BirthDate = birthDate,
                        Email = email,
                        PhoneNumber = ph,
                        CustomerCode = custCode
                    });
                }
            }
            foreach (Customer item in customers)
                result.Add(Mapper.MapCustomerEntityToModel(item));
            return result;
        }

        public List<ProductModel> GetProducts()
        {
            List<ProductModel> result = new List<ProductModel>();
            List<Product> products = new List<Product>();
            using (SqlCommand command = new SqlCommand("Select * from Product", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string pName = reader.GetString(1);
                    decimal price = reader.GetDecimal(2);
                    int qty = reader.GetInt32(3);
                    string productCode = reader.GetString(4);
                    products.Add(new Product()
                    {
                        ProductId = id,
                        ProductName = pName,
                        Price = price,
                        AvailableQty = qty,
                        ProductCode = productCode
                    });
                }
            }
            foreach (Product item in products)
                result.Add(Mapper.MapProductEntityToModel(item));
            return result;
        }

        public List<OrderModel> GetOrders()
        {
            List<OrderModel> result = new List<OrderModel>();
            List<Orders> orders = new List<Orders>();
            using (SqlCommand command = new SqlCommand("Select * from Orders", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int pId = reader.GetInt32(1);
                    int cId = reader.GetInt32(2);
                    int qty = reader.GetInt32(3);
                    orders.Add(new Orders()
                    {
                        OrderId = id,
                        ProdId = pId,
                        CustId = cId,
                        OrderQuantity = qty
                    });
                }
            }
            foreach (Orders item in orders)
                result.Add(Mapper.MapOrderEntityToModel(item));
            return result;
        }

        public CustomerModel GetCustomerByCode(string code)
        {
            Customer customer = new Customer();
            using (SqlCommand command = new SqlCommand($"Select * from Customer where CustomerCode = '{code}'", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string fName = reader.GetString(1);
                    string lName = reader.GetString(2);
                    DateTime birthDate = reader.GetDateTime(3);
                    string email = reader.GetString(4);
                    long ph = reader.GetInt64(5);
                    string custCode = reader.GetString(6);
                    customer = new Customer()
                    {
                        CustomerId = id,
                        FirstName = fName,
                        LastName = lName,
                        BirthDate = birthDate,
                        Email = email,
                        PhoneNumber = ph,
                        CustomerCode = custCode
                    };
                }
            }
            return Mapper.MapCustomerEntityToModel(customer);
        }

        public ProductModel GetProductByCode(string code)
        {
            Product product = new Product();
            using (SqlCommand command = new SqlCommand($"Select * from Product where ProductCode = '{code}'", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string pName = reader.GetString(1);
                    decimal price = reader.GetDecimal(2);
                    int qty = reader.GetInt32(3);
                    string productCode = reader.GetString(4);
                    product = new Product()
                    {
                        ProductId = id,
                        ProductName = pName,
                        Price = price,
                        AvailableQty = qty,
                        ProductCode = productCode
                    };
                }
            }
            return Mapper.MapProductEntityToModel(product);
        }

        public OrderModel GetOrderById(int Id)
        {
            Orders order = new Orders();
            using (SqlCommand command = new SqlCommand($"Select * from Orders where OrderId = {Id}", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int pId = reader.GetInt32(1);
                    int cId = reader.GetInt32(2);
                    int qty = reader.GetInt32(3);
                    order = new Orders()
                    {
                        OrderId = id,
                        ProdId = pId,
                        CustId = cId,
                        OrderQuantity = qty
                    };
                }
            }
            return Mapper.MapOrderEntityToModel(order);
        }

        public int GetTotalNumberOfCustomers()
        {
            int result = 0;
            using (SqlCommand command = new SqlCommand($"Select Count(*) from Customer", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetInt32(0);
            }
            return result;
        }

        public string AddNewCustomer(CustomerModel model)
        {
            int numOfCustomer = GetTotalNumberOfCustomers();
            string custCode = (numOfCustomer <= 9) ? $"CUSTCODE0{numOfCustomer + 1}" : $"CUSTCODE{numOfCustomer + 1}";
            using (SqlCommand command = new SqlCommand($"Insert into Customer values('{model.FirstName}','{model.LastName}','{model.BirthDate}','{model.EmailId}',{model.PhoneNumber},'{custCode}')", connection))
                command.ExecuteNonQuery();
            return custCode;
        }

        public int DeleteCustomer(string custCode)
        {
            using (SqlCommand command = new SqlCommand($"Delete from Customer where CustomerCode = '{custCode}'", connection))
                return command.ExecuteNonQuery();
        }

        public int UpdateCustomer(string customerCode, CustomerModel model)
        {
            using (SqlCommand command = new SqlCommand($"Update Customer Set FirstName = '{model.FirstName}', LastName = '{model.LastName}', BirthDate = '{model.BirthDate}', EmailId = '{model.EmailId}', PhoneNumber = '{model.PhoneNumber}' where CustomerCode = '{customerCode}'", connection))
                return command.ExecuteNonQuery();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    CloseConnection();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
