using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApiDemo.UI.Models;

namespace WebApiDemo.UI.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public async Task<ActionResult> Index()
        {
            IEnumerable<CustomerViewModel> customers = null;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:44346/api/");

                    //HTTP Get
                    var responseTask = await client.GetAsync("customer");

                    if (responseTask.IsSuccessStatusCode)
                    {
                        string customersString = await responseTask.Content.ReadAsStringAsync();
                        customers = JsonConvert.DeserializeObject<List<CustomerViewModel>>(customersString);
                    }
                    else //web api sent error response 
                    {
                        //log response status here..
                        customers = Enumerable.Empty<CustomerViewModel>();
                        ModelState.AddModelError("", "Server error. Please contact administrator.");
                    }
                }
                catch (Exception)
                {
                    customers = Enumerable.Empty<CustomerViewModel>();
                    ModelState.AddModelError("", "Server error. Please contact administrator.");
                }
            }
            return View(customers);
        }

        public ActionResult create()
        {
            return View();
        }

        // Post: Customer
        [HttpPost]
        public async Task<ActionResult> Create(CustomerViewModel customer)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:44346/api/");

                    var content = new StringContent(
                                        JsonConvert.SerializeObject(customer),
                                        Encoding.UTF8,
                                        "application/json");
                    //HTTP Post
                    var postTask = await client.PostAsync("customer", content);

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else //web api sent error response 
                    {
                        //log response status here..
                        ModelState.AddModelError("", "Server error. Please contact administrator.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Server error. Please contact administrator.");
                }
            }
            return View(customer);
        }

        public async Task<ActionResult> Edit(string custCode)
        {
            CustomerViewModel customer = null;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:44346/api/");

                    //HTTP Get
                    var responseTask = await client.GetAsync($"customer/{custCode}");

                    if (responseTask.IsSuccessStatusCode)
                    {
                        string customersString = await responseTask.Content.ReadAsStringAsync();
                        customer = JsonConvert.DeserializeObject<CustomerViewModel>(customersString);
                    }
                    else //web api sent error response 
                    {
                        //log response status here..
                        customer = new CustomerViewModel();
                        ModelState.AddModelError("", "Server error. Please contact administrator.");
                    }
                }
                catch (Exception)
                {
                    customer = new CustomerViewModel();
                    ModelState.AddModelError("", "Server error. Please contact administrator.");
                }
            }
            return View(customer);
        }

        // PUT: Customer
        [HttpPost]
        public async Task<ActionResult> Edit(CustomerViewModel customer)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:44346/api/");

                    var content = new StringContent(
                                        JsonConvert.SerializeObject(customer),
                                        Encoding.UTF8,
                                        "application/json");
                    //HTTP Post
                    var postTask = await client.PutAsync($"customer/{customer.CustomerCode}", content);

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else //web api sent error response 
                    {
                        //log response status here..
                        ModelState.AddModelError("", "Server error. Please contact administrator.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Server error. Please contact administrator.");
                }
            }
            return View(customer);
        }


        public async Task<ActionResult> Delete(string custCode)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:44346/api/");

                    //HTTP Get
                    var responseTask = await client.DeleteAsync($"customer/{custCode}");

                    if (responseTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else //web api sent error response 
                    {
                        //log response status here..
                        ModelState.AddModelError("", "Server error. Please contact administrator.");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Server error. Please contact administrator.");
                }
            }
            return RedirectToAction("Index");
        }
    }
}