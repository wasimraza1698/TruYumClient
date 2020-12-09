using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TruyumClientApp.Models;

namespace TruyumClientApp.Controllers
{
    public class CustomerController : Controller
    {
        public async Task<IActionResult> Menu()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("https://localhost:44301/menu/getall");
                    var result =await response.Content.ReadAsStringAsync();
                    var menu = JsonConvert.DeserializeObject<List<MenuItem>>(result);
                    List<MenuItem> menu1=new List<MenuItem>();
                    foreach (var item in menu)
                    {
                        if (item.Active == "Yes")
                        {
                            menu1.Add(item);
                        }
                    }
                    return View(menu1);
                }
            }
        }

        public async Task<IActionResult> ViewCart()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("https://localhost:44330/cart/getall");
                    var result = await response.Content.ReadAsStringAsync();
                    var cart = JsonConvert.DeserializeObject<List<CartItem>>(result);
                    return View(cart);
                }
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync("https://localhost:44330/remove"+id.ToString());
                var result = response.StatusCode;
                if (result == HttpStatusCode.OK)
                {
                    return RedirectToAction("ViewCart");
                }
                else
                {
                    return Content("couldn't delete");
                }
            }
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                CartItem item = new CartItem(){UserID =(int)HttpContext.Session.GetInt32("userID"),CartItemID = id};
                StringContent content = new StringContent(JsonConvert.SerializeObject(item),Encoding.UTF8,"application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync("https://localhost:44330/cart/add", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ViewCart");
                    }
                    else
                    {
                        return Content("could not add");
                    }
                }
            }
        }
    }
}