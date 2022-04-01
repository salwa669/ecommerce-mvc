using Microsoft.AspNetCore.Mvc;
using Online_Shoping.Models;
using Online_Shoping.Reporistry;

namespace Online_Shoping.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepositry orderRepositry;
        public OrderController(IOrderRepositry _orderRepositry)
        {
            orderRepositry = _orderRepositry;
        }

        //public IActionResult Index()
        //{
        //    List<Order> OrderList = orderRepositry.GetAll();
        //    return View(OrderList);
        //}
        //Show All Orders
        public IActionResult ShowOrders()
        {
            return View("ShowOrders", orderRepositry.GetAll());
        }
        public IActionResult CreateNewOrder()
        {
            return View("CreateNewOrder");
        }
        //save
        [HttpPost]
        public IActionResult SaveNewOrder(Order newOrder)
        {
            if (ModelState.IsValid == true)
            {
                orderRepositry.insert(newOrder);
                return RedirectToAction("ShowOrders");
            }

            return View("CreateNewOrder", newOrder);

        }

        public IActionResult Edit(int id)
        {
            Order Order = orderRepositry.GetById(id);
            return View("Edit", Order);

        }

        [HttpPost]
        public IActionResult SaveEdit(int id, Order NewOrder)
        {
            if (ModelState.IsValid == true)
            {
                //save
                orderRepositry.update(id, NewOrder);
                return RedirectToAction("Index");
            }
            //not saved

            return View("Edit", NewOrder);
        }
        public IActionResult RemoveOrder(int id)
        {
            if (ModelState.IsValid)
            {
                orderRepositry.delete(id);
                return RedirectToAction("ShowOrders");



            }
            return RedirectToAction("ShowOrders");
        }
    }
}
