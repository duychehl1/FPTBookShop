using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShop.Models;
using BookShop.Data;

namespace BookShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BookDbContext dbContext;
        public CategoryController(BookDbContext categoryContext)
        {
            dbContext = categoryContext;
        }



        public async Task<IActionResult> Index()
        {
            var categorie = await dbContext.Categories.ToListAsync();
            return View(categorie);
            /*            return dbContext.Categories != null ? View(await dbContext.Categories.ToListAsync()) :  Problem("Entity set.");
            */
        }




        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(category);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }













        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? categoryid)
        {
            if (categoryid == null || dbContext.Categories == null)
            {
                return NotFound();
            }

            var category = await dbContext.Categories.FindAsync(categoryid);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? categoryid, [Bind("CategoryID,CategoryName")] Category category)
        {
            if (categoryid != category.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(category);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryID))
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
            return View(category);
        }





        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? categoryid)
        {
            if (categoryid == null || dbContext.Categories == null)
            {
                return NotFound();
            }

            var category = await dbContext.Categories.FirstOrDefaultAsync(m => m.CategoryID == categoryid);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int categoryid)
        {
            if (dbContext.Categories == null)
            {
                return Problem("Entity set 'Category'  is null.");
            }
            var category = await dbContext.Categories.FindAsync(categoryid);
            if (category != null)
            {
                dbContext.Categories.Remove(category);
            }

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int categoryid)
        {
            return dbContext.Categories.Any(e => e.CategoryID == categoryid);
        }
    }
}
