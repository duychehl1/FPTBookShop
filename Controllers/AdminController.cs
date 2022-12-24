using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
namespace BookShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly BookDbContext dbContext;
       
        public AdminController(BookDbContext context)
        {
            dbContext = context;
          
        }
        public IActionResult Index()
        {
            if (!checkSession()) return RedirectToAction("Login", "Account");
            return View(dbContext.Accounts.ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (!checkSession()) return RedirectToAction("Login", "Account");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Account form)
        {
            if (!checkSession()) return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                var check = dbContext.Accounts.FirstOrDefault(s => s.Email == form.Email);

                if (check == null)
                {
                    Account newAccount = new Account()
                    {
                        Email = form.Email,
                        Password = GetMD5(form.Password),
                        Role = form.Role,
                    };
                   
                    dbContext.Add(newAccount);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.error = "This email already exist!";
                    return View();
                }
            }
            return View();

        }

        [HttpGet]
        public IActionResult Edit(string? email)
        {
            if (!checkSession()) return RedirectToAction("Login", "Account");
            var account = dbContext.Accounts.Where(a => a.Email.Contains(email)).FirstOrDefault();
            
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Account editedAccount)
        {
            if (!checkSession()) return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                dbContext.Update(editedAccount);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(editedAccount);
            }
        }

        public IActionResult OwnerAccount()
        {
            if (!checkSession()) return RedirectToAction("Login", "Account");
            return View(dbContext.Accounts.Where(b => b.Role.Equals("OWNER")).ToList());
        }

        public IActionResult CustomerAccount()
        {
            if (!checkSession()) return RedirectToAction("Login", "Account");
            return View(dbContext.Accounts.Where(b => b.Role.Equals("CUSTOMER")).ToList());
        }
       
        public IActionResult Delete(string? email)
        {

            if(!checkSession()) return RedirectToAction("Login", "Account");
            var account = dbContext.Accounts.Where(a => a.Email.Contains(email)).FirstOrDefault();
            dbContext.Remove(account);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        private bool checkSession()
        {
            if (HttpContext.Session.GetString("Role") == null || !HttpContext.Session.GetString("Role").Equals("ADMIN"))
            return false;
            else return true;
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
