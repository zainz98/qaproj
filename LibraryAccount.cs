using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_mstest
{
    public class LibraryAccount
    {
        #region Private Variables
        private const int MAX_NUM_OF_LOAN_BOOKS = 3;
        private Reader owner;
        private List<Book> loanBooks;
        private List<Book> reservedBooks;
        private double ownerDebt; // חוב של בעל כרטיס הספרייה
        #endregion

        #region Properties
        public int MaxNumOfLoanBooks
        {
            get { return MaxNumOfLoanBooks; }
        }
        public Reader Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public List<Book> LoanBooks
        {
            get { return loanBooks; }
            set { loanBooks = value; }
        }
        public List<Book> ReservedBooks
        {
            get { return reservedBooks; }
            set { reservedBooks = value; }
        }
        public double OwnerDebt
        {
            get { return ownerDebt; }
            set { ownerDebt = value; }
        }
        #endregion

        #region Constructors
        public LibraryAccount(Reader owner)
        {
            this.owner = owner;
            loanBooks = new List<Book>();
            reservedBooks = new List<Book>();
            this.ownerDebt = 0;
        }
        #endregion

        #region Methods
        public bool LoanBook(Book bookToLoan)
        {
            //Validations before approving the loan activity
            if (loanBooks.Count == MAX_NUM_OF_LOAN_BOOKS)
            {
                return false;
            }
            if(bookToLoan.Status != Book.BookStatus.InTheLibrary)
            {
                return false;
            }
            if(bookToLoan.Type != Book.BookType.ChildrenBook)
            {
                return false;
            }
            if (this.ownerDebt > 0)
            {
                return false;
            }
            //if the code came here then we can perform the loan activity
            bookToLoan.Status = Book.BookStatus.OutOfTheLibrary;
            LoanBooks.Add(bookToLoan);
            return true;
        }
        public bool ReturnBook(Book bookToReturn)
        {
            if(!LoanBooks.Contains(bookToReturn))
            {
                return false;
            }
            if (bookToReturn.Status == Book.BookStatus.OutOfTheLibrary)
            {
                bookToReturn.Status = Book.BookStatus.InTheLibrary;
            }
            if (bookToReturn.Status == Book.BookStatus.OutOfTheLibraryAndReserved)
            {
                bookToReturn.Status = Book.BookStatus.Reserved;
            }
            LoanBooks.Remove(bookToReturn);
            return true;
        }

        public bool ReserveBook(Book bookToReserve)
        {
            if (bookToReserve.Status == Book.BookStatus.Reserved)
            {
                return false;
            }
            if (bookToReserve.Status == Book.BookStatus.OutOfTheLibraryAndReserved)
            {
                return false;
            }
            if (Owner.Type == Reader.ReaderType.Adult && bookToReserve.Type == Book.BookType.ChildrenBook)
            {
                return false;
            }
            if (Owner.Type != Reader.ReaderType.Adult && bookToReserve.Type != Book.BookType.ChildrenBook)
            {
                return false;
            }
            if (reservedBooks.Count == MAX_NUM_OF_LOAN_BOOKS)
            {
                return false;
            }
            if (this.ownerDebt > 0)
            {
                return false;
            }

            if (bookToReserve.Status == Book.BookStatus.InTheLibrary)
            {
                bookToReserve.Status = Book.BookStatus.Reserved;
            }
            if (bookToReserve.Status == Book.BookStatus.OutOfTheLibrary)
            {
                bookToReserve.Status = Book.BookStatus.OutOfTheLibraryAndReserved;
            }
            reservedBooks.Add(bookToReserve);
            return true;
        }

        public bool CancelReserveBook(Book bookToCancelReserve)
        {
            if (!reservedBooks.Contains(bookToCancelReserve))
            {
                return false;
            }
            if (bookToCancelReserve.Status == Book.BookStatus.Reserved)
            {
                bookToCancelReserve.Status = Book.BookStatus.InTheLibrary;
            }
            if (bookToCancelReserve.Status == Book.BookStatus.OutOfTheLibraryAndReserved)
            {
                bookToCancelReserve.Status = Book.BookStatus.OutOfTheLibrary;
            }
            reservedBooks.Remove(bookToCancelReserve);
            return true;

        }
      #endregion
    }
}
