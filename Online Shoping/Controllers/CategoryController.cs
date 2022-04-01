using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Online_Shoping.Hubs;
using Online_Shoping.Models;
using Online_Shoping.Reporistry;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Shoping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IHubContext<CategoryHub> categoryHub;
        private readonly ICatogory categoryReprositry;

        public CategoryController(ICatogory categoryReprositry, IHubContext<CategoryHub> categoryHub)
        {
            this.categoryReprositry = categoryReprositry;
            this.categoryHub = categoryHub;
        }
        public IActionResult Index()
        {
            List<Category> categories = categoryReprositry.GetAll();
            return View(categories);
        }
        public IActionResult Indexx()
        {
            List<Category> categories = categoryReprositry.GetAll();
            return View("_NavView", categories);
        }
        public IActionResult showCategory()
        {
            List<Category> categories = categoryReprositry.GetAll();
            //ViewBag.Cagts = categories;
            return View(categories);
        }
        public IActionResult CreateCategory()
        {
            List<Category> categories = categoryReprositry.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategoryAsync(Category C)
        {
            if (ModelState.IsValid == true)
            {
                await categoryHub.Clients.All.SendAsync("AddNewCategory", C);
                    categoryReprositry.insert(C);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("name", "Category Name is invalid");
            return View("CreateCategory", C);
        }
        public IActionResult Edit(int id)
        {
            Category category = categoryReprositry.GetById(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult SaveEdit(int id, Category C)
        {
            if (ModelState.IsValid == true)
            {
                categoryReprositry.update(id, C);
                return RedirectToAction("Index");
            }
            return View("Edit", C);
        }
        public IActionResult Delete(int id)
        {

            categoryReprositry.delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult SearchCatId(int cattid)
        {
            List<Product> products =
               categoryReprositry.GetProductsByCatId(cattid);
            return Json(products);
        }
    }
}
