using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TruyumClientApp.Models;

namespace TruyumClientApp.Controllers
{
    public class UserController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            User item;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://localhost:44300/User/Get", content);
                string apiResponse = await response.Content.ReadAsStringAsync();
                item = JsonConvert.DeserializeObject<User>(apiResponse);
                using (var response1 = await httpClient.PostAsync("https://localhost:44300/User/Login", content))
                {
                    if (response1.IsSuccessStatusCode)
                    {
                        var token1 = await response1.Content.ReadAsStringAsync();
                        JWT token = JsonConvert.DeserializeObject<JWT>(token1);
                        HttpContext.Session.SetString("token", token.Token);
                        HttpContext.Session.SetString("user", JsonConvert.SerializeObject(item));
                        HttpContext.Session.SetInt32("userID", item.UserID);
                        HttpContext.Session.SetString("userName", item.UserName);
                        ViewBag.Message = "User logged in successfully!";
                        if (item.UserName == "Admin")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        return RedirectToAction("Menu", "Customer");
                    }
                    else
                    {
                        ViewBag.Message = "invalid username/password";
                        return View();
                    }
                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            // HttpContext.Session.SetString("user", null);

            return View("Login");
        }

    }
}