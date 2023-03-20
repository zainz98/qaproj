//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using qafinal;
using Library_mstest;
using NUnit.Framework;

namespace LibraryLibraryAccountTests_nunit

{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
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

        [Test]
       
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


        [Test]
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
    }
}
