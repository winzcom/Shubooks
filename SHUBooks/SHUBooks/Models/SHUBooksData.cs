using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHUBooks.Models
{
    public class SHUBooksData
    {
        private static ApplicationDbContext context = new ApplicationDbContext();
        
        

        public IList<Subject> GetSubjects() {
            
            var books = context.Subjects;
            return books.ToList<Subject>();
        }

        public void CreateBook(Book NewBook)
        {
            var newRecord = context.Books.Add(NewBook);
            context.SaveChanges();
        }

        /*Get all books record from the database
         * Method Name: GetBooks
         * Method Data Type: Ilist<Book>
         * Parameter Name: Null
         * Parameter Type: Null
         * Return Type: IList
         */
        public IList<Book> GetBooks()
        {
            IList<Book> books = context.Books.ToList<Book>();

            return BookFilter(books);
            //return books.ToList<Book>();
        }


        /*Get unique book record from the database belonging to different Users/Sellers
         * Method Name: GetUniqueISBN
         * Method Data Type: Ilist<Book>
         * Parameter Name: Null
         * Parameter Type: Null
         * Return Type: IList
         */
        /*public IList GetUniqueISBN()
        {
        IQueryable recording;
        
            recording = (from Books in context.Books
            select new {
              Books.ISBN
            }).Distinct();

            return (IList)recording;
        }*/

        public IList<Book> BookFilter (IList<Book> BookList){
            IList<String> ISBNLog = new List<String>();
            //IList<Book> BookList;
            IList<Book> FilterResult = new List<Book>();

            //BookList = GetBooks();
            foreach (var item in BookList){
                if (!ISBNLog.Contains(item.ISBN)){
                    ISBNLog.Add(item.ISBN);
                    FilterResult.Add(item);
                }
            }
            return (FilterResult);
        }


        /*Get specific book record from the database belonging to different Users/Sellers
         * Method Name: GetBook
         * Method Data Type: Ilist<Book>
         * Parameter Name: ISBN
         * Parameter Type: String
         * Return Type: IList
         */
        public IList<Book> GetBook(String ISBN)
        {
            IQueryable<Book> recording;
            recording = (from record in context.Books
                         where record.ISBN == ISBN
                         select record);
            return recording.ToList<Book>();
        }

        /*Get All book record from the database belonging to a specific Sellers
         * Method Name: GetMyBooks
         * Method Data Type: Ilist<Book>
         * Parameter Name: UserId
         * Parameter Type: String
         * Return Type: IList
         */
        public IList<Book> GetMyBooks(String UserId)
        {
            IQueryable<Book> recording;
            recording = (from record in context.Books
                         where record.UserId == UserId
                         select record);
            return recording.ToList<Book>();
        }

        /*Get specific book record from the database belonging to a specific User/Seller
         * Method Name: GetUserBook
         * Method Data Type: Book
         * Parameter Name: BookId
         * Parameter Type: int
         * Return Type: Book
         */
        public Book GetUserBook(int BookId)
        {
            IQueryable<Book> recording;
            recording = (from record in context.Books
                         where record.BookId == BookId
                         select record);
            return recording.ToList<Book>().First();
        }


        /*Search for book record from the database based on Title, Author, ISBN, rice
         * Method Name: SearchBook
         * Method Data Type: Ilist<Book>
         * Parameter Name: searchString, searchBy
         * Parameter Type: String, int
         * Return Type: IList
         */
        public IList<Book> SearchBook(String searchString, int searchBy)
        {
            IQueryable<Book> recording;
            switch (searchBy)
            {
                case 1: //Search by Title
                    {
                        recording = (from record in context.Books
                                     where record.Title.Contains(searchString)
                                     select record);
                        break;
                    }
                case 2: //Search by ISBN
                    {
                        recording = (from record in context.Books
                                     where record.ISBN.Contains(searchString)
                                     select record);
                        break;
                    }
                case 3: //Search by Author
                    {
                        recording = (from record in context.Books
                                     where record.Author.Contains(searchString)
                                     select record);
                        break;
                    }
                case 4: //Search by Subject
                    {
                        recording = (from record in context.Books
                                     where record.Publisher.Contains(searchString)
                                     select record);
                        break;
                    }
                default: //Search by Title
                    {
                        recording = (from record in context.Books
                                     select record);
                        break;
                    }
            }
            return BookFilter(recording.ToList<Book>());
            //return recording.ToList<Book>();
        }


        /*Edit a specific User/Seller book record and update the database
         * Method Name: EditUserBook
         * Method Data Type: Void
         * Parameter Name: EditRecord
         * Parameter Type: Book
         * Return Type: Void
         */
        public void EditUserBook(Book EditRecord)
        {
            Book recording;
            recording = (from record in context.Books
                         where record.BookId == EditRecord.BookId
                         select record).ToList<Book>().First();

            recording.Author = EditRecord.Author;
            recording.CopyrightDate = EditRecord.CopyrightDate;
            recording.Description = EditRecord.Description;
            recording.ISBN = EditRecord.ISBN;
            recording.Condition = EditRecord.Condition;
            recording.Age = EditRecord.Age;
            recording.Pages = EditRecord.Pages;
            recording.Picture = EditRecord.Picture;
            recording.Price = EditRecord.Price;
            recording.Publisher = EditRecord.Publisher;
            recording.QuantityInStock = EditRecord.QuantityInStock;
            recording.SubjectId = EditRecord.SubjectId;
            recording.Title = EditRecord.Title;

            context.SaveChanges();
        }


        /*Delete a specific User/Seller book record and update the database
         * Method Name: DeleteUserBook
         * Method Data Type: Void
         * Parameter Name: DeleteRecord
         * Parameter Type: Book
         * Return Type: Void
         */
        public void DeleteUserBook(Book DeleteRecord)
        {
            context.Books.Remove(DeleteRecord);
            context.SaveChanges();
        }


        /*Get all orders in the database
         * Method Name: GetOrders
         * Method Data Type: IList
         * Parameter Name: Null
         * Parameter Type: Null
         * Return Type: IList
         */
        public IList<Orders> GetOrders()
        {
            return context.Orders.ToList<Orders>();
        }

        /*Get orders in the database belonging to a user
         * Method Name: GetUserOrders
         * Method Data Type: IList
         * Parameter Name: UserId
         * Parameter Type: string
         * Return Type: IList
         */
        public IList<Orders> GetOrders(String UserId)
        {
            IQueryable<Orders> recording;
            recording = (from record in context.Orders
                         where record.UserId == UserId
                         select record);

            return recording.ToList<Orders>();
        }

        /*Get best selling books from the orders in the database
         * Method Name: GetBestSellers
         * Method Data Type: IList
         * Parameter Name: Null
         * Parameter Type: Null
         * Return Type: IList
         */
        public IList<BestSeller> GetBestSellers()
        {
           
            IQueryable<BestSeller> result = from ord in context.Orders
                                            join bk in context.Books on ord.BookId equals bk.BookId
                                            group ord by bk
                                                into orderTotals
                                                select new BestSeller
                                                {
                                                    BookId = orderTotals.Key.BookId,
                                                    Title = orderTotals.Key.Title,
                                                    Total = orderTotals.Sum(o => o.Quantity)
                                                };

            return result.ToList<BestSeller>();
        }

        

        public IList<BestSeller> GetBestSellersMerge()
        {
            IQueryable<BestSeller> result = from ord in context.Orders
                                            join bk in context.Books on ord.BookId equals bk.BookId
                                            group ord by bk
                                                into orderTotals
                                                select new BestSeller
                                                {
                                                    BookId = orderTotals.Key.BookId,
                                                    Title = orderTotals.Key.Title,
                                                    Total = orderTotals.Sum(o => o.Quantity)
                                                };

            return result.ToList<BestSeller>();
        }



        /*Insert the cart list items into the order table and decrease the available quantity in Book table
         
         *Method Name : PlaceOrder
         *Method Data Type : void
         *Parameter : Ilist<item>
         *Return Type : null
         */
        public void PlaceOrder(IList<Item> cartItems, String BuyerId)
        {

            foreach (Item item in cartItems)
            {
                context.Orders.Add(

                        new Orders
                        {

                            OrderDate = DateTime.Now,
                            Quantity = item.quantity,
                            BookId = item.book.BookId,
                            UserId = item.book.UserId,
                            BuyerId = BuyerId,
                            Status = 0
                        }
                    );
                item.book.QuantityInStock = item.book.QuantityInStock - item.quantity;
                /*Book query = (from book in context.Books
                              where book.ISBN == item.book.ISBN
                             select book).First();
                query.QuantityInStock = item.book.QuantityInStock;*/

                //context.Entry((Book)item.book).State = System.Data.Entity.EntityState.Modified;
            }
            context.SaveChanges();
        }

        public IList<OrderHistory> GetPurchaseHistory(String BuyerId)
        {
            IQueryable<OrderHistory> result = (from ord in context.Orders
                                            join bk in context.Books on ord.BookId equals bk.BookId
                                              where ord.BuyerId == BuyerId
                                            select new OrderHistory
                                                {
                                                    Author = bk.Author,
                                                    Title = bk.Title,
                                                    ISBN = bk.ISBN,
                                                    Picture = bk.Picture,
                                                    Price = bk.Price,
                                                    Quantity = ord.Quantity,
                                                    Status = ord.Status,
                                                    BookId = ord.BookId,
                                                    OrderId = ord.OrderId
                                                }).OrderByDescending(x => x.OrderId);
            return result.ToList<OrderHistory>();
        }

        public IList<OrderHistory> GetSalesHistory(String UserId)
        {
            IQueryable<OrderHistory> result = (from ord in context.Orders
                                              join bk in context.Books on ord.BookId equals bk.BookId
                                              where ord.UserId == UserId
                                              select new OrderHistory
                                              {
                                                  OrderId = ord.OrderId,
                                                  Author = bk.Author,
                                                  Title = bk.Title,
                                                  ISBN = bk.ISBN,
                                                  Picture = bk.Picture,
                                                  Price = bk.Price,
                                                  Quantity = ord.Quantity,
                                                  Status = ord.Status,
                                                  BookId = ord.BookId
                                                }).OrderByDescending(x => x.OrderId);
                                              
            return result.ToList<OrderHistory>();
        }

        //Get contact details of the book seller
        public Seller GetBookSeller(int BookId)
        {
            IQueryable<Seller> result = (from bk in context.Books
                                               join usr in context.Users on bk.UserId equals usr.Id
                                         where bk.BookId == BookId
                                               select new Seller
                                               {
                                                   FirstName = usr.FirstName,
                                                   SurName = usr.SurName,
                                                   Id = usr.Id,
                                                   Email = usr.Email,
                                                   PhoneNo = usr.PhoneNo,
                                                   BookTitle = bk.Title,
                                                   BookISBN = bk.ISBN,
                                                   BookId = bk.BookId,
                                                   BookPicture = bk.Picture
                                               });
            //recording.ToList<Book>().First()
            return result.ToList<Seller>().First();
        }


        public void ConfirmPayment(int OrderId)
        {

            Orders order =  context.Orders.Where(x => x.OrderId == OrderId).FirstOrDefault();
            Book book = context.Books.Find(order.BookId);
            book.QuantityInStock = book.QuantityInStock - order.Quantity;
            order.Status = 1;
            context.SaveChanges();
        }


    }
}