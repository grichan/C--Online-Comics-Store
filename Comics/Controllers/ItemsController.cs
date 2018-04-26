using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Comics.Models;
using Comics.ViewModels;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.IO;

namespace Comics.Controllers
{


    public class ItemsController : Controller
    {

        private MyDbContext _context;
        // enable-migrations (first time )
        // add-­migration SomeChangesName         // update-database


        public ItemsController()
       {
            _context = new MyDbContext(); // new db conn
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("index")]
        [Route("")]
        [Route("home")]
        public ActionResult Index()
        {



            Items[] item_array = new Items[4];
            item_array = _context.Items.OrderBy(item => item.id).ToArray();
            Array.Reverse(item_array);
            Array.Resize(ref item_array , 4);

            return View(item_array);
        }

        [Route("list")]
        public ActionResult List()
        {
            var item = _context.Items.ToList();
            var viewModel = new ItemsList
            {
                items_list = item //sets list from db to list in viewModel

            };
            return View(viewModel);
        }

        [Route("details/{name}")]
        public ActionResult Details(int name)
        {

            var name1 = _context.Items.FirstOrDefault(Item => Item.id == name);

            return View(name1);

        }

        [Route("Add")]
        public ActionResult Add()
        {


            return View();

        }

        [Route("Checkout")]
        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Order(OrderViewModel Model) // receives a view model (string) ( must be same name in post-ajax)
        {

            if (Model.balance != null)
            {
                int[] IdArray = new int[Model.balance.Length];
                Items[] itemArray = new Items[Model.balance.Length];

                var i = 0;
                decimal total = 0;
                foreach (var Obj in Model.balance)
                {

                    IdArray[i] = int.Parse(Model.balance[i]); // parse(convert) models string var -> int
                    var id = IdArray[i];
                    itemArray[i] = _context.Items.FirstOrDefault(Item => Item.id == id); // add item objects to array 
                    total += itemArray[i].price; // total price
                    i++;
                }

                Orders order = new Orders(); // create new order
                string ids = string.Join(",", IdArray);
                order.product_ids = ids;
                order.username = Session["User"].ToString();
                order.total = total;
                order.qty = IdArray.Length;
                order.date = DateTime.Now;

                _context.Orders.Add(order); // add order
                _context.SaveChanges();

                return Json("Order Completed");
            }
            else
            {
                return Json("Could Not Place Order");
            }



        }

        [HttpPost]
        public JsonResult UploadFile(AddViewModel Model)
                    {
                    try
                    {

                        if (String.IsNullOrWhiteSpace(Model.category) || Model.qty <= 0
                        || Model.qty <= 0 || String.IsNullOrWhiteSpace(Model.title) || Model.file.ContentLength <= 0)  // not empty check 
                        {
                            return Json("Check The Fields");
                        }
                        else
                        {

                            var item = _context.Items.FirstOrDefault(Item => Item.name == Model.title);
                            if (item == null)
                            {
                                Items newItem = new Items();
                                string _FileName = Path.GetFileName(Model.file.FileName);
                                string _path = Path.Combine(Server.MapPath("~/Content/Images"), _FileName);
                                Model.file.SaveAs(_path); // FILE SAVED!

                                newItem.name = Model.title.ToString();
                                newItem.price = Model.price;
                                newItem.qty = Model.qty;
                                newItem.category = Model.category.ToString();
                                newItem.iamgeURL = _FileName;

                                _context.Items.Add(newItem); // add User
                                _context.SaveChanges();
                                return Json("Created");

                            }
                            else 
                            {
                                return Json("Already Exists" );
                            }
                        }
                    }

                    catch
                        {
                        return Json("ADDING FALED !!");
                        }
                    }
        


            } // END OF ITEMS CONTROLLER 
} 