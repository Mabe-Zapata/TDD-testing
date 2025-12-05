using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryCore
{
    public class LibraryService
    {
        private readonly List<Book> books = new();
        private readonly List<User> users = new();
        private readonly List<Loan> loans = new();
        private static readonly StringComparer CodeComparer = StringComparer.OrdinalIgnoreCase;

        private void ValidateBookCode(string code)
        {
            if (books.Any(b => CodeComparer.Equals(b.Code, code)))
                throw new InvalidOperationException("No se permiten libros con codigos duplicados");
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

            if (users.Any(u => CodeComparer.Equals(u.Code, code)))
                throw new InvalidOperationException("No se puede ingresar usuarios con codigos duplicados");

            var newUser = new User(code, name, lastName);
            users.Add(newUser);
            return newUser;
        }

        private void ValidateUserCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new InvalidOperationException("El codigo del usuario no puede estar vacio");
        }

        private void ValidateUserName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("El nombre del usuario no puede estar vacio");
        }

        public Book GetBookByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;

            return books.FirstOrDefault(b => CodeComparer.Equals(b.Code, code));
        }

        public User GetUserByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;

            return users.FirstOrDefault(u => CodeComparer.Equals(u.Code, code));
        }

        public Book[] ShowBooks()
        {
            return books.ToArray();
        }

        public User[] ShowUsers()
        {
            return users.ToArray();
        }

        public IEnumerable<Book> SearchBooks(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return books;

            var normalized = query.Trim().ToLower();

            return books.Where(book =>
                book.Code.ToLower().Contains(normalized) ||
                book.Title.ToLower().Contains(normalized) ||
                book.Author.ToLower().Contains(normalized) ||
                (!string.IsNullOrWhiteSpace(book.Description) &&
                 book.Description.ToLower().Contains(normalized)));
        }

        public IEnumerable<User> SearchUsers(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return users;

            var normalized = query.Trim().ToLower();

            return users.Where(user =>
            {
                var fullName = $"{user.FirstName} {user.LastName}".ToLower();
                return user.Code.ToLower().Contains(normalized) ||
                       fullName.Contains(normalized);
            });
        }

        public Loan LendBook(string idLoan, string codeBook, string codeUser)
        {
            var book = books.FirstOrDefault(b => CodeComparer.Equals(b.Code, codeBook))
                       ?? throw new InvalidOperationException("Solo se prestan libros existentes dentro de la biblioteca");

            var user = users.FirstOrDefault(u => CodeComparer.Equals(u.Code, codeUser))
                       ?? throw new InvalidOperationException("Solo se prestan libros a usuarios existentes");

            if (book.Copies <= 0)
                throw new InvalidOperationException("No hay copias disponibles para prestar");

            var loan = new Loan(idLoan, book.Code, user.Code);
            loans.Add(loan);
            book.Copies--;

            return loan;
        }

        public Loan ReturnBook(string idLoan)
        {
            var loan = loans.FirstOrDefault(l => CodeComparer.Equals(l.IdLoan, idLoan))
                       ?? throw new InvalidOperationException("No existe el prestamo activo");

            if (loan.Returned)
                throw new InvalidOperationException("El prestamo ya fue devuelto previamente");

            var book = books.FirstOrDefault(b => CodeComparer.Equals(b.Code, loan.BookCode))
                       ?? throw new InvalidOperationException("El libro asociado al prestamo no existe");

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
                    var book = books.First(b => CodeComparer.Equals(b.Code, l.BookCode));
                    var user = users.First(u => CodeComparer.Equals(u.Code, l.UserCode));

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
                .ToArray();
        }

        public LoanInfo[] ShowLoanHistoryByUser(string userCode)
        {
            if (!users.Any(u => CodeComparer.Equals(u.Code, userCode)))
                throw new InvalidOperationException("Usuario no existe");

            return loans
                .Where(l => CodeComparer.Equals(l.UserCode, userCode))
                .Select(l =>
                {
                    var book = books.FirstOrDefault(b => CodeComparer.Equals(b.Code, l.BookCode))
                               ?? throw new InvalidOperationException($"Libro con codigo {l.BookCode} no existe");

                    var user = users.First(u => CodeComparer.Equals(u.Code, l.UserCode));

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
