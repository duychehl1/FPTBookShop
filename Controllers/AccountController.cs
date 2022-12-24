using System;
using BookShop.Data;
using BookShop.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;


namespace BookShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly BookDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AccountController(BookDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            dbContext = context;
            this.webHostEnvironment = webHostEnvironment;
        }



        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Account model)
        {

            if (ModelState.IsValid)
            {
                var check = dbContext.Accounts.FirstOrDefault(s => s.Email == model.Email);
              
                if (check == null)
                {
                    Account account = new Account()
                    {
                        Email = model.Email,
                        Password = GetMD5(model.Password),
                        Role = model.Role,
                    };                             
                    Customer newCustomer = new Customer()
                    {
                        Name = "CustomerName",
                        DateOfBirth = DateTime.Now,
                        Gender = "Unknown",
                        Email = model.Email,
                        Address = "CustomerAddress",
                        CustomerPicture = "",
                        Account = account,
                    };
                    dbContext.Add(account);
                    dbContext.Add(newCustomer);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Login));
                } else
                {
                    ViewBag.error = "This email already exist!";
                    return View();
                } 
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var encodePassword = GetMD5(password);
                var data = dbContext.Accounts.Where(s => s.Email.Equals(email) && s.Password.Equals(encodePassword)).ToList();
                if (data.Count() > 0)
                {                  
                    var users = dbContext.Customers.Where(s => s.Email.Equals(email));                                    
                    if (data.FirstOrDefault().Role.Equals("CUSTOMER"))
                    {
                        HttpContext.Session.SetInt32("ID", users.FirstOrDefault().CusId);
                        return RedirectToAction("Index", "Customer");
                    }    
                        
                    else if (data.FirstOrDefault().Role.Equals("OWNER"))
                    {
                        HttpContext.Session.SetString("Role", data.FirstOrDefault().Role.ToString());
                        return RedirectToAction("Index", "Manager");
                    }
                    else if (data.FirstOrDefault().Role.Equals("ADMIN")) 
                    {
                        HttpContext.Session.SetString("Role", data.FirstOrDefault().Role.ToString());
                        return RedirectToAction("Index", "Admin");
                    }
                        

                }
                else
                {
                    ViewBag.error = "Login failed!";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }      
    }
}
