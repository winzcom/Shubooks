using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHUBooks.Models;
using PagedList;

/******* 14 July 2014 Begin ***************/
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
/******* End *****************************/

namespace SHUBooks.Controllers
{
    public class HomeController : Controller
    {
        SHUBooksService service = new SHUBooksService();

        /**********14 July 2014 Begin ************/
        protected ApplicationDbContext AppDbContext { get; set; }
        protected UserManager<ApplicationUser> MyUserManager { get; set; }

        public HomeController()
        {
            this.AppDbContext = new ApplicationDbContext();
            this.MyUserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.AppDbContext));
        }
        /***********End*******************/
        
        public ActionResult Index()
        {
            //service.BookFilter();
            
            ViewBag.BSeller = service.GetBestSellers().OrderByDescending(x => x.Total).Take(10);
            
            ViewBag.Quantity = SumOfItems();
            return View();
        }

        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult CreateBook()
        {
            var subjectCat = service.GetSubjects();
            var subjects = (from a in subjectCat
                        select new SelectListItem
                        {
                            Text = a.Title,
                            Value = a.Id.ToString(),
                            Selected = false
                        });

            ViewBag.Subjects = subjects;
            return View();
        }
        [HttpPost]
        public ActionResult CreateBook(Book NewBook, HttpPostedFileBase Image)
        {
            var subjectCat = service.GetSubjects();
            var subjects = (from a in subjectCat
                            select new SelectListItem
                            {
                                Text = a.Title,
                                Value = a.Id.ToString(),
                                Selected = false
                            });
            if (Image != null)
            {

                string pic = System.IO.Path.GetFileName(Image.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/uploads"), pic);

                Image.SaveAs(path);
                NewBook.Picture = "~/uploads/" + Image.FileName;
            }

            var loginUser = MyUserManager.FindById(User.Identity.GetUserId());
            NewBook.UserId = loginUser.Id;

            service.CreateBook(NewBook);
            ViewBag.Subjects = subjects;
            return View();
        }

       

        public ActionResult Subject()
        {
           
            return View(service.GetSubjects());
        }

        /************ 14 July 2015 Begin**************************/
        public ActionResult ViewBooks(int? Page)
        {
            ViewBag.Quantity = SumOfItems();
            int pageNumber = (Page ?? 1);
            return View(service.GetBooks().ToPagedList(pageNumber, SetPagger()));
        }

        [HttpPost]
        public ActionResult ViewBooks(int? Page, String searchString, int searchby)
        {
            ViewBag.Quantity = SumOfItems();
            int pageNumber = (Page ?? 1);
            return View(service.SearchBook(searchString, searchby).ToPagedList(pageNumber, SetPagger()));
        }

        [Authorize]
        public ActionResult ViewMyBooks()
        {
            ViewBag.Quantity = SumOfItems();
            var loginUser = MyUserManager.FindById(User.Identity.GetUserId());
            return View(service.GetMyBooks(loginUser.Id));
        }

        public ActionResult ViewBook(int? Page, String ISBN)
        {
            ViewBag.Quantity = SumOfItems();
            int pageNumber = (Page ?? 1);
            return View(service.GetBook(ISBN).ToPagedList(pageNumber, SetPagger()));
        }

        public ActionResult ViewUserBook(int BookId)
        {
            return View(service.GetUserBook(BookId));
        }

        public ActionResult EditUserBook(int BookId)
        {
            var subjectCat = service.GetSubjects();
            var subjects = (from a in subjectCat
                            select new SelectListItem
                            {
                                Text = a.Title,
                                Value = a.Id.ToString(),
                                Selected = false
                            });

            ViewBag.Subjects = subjects;
            ViewBag.Quantity = SumOfItems();
            Book UserBookRecord = new Book();
            UserBookRecord = service.GetUserBook(BookId);
            return View(UserBookRecord);
        }
        
        [HttpPost]
        public ActionResult EditUserBook(Book record)
        {
            service.EditUserBook(record);
            ViewBag.Quantity = SumOfItems();
            return RedirectToAction("ViewBooks", "Home");
        }

        public ActionResult BestSeller()
        {
            ViewBag.TrckList = service.GetBestSellers();
            ViewBag.Quantity = SumOfItems();
            return View(service.GetBestSellers());
            
        }


        //Cart Begin Here
        public ActionResult Cart(int id = 0)
        {

            Book book;
            if (id != 0)
            {
                 book = service.GetUserBook(id);
                if (Session["cart"] == null)
                {
                    IList<Item> cart = new List<Item>();
                    cart.Add(new Item(book, 1));
                    Session["cart"] = cart;
                }
                else
                {


                    IList<Item> cart = (IList<Item>)Session["cart"];
                    Item item = cart.Where(x => x.book.BookId == id).FirstOrDefault();
                    if (item != null)
                    {
                        item.quantity++;
                        Session["cart"] = cart;
                    }

                    else { cart.Add(new Item(book, 1)); Session["cart"] = cart; }
                }

                if (Request.IsAjaxRequest())
                    return Json("new", JsonRequestBehavior.AllowGet);
                ViewBag.Quantity = SumOfItems();
                return View(Session["cart"]);

            }

            else if (Session["cart"] != null)
            {
                ViewBag.Quantity = SumOfItems();
                return View((IList<Item>)Session["cart"]);
            }
                

            return RedirectToAction("ViewBooks");
        }


        public ActionResult CheckOut()
        {
            var loginUser = MyUserManager.FindById(User.Identity.GetUserId());
            //NewBook.UserId = loginUser.Id;
            
            if (Session["cart"] != null)
            {
                service.PlaceOrder((IList<Item>)Session["cart"], loginUser.Id);
                Session["cart"] = null;
            }
            return RedirectToAction("PurchaseHistory");
        }

        public ActionResult EditCart(int id)
        {
            IList<Item> cart = (IList<Item>)Session["cart"];
            Item item = cart.Where(x => x.book.BookId == id).FirstOrDefault();
            ViewBag.Quantity = SumOfItems();
            return View(item);
        }


        [HttpPost]
        public ActionResult EditCart(Item item)
        {
            IList<Item> cart = (IList<Item>)Session["cart"];
            Item cartitem = cart.Where(x => x.book.BookId == item.book.BookId).FirstOrDefault();
            cartitem.quantity = item.quantity;
            return RedirectToAction("Cart");
        }


        public ActionResult DeleteItemFromCart(int id)
        {

            IList<Item> carts = (IList<Item>)Session["cart"];
            Item cartitems = carts.Where(x => x.book.BookId == id).FirstOrDefault();
            carts.Remove(cartitems);
            if (Request.IsAjaxRequest())
                return Json("success", JsonRequestBehavior.AllowGet);
            return View(cartitems);
        }

        [HttpPost]
        public ActionResult DeleteItemFromCart(Item item)
        {
            IList<Item> cart = (IList<Item>)Session["cart"];
            Item cartitem = cart.Where(x => x.book.BookId == item.book.BookId).FirstOrDefault();
            if (cartitem != null)
                cart.Remove(cartitem);
            return RedirectToAction("Cart");
        }

        public ActionResult PurchaseHistory(int BookId = 0)
        {
            var loginUser = MyUserManager.FindById(User.Identity.GetUserId());
            if (BookId != 0)
            {
                ViewBag.BookSeller = service.GetBookSeller(BookId);
                //ViewBag.ContactSeller = Contact;
            }
            ViewBag.PHistory = service.GetPurchaseHistory(loginUser.Id);
            ViewBag.Quantity = SumOfItems();
            return View();
        }

        

        public ActionResult SalesHistory()
        {
            var loginUser = MyUserManager.FindById(User.Identity.GetUserId());
            ViewBag.PHistory = service.GetSalesHistory(loginUser.Id);
            ViewBag.Quantity = SumOfItems();
            return View();
        }

        public ActionResult ConfirmPayment(int id)
        {
            service.ConfirmPayment(id);
            if (Request.IsAjaxRequest())
            {

                return Json("Confirm", JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("SalesHistory");
        }


        public int SetPagger()
        {
            int Pagger = 5;
            return Pagger;
        }

        private String SumOfItems()
        {

            string sumOfQuantities = "";
            if(Session["cart"] != null)
            {
                IList<Item> items = (IList<Item>)Session["cart"];
                sumOfQuantities = items.Sum(x => x.quantity).ToString();
            }
            return sumOfQuantities;
        }

    }
}