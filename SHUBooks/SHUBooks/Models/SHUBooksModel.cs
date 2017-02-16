using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SHUBooks.Models
{
    public class Book
    {
        public string Author { set; get; }
        public string Title { set; get; }
        public string Publisher { set; get; }
        public string CopyrightDate { set; get; }
        public string Pages {set; get;}
        [Display(Name = "Condition")]
        public string Condition { set; get; }

        public string Description { set; get; }

        //public ICollection<Subject> Subjects { set; get; }

        public int  SubjectId { get; set; }
        public virtual Subject Subject { set; get; }

        //public string there { get; set; }

       //[Key]
        public string ISBN { set; get; }

        [DataType(DataType.ImageUrl)]
        public string Picture { set; get; }
        [Display(Name = "Price (£)")]
        public double Price { set; get; }

        [Display(Name = "# Available")]
        public int QuantityInStock { set; get; }

        public virtual ApplicationUser User { set; get; }
        //14 July 2015
        public string UserId { get; set; }
        [Key]
        public int BookId { set; get; }
        public String Age { set; get; }
    }

    public class Subject
    {
        [Display(Name = "Subject")]
        public string Title { set; get; }
        public int Id { set; get; }

        public virtual ICollection<Book> Books { set; get; }
    }


    public class Orders
    {
        [Key]
        public int OrderId { set; get; }
        public virtual ApplicationUser User { set; get; }
        public string UserId { set; get; }
        public string BuyerId { set; get; }
        public int BookId { get; set; }
        public string BookISBN { set; get; }
        public int Status { set; get; }
        public int Quantity { set; get; }
        public DateTime OrderDate { set; get; }
    }


    public class Rating
    {
        
        public int Id { set; get; }
        public Book Book { set; get; }
        public int Score { set; get; }

        public int BookId { get; set; }
        public string BookISBN { get; set; }
        public string UserId { get; set; }
        //public int CummulativeRate { set; get; }

        //public double Ratings { set; get; }
    }

    public class BestSeller
    {
        public int BookId { get; set; }
        public int Total { get; set; }
        public String Title { get; set; }
        public String BookISBN { get; set; }


        internal IList<BestSeller> ToList<T1>()
        {
            throw new NotImplementedException();
        }
    }


    public class OrderHistory
    {
        public OrderHistory()
        {

        }
        public string Author { set; get; }
        public string Title { set; get; }

        public string ISBN { set; get; }
        public int OrderId { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Picture { set; get; }
        [Display(Name = "Price (£)")]
        public double Price { set; get; }

        [Display(Name = "Qty")]
        public int Quantity { set; get; }
        public int BookId { get; set; }
        public int Status { get; set; }

        internal IList<OrderHistory> ToList<T1>()
        {
            throw new NotImplementedException();
        }
    }


    public class Item
    {
        public Item(Book book, int quantity)
        {
            this.book = book;
            this.quantity = quantity;
        }

        public Item()
        {

        }
        public Book book { get; set; }
        public int quantity { get; set; }
    }


    public class Seller
    {
        public Seller()
        {

        }
        [Display(Name = "First Name")]
        public string FirstName { set; get; }

        [Display(Name = "Surname")]
        public string SurName { set; get; }

        public string Id { set; get; }

        [Display(Name = "Book ISBN")]
        public string BookISBN { get; set; }

        [DataType(DataType.ImageUrl)]
        public string BookPicture { set; get; }

        [Display(Name = "Book Title")]
        public string BookTitle { set; get; }

        [Display(Name = "Phone No")]
        public string PhoneNo { set; get; }

        public int BookId { get; set; }

        public string Email { get; set; }

        internal IList<Seller> ToList<T1>()
        {
            throw new NotImplementedException();
        }
    }


}