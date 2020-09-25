using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiDemo.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private DAL.DAL dalObject;
        public ProductController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            dalObject = new DAL.DAL(connectionString);
        }
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(dalObject.GetProducts());
            }
            catch (Exception ex)
            {
                //TODO: Add logging
                return InternalServerError(ex);
            }
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
