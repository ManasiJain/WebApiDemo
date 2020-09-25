using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiDemo.DAL;
using System.Configuration;
using WebApiDemo.DAL.Models;

namespace WebApiDemo.WebApi.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        private DAL.DAL dalObject;
        public CustomerController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            dalObject = new DAL.DAL(connectionString);
        }

        [Route()]
        public IHttpActionResult Get()
        {
            try
            {
                //return BadRequest("It was a bad day :(");
                //return Ok(new { Name = "Manasi", City = "Pune" });
                return Ok(dalObject.GetCustomers());
            }
            catch (Exception ex)
            {
                //TODO: Add logging
                return InternalServerError(ex);
            }
        }

        [Route("{customerCode}", Name = "GetCustomer")]
        public IHttpActionResult Get(string customerCode)
        {
            try
            {
                var result = dalObject.GetCustomerByCode(customerCode);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [Route()]
        public IHttpActionResult Post(CustomerModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = dalObject.AddNewCustomer(model);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return BadRequest(ModelState);
        }

        [Route("{customerCode}")]
        public IHttpActionResult PUT(string customerCode, CustomerModel model)
        {
            int result = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    result = dalObject.UpdateCustomer(customerCode, model);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return BadRequest(ModelState);
        }

        [Route("{customerCode}")]
        public IHttpActionResult Delete(string customerCode)
        {
            int result = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    result = dalObject.DeleteCustomer(customerCode);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return BadRequest(ModelState);
        }

        protected override void Dispose(bool disposing)
        {
            if (dalObject != null)
            {
                dalObject.Dispose();
                dalObject = null;
            }
            base.Dispose(disposing);
        }
    }
}
