using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Online_Shoping.Hubs;
using Online_Shoping.Models;
using Online_Shoping.Reporistry;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Shoping.Controllers
{
    public class ProductController : Controller
    {

        IProduct p;
        ICatogory c;//cateogry interface
        private readonly IHubContext<ProductHub> productHub;
        public ProductController(IProduct pp,ICatogory cc, IHubContext<ProductHub> productHub)
        {
            p = pp;
            c = cc;
            this.productHub = productHub;
        }
        public IActionResult Index()
        {
            List<Product> Products = p.GetAll();
            return View(Products);
        }
        public IActionResult GetProductById(int ID)
        {
            Product product = p.GetById(ID);
            return View();
        }
        public IActionResult NewProduct()
        {
            List<Category> catogList = c.GetAll();
            ViewData["catog"] = catogList;
           
            return View(new Product());
        }
        [HttpPost]
        public async Task<IActionResult> NewProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid==true)
                {
                    int effectedrows = p.insert(product);
                    await productHub.Clients.All.SendAsync("AddNewProduct", product);
                    if (effectedrows >= 1)
                    {
                        return RedirectToAction("index");
                    }
                    ViewBag.error = "an error occured";

                }
                return View(product);
            }catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(product);
            }
        }
        public IActionResult Edit(int id)
        {
            Product product = p.GetById(id);
            List<Category> catoglist=c.GetAll();
            ViewData["catog"] = catoglist;
            return View(product);
        }
        [HttpPost]
        public IActionResult SaveEdit(int ID,Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int efectedrows = p.update(ID, product);
                    if (efectedrows >= 1)
                    {
                        return RedirectToAction("index");
                    }
                    ViewBag.error = "an error occured";

                }
                return View("Edit", product);
            }catch(Exception ex)
            {
                ViewBag.message=ex.Message;
                return View("Edit", product);
            }
            //return View();
        }
        public IActionResult Delete(int ID)
        {
            if(ID != 0)
            {
               
                Product product = p.GetById(ID);
                if (product != null)
                {
                    List<Category> categories = c.GetAll();
                    ViewData["catog"] = categories;
                    return View(product);
                }
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int ID)
        {
            
            
            int effectedrow = p.delete(ID);
            if (effectedrow >= 1)
            {
                return RedirectToAction("index");
            }
            
            return RedirectToAction("Delete",new {id =ID});
        }

    }
}
