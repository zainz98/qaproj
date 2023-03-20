using  Microsoft.VisualStudio.TestTools.UnitTesting;
using  System;

namespace Library_mstest.UnitTests
{
    [TestClass]
    public class LibraryAccountTests_mstest
    {

        private TestContext context;

        public TestContext TestContext
        {
            get { return context; }
            set { context = value; }
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\XMLTEST\XMLTEST_LIBARY.xml"
            , "ReturnBooktest",
            DataAccessMethod.Random)]

        public void ReturnBooktetsxml()
        {
            //Arrnge

            Reader childReader = new Reader("Lior", "BB", Reader.ReaderType.Child);
            LibraryAccount libraryAccount = new LibraryAccount(childReader);
            Book childrenBook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.InTheLibrary);
            libraryAccount.LoanBook(childrenBook);
            bool actual;
           
             string Status1TEST = TestContext.DataRow["Status1"].ToString();
            string expectedresult = TestContext.DataRow["expectedresult"].ToString();
            bool answer = (expectedresult.ToLower() == "false") ? false : true;

            //Act

             if (Book.BookStatus.OutOfTheLibrary.ToString() == Status1TEST)
             {
                    childrenBook.Status = Book.BookStatus.OutOfTheLibrary;
                answer = false;
             }
             else if (Book.BookStatus.OutOfTheLibraryAndReserved.ToString() != Status1TEST)
             {
                    childrenBook.Status = Book.BookStatus.OutOfTheLibraryAndReserved;
                answer = false;

             }
             else if (Book.BookStatus.InTheLibrary.ToString() == Status1TEST)
            {
                childrenBook.Status = Book.BookStatus.InTheLibrary;
                answer = false;

            }
            else if (Book.BookStatus.Reserved.ToString() == Status1TEST)
            {
               childrenBook.Status = Book.BookStatus.Reserved;
                answer = false;

            }


            actual = libraryAccount.ReturnBook(childrenBook);
            //Assert
            Assert.IsFalse(actual == answer);
            //Assert.IsTrue(childrenBook.Status == Book.BookStatus.OutOfTheLibrary);
           

        }


        [TestMethod]
        public void LoanBook_ChildLoanAvailableChildrenBook_ReturnTrueAndListIsUpdated()
        {
            //Arrnge
            Reader childReader = new Reader("Lior", "BB", Reader.ReaderType.Child);
            LibraryAccount libraryAccount = new LibraryAccount(childReader);
            Book childrenBook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.InTheLibrary);

            bool actual;

            //Act
            actual = libraryAccount.LoanBook(childrenBook);

            //Assert
            Assert.IsTrue(actual, "BUG: A child didnt manage to loan a child book");
            Assert.IsTrue(libraryAccount.LoanBooks.Contains(childrenBook), "BUG: book was not updated in the loan list ");
            Assert.IsTrue(childrenBook.Status == Book.BookStatus.OutOfTheLibrary);
            Assert.IsFalse(libraryAccount.ReservedBooks.Contains(childrenBook), "BUG: book was loaned but still mentioned in the reserve list");
        }

        [TestMethod]
        [Timeout(100)]
        public void ReturnBook_test()
        {
            //Arrnge
            Reader childReader = new Reader("Lior", "BB", Reader.ReaderType.Child);
            LibraryAccount libraryAccount = new LibraryAccount(childReader);
            Book childrenBook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.InTheLibrary);
            libraryAccount.LoanBook(childrenBook);
            bool actual;

            //Act
            actual = libraryAccount.ReturnBook(childrenBook);

            //Assert
            Assert.IsTrue(actual, "BUG: A child didnt manage to loan a child book");
            Assert.IsTrue(childrenBook.Status == Book.BookStatus.InTheLibrary);
        }


        [TestMethod]
        public void ReserveBook_test()
        {
            //Arrnge
            Reader childReader = new Reader("Lior", "BB", Reader.ReaderType.Child);
            LibraryAccount libraryAccount = new LibraryAccount(childReader);
            Book childrenBook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.InTheLibrary);
            libraryAccount.LoanBook(childrenBook);
            bool actual;

            //Act
            actual = libraryAccount.ReserveBook(childrenBook);

            //Assert
            Assert.IsTrue(actual, "BUG: A child didnt manage to loan a child book");
            Assert.IsTrue(childrenBook.Status == Book.BookStatus.OutOfTheLibraryAndReserved);
            Assert.IsTrue(childrenBook.Status != Book.BookStatus.Reserved);
            
        }

        [TestMethod]
        public void CancelReserveBook_test()
        {
            //Arrnge
            Reader childReader = new Reader("Lior", "BB", Reader.ReaderType.Child);
            LibraryAccount libraryAccount = new LibraryAccount(childReader);
            Book childrenBook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.InTheLibrary);
            libraryAccount.ReserveBook(childrenBook);
            bool actual;

            //Act
            actual = libraryAccount.CancelReserveBook(childrenBook);

            //Assert
            Assert.IsTrue(actual, "BUG: A child didnt manage to loan a child book");
            Assert.IsTrue(childrenBook.Status == Book.BookStatus.InTheLibrary);

        }

        [TestMethod]
        [DataRow(Book.BookStatus.Reserved,Reader.ReaderType.Adult,false)]
        [DataRow(Book.BookStatus.OutOfTheLibraryAndReserved, Reader.ReaderType.Child, false)]
        [DataRow(Book.BookStatus.InTheLibrary, Reader.ReaderType.Child, true)]

        public void ReserveBook_testdataRow(Book.BookStatus statusbook,Reader.ReaderType readerty,bool exbected)
        {
            //Arrnge
            Reader childReader = new Reader("Lior", "BB", readerty);
            LibraryAccount libraryAccount = new LibraryAccount(childReader);
            Book childrenBook = new Book(Book.BookType.ChildrenBook, statusbook);
            bool actual;

            //Act
            actual = libraryAccount.ReserveBook(childrenBook);

            //Assert

            Assert.IsTrue(actual == exbected);

        }

        [TestMethod]
        [DataRow(Book.BookStatus.OutOfTheLibraryAndReserved, false)]
        [DataRow(Book.BookStatus.Reserved, false)]
        [DataRow(Book.BookStatus.InTheLibrary, true)]

        public void LoanBook_testdataRow(Book.BookStatus statusbook, bool exbected)
        {
            //Arrnge
            Reader childReader = new Reader("Lior", "BB", Reader.ReaderType.Child);
            LibraryAccount libraryAccount = new LibraryAccount(childReader);
            Book childrenBook = new Book(Book.BookType.ChildrenBook, statusbook);
            bool actual;

            //Act
            actual = libraryAccount.LoanBook(childrenBook);

            //Assert

            Assert.IsTrue(actual == exbected);

        }


















    }
}
