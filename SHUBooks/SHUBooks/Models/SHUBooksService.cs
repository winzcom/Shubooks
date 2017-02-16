using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SHUBooks.Models
{
    public class SHUBooksService
    {
        SHUBooksData data = new SHUBooksData();

        public IList<Subject> GetSubjects(){

            return data.GetSubjects();
        }

        public void CreateBook(Book NewBook)
        {
            data.CreateBook(NewBook);
        }

        ///////////// 14 July 2015 //////////////////////////
        /*Get all books record from the database
         * Method Name: GetBooks
         * Method Data Type: Ilist<Book>
         * Parameter Name: Null
         * Parameter Type: Null
         * Return Type: IList
         */
        public IList<Book> GetBooks()
        {
            return data.GetBooks();
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
            return data.GetBook(ISBN);
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
            return data.GetUserBook(BookId);
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
            return data.SearchBook(searchString, searchBy);
        }

        
        
        /*Edit a specific User/Seller book record and update the database
         * Method Name: EditUserBook
         * Method Data Type: void
         * Parameter Name: EditRecord
         * Parameter Type: Book
         * Return Type: Void
         */
        public void EditUserBook(Book EditRecord)
        {
            data.EditUserBook(EditRecord);
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
            data.DeleteUserBook(DeleteRecord);
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
            return data.GetBestSellers();
        }

        public IList<BestSeller> GetBestSellersMerge()
        {
            return data.GetBestSellersMerge();
        }


        /*Insert the cart list items into the order table and decrease the available quantity in Book table
         
        *Method Name : PlaceOrder
        *Method Data Type : void
        *Parameter : Ilist<item>
        *Return Type : null
        */

        public void PlaceOrder(IList<Item> cartItems, String BuyerId)
        {
            this.data.PlaceOrder(cartItems, BuyerId);
        }

        public IList<OrderHistory> GetPurchaseHistory(String BuyerId)
        {
            return data.GetPurchaseHistory(BuyerId);
        }

        public IList<OrderHistory> GetSalesHistory(String UserId)
        {
            return data.GetSalesHistory(UserId);
        }

        public void ConfirmPayment(int OrderId)
        {
            data.ConfirmPayment(OrderId);
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
            return data.GetMyBooks(UserId);
        }


        public Seller GetBookSeller(int BookId)
        {
            return data.GetBookSeller(BookId);
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
            return data.GetUniqueISBN();
        }*/







    }
}