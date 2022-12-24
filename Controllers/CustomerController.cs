using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace BookShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly BookDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CustomerController(BookDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            dbContext = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        //xem all
        public async Task<IActionResult> Index()
        {
			CategoryList categoryList = new CategoryList();
			categoryList.LCategory = dbContext.Categories.ToList();
			categoryList.LBook = dbContext.Books.ToList();
			//var books = await dbContext.Books.ToListAsync();
            return View(categoryList);
        }


        public async Task<IActionResult> Category(int? id)
        {
            CategoryList categoryList = new CategoryList();
            categoryList.LCategory = dbContext.Categories.ToList();
            if(id == null)
            {
				categoryList.LBook = dbContext.Books.ToList();
			}
            else
            {
				categoryList.LBook = dbContext.Books.Where(c => c.CategoryID == id).ToList();

			}

			return View(categoryList);
        }






        //xem chi tiet

        public IActionResult Detail(int id)
        {
            var book = dbContext.Books.FirstOrDefault(c => c.Id == id);
            return View(book);
        }
        // dang nhap
        public IActionResult Login()
        {
            return RedirectToAction("Login", "Account");
        }
        // profile
        [HttpGet]
        public async Task<IActionResult> Profile(int id)
        {            
            var customer = await dbContext.Customers.FindAsync(id);
            var viewModel = new CustomerViewModel
            {
                CusId = customer.CusId,
                Name = customer.Name,
                DateOfBirth = customer.DateOfBirth,
                Gender = customer.Gender,
                Email = customer.Email,
                Address = customer.Address,
                CustomerPicture = customer.CustomerPicture,
                UploadPicture = null,
                Account = customer.Account,
            };
            return View(viewModel);         
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(CustomerViewModel editedCustomer)
        {            
                //xu li anh tai len neu dang anh moi
                if (editedCustomer.UploadPicture != null)
                {
                string wwwPath = this.webHostEnvironment.WebRootPath;
                string contentPath = this.webHostEnvironment.ContentRootPath;
                string path = Path.Combine(this.webHostEnvironment.WebRootPath, "uploads");
                string fileName = Path.GetFileName(editedCustomer.UploadPicture.FileName);
                editedCustomer.CustomerPicture = fileName;
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        //copy anh va lay ten anh
                        editedCustomer.UploadPicture.CopyTo(stream);
                        editedCustomer.CustomerPicture = fileName;
                    }
                } 
                
                // tao cus moi de update
                Customer updatedCustomer = new Customer
                {
                    CusId = editedCustomer.CusId,
                    Name = editedCustomer.Name,
                    DateOfBirth = editedCustomer.DateOfBirth,
                    Gender = editedCustomer.Gender,
                    Email = editedCustomer.Email,
                    Address = editedCustomer.Address,
                    CustomerPicture = editedCustomer.CustomerPicture,
                    Account = editedCustomer.Account,
                };
                dbContext.Update(updatedCustomer);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));           
        }

        //search 
        public async Task<IActionResult> Search(string Search)
        {
            var books = from m in dbContext.Books select m;

            if (!String.IsNullOrEmpty(Search))
            {
                books = books.Where(x => x.Title.Contains(Search) || x.Price.ToString().Contains(Search) ||
                x.CategoryID.ToString().Contains(Search));
            }
            /*return View(await _context.Movie.ToListAsync());*/
            return View(await books.ToListAsync());
        }

       

    }
}
