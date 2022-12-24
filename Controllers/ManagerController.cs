using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers
{
    public class ManagerController : Controller
    {
        private readonly BookDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ManagerController(BookDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            dbContext = context;
            this.webHostEnvironment = webHostEnvironment;
        }



        public async Task<IActionResult> Index()
        {
            if (!checkSession()) return RedirectToAction("Login", "Account");
            var books = await dbContext.Books.ToListAsync();
            return View(books);
        }


        [HttpGet]
        public IActionResult Create()
        {
            if (!checkSession()) return RedirectToAction("Login", "Account");
            ViewBag.Categories = dbContext.Categories.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,PublicDate,CategoryID,Picture,Price")] Book model)
        {
            if (ModelState.IsValid)
            {
                if (model.Picture != null)
                {
                    string? uniqueFileName = null;
                    string ImageUploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                    string filepath = Path.Combine(ImageUploadFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filepath, FileMode.Create))
                    {
                        model.Picture.CopyTo(fileStream);
                    }
                    model.PicturePath = "~/wwwroot/images";
                    model.PictureName = uniqueFileName;
                }
                dbContext.Add(model);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }







        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!checkSession()) return RedirectToAction("Login", "Account");
            if (id == null || dbContext.Books == null)
            {
                return NotFound();
            }

            var book = await dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewBag.Categories = dbContext.Categories.ToList();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Author,PublicDate,CategoryID,Picture,Price")] Book model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string? uniFileName = null;
                    if (model.Picture != null)
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                        uniFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            model.Picture.CopyTo(fileStream);
                        }
                        model.PicturePath = "~/wwwroot/images";
                        model.PictureName = uniFileName;
                    }
                    dbContext.Update(model);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }




        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!checkSession()) return RedirectToAction("Login", "Account");
            if (id == null || dbContext.Books == null)
            {
                return NotFound();
            }

            var book = await dbContext.Books.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            ViewBag.Categories = dbContext.Categories.ToList();

            return View(book);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!checkSession()) return RedirectToAction("Login", "Account");
            if (dbContext.Books == null)
            {
                return Problem("Entity set 'BookContext.Book'  is null.");
            }
            var model = await dbContext.Books.FindAsync(id);
            if (model != null)
            {
                dbContext.Books.Remove(model);
            }

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool checkSession()
        {
            if (HttpContext.Session.GetString("Role") == null || !HttpContext.Session.GetString("Role").Equals("OWNER"))
                return false;
            else return true;
        }


        private bool BookExists(int id)
        {
            return dbContext.Books.Any(e => e.Id == id);
        }
    }
}








