using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_mstest
{
    public class Book
    {
        #region Enums
        // A book can be defined either child book or adult book
        public enum BookType
        {
            ChildrenBook,
            AdultBook
        }
        public enum BookStatus
        {
            InTheLibrary, //על המדף,זמין לכל
            Reserved, //הספר שמור ומחכה למי שהזמין
            OutOfTheLibrary, //הספר מושאל
            OutOfTheLibraryAndReserved, //הספר מושאל ובינתיים מישהו הזמין אותו
        }
        #endregion

        #region Private Variables
        private string id;
        private BookType type;
        private BookStatus status;
        private BookStatus reserved;
        #endregion

        #region Properties
        public string Id
        {
            get { return id; }
        }
        public BookType Type
        {
            get { return type; }
        }
        public BookStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion


        #region Constructors
        public Book()
        {
            //Generate a random string to be set as the book id
            Guid guid = Guid.NewGuid();
            id = guid.ToString();

            type = BookType.AdultBook;
            status = BookStatus.InTheLibrary;
        }

        public Book(BookType type,BookStatus status)
        {
            //Generate a random string to be set as the book id
            Guid guid = Guid.NewGuid();
            id = guid.ToString();

            this.type = type;
            this.status = status;
        }

        public Book(BookType type, BookStatus status, BookStatus reserved) : this(type, status)
        {
            this.reserved = reserved;
        }
        #endregion

    }
}
