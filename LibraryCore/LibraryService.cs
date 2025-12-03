using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore
{
    public class LibraryService
    {
        private List<Book> books = new();
        private List<User> users = new();
        private List<Loan> loans = new();

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

        public User RegisterUser(string code, string name, string lastName)
        {
            ValidateUserCode(code);
            ValidateUserName(name);

            if (users.Any(u => u.Code == code))
                throw new InvalidOperationException("No se puede ingresar usuarios con códigos duplicados");

            var newUser = new User(code, name, lastName);
            users.Add(newUser);

            return newUser;
        }

        private void ValidateUserCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new InvalidOperationException("El código del usuario no puede estar vacío");
        }

        private void ValidateUserName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("El nombre del usuario no puede estar vacío");
        }
        public Book GetBookByCode(string code)
        {
            return books.Find(b => b.Code == code);
        }


        public Loan LendBook(string codeBook, string codeUser)
        {
            
            var book = books.FirstOrDefault(b => b.Code == codeBook)
                       ?? throw new InvalidOperationException("Solo se prestan libros existentes dentro de la biblioteca");

            var user = users.FirstOrDefault(u => u.Code == codeUser)
                       ?? throw new InvalidOperationException("Solo se prestan libros a usuarios existentes");

           
            if (book.Copies <= 0)
                throw new InvalidOperationException("No hay copias disponibles para prestar");

           
            var loan = new Loan(codeBook, codeUser);
            loans.Add(loan);
            book.Copies--; 

            return loan;
        }

        public Loan ReturnBook(string codeBook, string codeUser)
        {
           
            Book book = books.FirstOrDefault(b => b.Code == codeBook);
            if (book == null)
                throw new InvalidOperationException("El libro no existe");

           
            Loan loan = loans.FirstOrDefault(l => l.BookCode == codeBook && l.UserCode == codeUser && !l.Returned);
            if (loan == null)
                throw new InvalidOperationException("No existe el préstamo activo");

           
            loan.Returned = true;
            book.Copies++;

            return loan;
        }

        public Book[] ShowAvailableBooks()
        {
            return books.Where(b => b.Copies > 0).ToArray();
        }

        public LoanInfo[] ShowActiveLoansWithBookInfo() 
        {

            return loans
        .Where(l => !l.Returned)
        .Select(l =>
        {
            var book = books.First(b => b.Code == l.BookCode);
            var user = users.First(u => u.Code == l.UserCode);

            return new LoanInfo
            {
                BookCode = book.Code,
                BookTitle = book.Title,
                UserCode = user.Code,
                UserName = user.FirstName,
                UserLastName = user.LastName,
                LoanDate = l.LoanDate
            };
        })
        .ToList()
        .ToArray();

        }

        public LoanInfo[] ShowLoanHistoryByUser(string userCode)
        {
            if (users == null || loans == null || books == null)
                throw new InvalidOperationException("Listas no inicializadas");

            var userExists = users.Any(u => u.Code == userCode);
            if (!userExists)
                throw new InvalidOperationException("Usuario no existe");

            return loans
                .Where(l => l.UserCode == userCode)
                .Select(l =>
                {
                    var book = books.FirstOrDefault(b => b.Code == l.BookCode);
                    if (book == null)
                        throw new InvalidOperationException($"Libro con código {l.BookCode} no existe");

                    var user = users.FirstOrDefault(u => u.Code == l.UserCode);
                    if (user == null)
                        throw new InvalidOperationException($"Usuario con código {l.UserCode} no existe");

                    return new LoanInfo
                    {
                        BookCode = book.Code,
                        BookTitle = book.Title,
                        UserCode = user.Code,
                        UserName = user.FirstName,
                        UserLastName = user.LastName,
                        LoanDate = l.LoanDate,
                        Returned = l.Returned
                    };
                })
                .ToArray();
        }




    }
}