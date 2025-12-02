using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore
{
    public  class LibraryService
    {
        private List<Book> books = new();
        private List<User> users = new ();
        private List<Loan> loans = new ();

        private void ValidateBookCode(string code)
        {
            if (books.Any(b => b.Code == code))
                throw new InvalidOperationException("No se permiten libros con códigos duplicados");
        }

        private void ValidateBookCopies(int copies)
        {
            if (copies <= 0)
                throw new InvalidOperationException("No se permiten libros con cantidades en 0 o inferior");
        }

        public Book RegisterBook(string code, string title, string author, string description, int copies)
        {
            ValidateBookCopies(copies);
            ValidateBookCode(code);

            var newBook = new Book(code, title, author, description, copies);
            books.Add(newBook);
            return newBook;
        }

        public User RegisterUser(string code, string firstName, string lastName)
        {
            return null;
        }

    }
}
