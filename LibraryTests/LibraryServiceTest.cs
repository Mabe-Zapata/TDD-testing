using LibraryCore;
namespace LibraryTests
{
    public class LibraryServiceTest
    {
        [Fact]
        public void CodigoDuplicado()
        {
            LibraryService library = new LibraryService();
           
            library.RegisterBook( "B001",  "The little prince", "Antoine de Saint-Exupery",
                "The Little Prince is a novella by Antoine de Saint-Exupéry about a pilot stranded in the " +
                "desert who meets a prince from a small asteroid. The prince has traveled to other planets, " +
                "encountering peculiar adults, and comes to Earth, where he learns about friendship " +
                "and love from a fox.", 10);
            Assert.Throws<InvalidOperationException>(() => library.RegisterBook("B001", "It", "Stephen King", "It is a 1986 horror" +
                " novel by Stephen King about seven children in Derry, Maine, who battle an evil, " +
                "shape-shifting creature that appears every 27 years to feed on children's fears.", 10 ));
        }

        [Fact]
        public void cantidadCero()
        {
            LibraryService library = new LibraryService();

            Assert.Throws<InvalidOperationException>(()=> library.RegisterBook("B001", "The little prince", "Antoine de Saint-Exupery",
                "The Little Prince is a novella by Antoine de Saint-Exupéry about a pilot stranded in the " +
                "desert who meets a prince from a small asteroid. The prince has traveled to other planets, " +
                "encountering peculiar adults, and comes to Earth, where he learns about friendship " +
                "and love from a fox.",0 )
            );
        }

        [Fact]
        public void registroLibroCorrecto()
        {
            LibraryService library = new LibraryService();
            Book answer = new Book("B001", "The little prince", "Antoine de Saint-Exupery",
                "The Little Prince is a novella by Antoine de Saint-Exupéry about a pilot stranded in the " +
                "desert who meets a prince from a small asteroid. The prince has traveled to other planets, " +
                "encountering peculiar adults, and comes to Earth, where he learns about friendship " +
                "and love from a fox.", 10);
            Book result = library.RegisterBook("B001", "The little prince", "Antoine de Saint-Exupery",
                "The Little Prince is a novella by Antoine de Saint-Exupéry about a pilot stranded in the " +
                "desert who meets a prince from a small asteroid. The prince has traveled to other planets, " +
                "encountering peculiar adults, and comes to Earth, where he learns about friendship " +
                "and love from a fox.", 10);
            Assert.Equal(answer.Code, result.Code);
            Assert.Equal(answer.Title, result.Title);
            Assert.Equal(answer.Author, result.Author);
            Assert.Equal(answer.Description, result.Description);
            Assert.Equal(answer.Copies, result.Copies);

        }

       
	        [Fact]
	        public void registroUsuarioCodigoDuplicado()
	        {
                LibraryService library = new LibraryService();
	            library.RegisterUser("U1805789234", "Carlos", "Sanchez");
	            Assert.Throws<InvalidOperationException>(() =>
	            library.RegisterUser("U1805789234", "Ana", "Perez"));
	        }

        [Fact]
        public void registroUsuarioCuandoCodigoVacio()
        {
            LibraryService library = new LibraryService();
            Assert.Throws<InvalidOperationException>(() =>
            library.RegisterUser("", "Carlos", "Sanchez"));
        }

        [Fact]
        public void registroUsuarioCuandoNombreVacio()
        {
            LibraryService library = new LibraryService();
            Assert.Throws<InvalidOperationException>(() =>
            library.RegisterUser("U1805789234", "", "Sanchez"));
        }

        [Fact]
        public void prestamoLibroInexistente() { 
            LibraryService library = new LibraryService();
            library.RegisterUser("U001", "Maria", "Zapata");
            Assert.Throws<InvalidOperationException>(() =>
            library.LendBook("L001", "B002", "U001"));
        }

        [Fact]
        public void prestamoUsuarioInexistente()
        {
            LibraryService library = new LibraryService();
            library.RegisterBook("B001", "The little prince", "Antoine de Saint-Exupery",
               "The Little Prince is a novella by Antoine de Saint-Exupéry about a pilot stranded in the " +
               "desert who meets a prince from a small asteroid. The prince has traveled to other planets, " +
               "encountering peculiar adults, and comes to Earth, where he learns about friendship " +
               "and love from a fox.", 10);
            Assert.Throws<InvalidOperationException>(() =>
            library.LendBook("L001", "B001", "U001"));
        }


        [Fact]
        public void prestamosControlCantidad()
        {
            LibraryService library = new LibraryService();
            library.RegisterBook("B001", "The little prince", "Antoine de Saint-Exupery",
               "The Little Prince is a novella by Antoine de Saint-Exupéry about a pilot stranded in the " +
               "desert who meets a prince from a small asteroid. The prince has traveled to other planets, " +
               "encountering peculiar adults, and comes to Earth, where he learns about friendship " +
               "and love from a fox.", 1);
            library.RegisterUser("U001", "Maria", "Zapata");
            library.LendBook("L001", "B001", "U001");
            Assert.Throws<InvalidOperationException>(() =>
            library.LendBook("L002", "B001", "U001"));
        }

        [Fact]
        public void PrestamoLibroCorrecto()
        {
            
            var library = new LibraryService();
            library.RegisterBook("B001", "The Little Prince", "Antoine de Saint-Exupery", "Descripción", 2);
            library.RegisterUser("U001", "Maria", "Zapata");

        
            Loan loan = library.LendBook("L001", "B001", "U001");

            
            Assert.NotNull(loan);                        
            Assert.Equal("B001", loan.BookCode);         
            Assert.Equal("U001", loan.UserCode);         

            Book book = library.GetBookByCode("B001"); //metodo de testing 
            Assert.Equal(1, book.Copies);               
        }

        [Fact]
        public void existePrestamo() {
            LibraryService library = new LibraryService();
            library.RegisterBook("B001", "The Little Prince", "Antoine de Saint-Exupery", "Descripción", 2);
            library.RegisterUser("U001", "Maria", "Zapata");

            Assert.Throws<InvalidOperationException>(() =>
            library.ReturnBook("L001"));
        }

        [Fact]
        public void retornarLibroCantidadControl()
        {
            LibraryService library = new LibraryService();
            library.RegisterBook("B001", "The Little Prince", "Antoine de Saint-Exupery", "Descripción", 3);
            library.RegisterUser("U001", "Maria", "Zapata");
            library.LendBook("L001","B001","U001");

            Loan loan = library.ReturnBook("L001");
            Assert.NotNull(loan);
            Book book = library.GetBookByCode("B001");
            Assert.Equal(3, book.Copies);
            Assert.True(loan.Returned);
        }

        [Fact]
        public void listarDisponibles() {
            LibraryService library = new LibraryService();
            library.RegisterBook("B001", "The Little Prince", "Antoine de Saint-Exupery",
                "Descripción", 2);
            library.RegisterBook("B002", "1984", "George Orwell", "Descripción", 1);
            library.RegisterUser("U001", "Maria", "Zapata");
            library.LendBook("L001", "B002", "U001");
            Book[] availableBooks = library.ShowAvailableBooks();

            Assert.Single(availableBooks);
            Assert.Equal("B001", availableBooks[0].Code);

        }
        [Fact]
        public void listarPrestados()
        {
            LibraryService library = new LibraryService();
            library.RegisterBook("B001", "The Little Prince", "Antoine de Saint-Exupery", "Descripción", 2);
            library.RegisterBook("B002", "1984", "George Orwell", "Descripción", 1);
            library.RegisterUser("U001", "Maria", "Zapata");
            library.RegisterUser("U002", "Erick", "Guerron");

            library.LendBook("L001", "B001", "U001");
            library.LendBook("L002", "B002", "U002");

            var loansWithInfo = library.ShowActiveLoansWithBookInfo();
            Assert.Contains(loansWithInfo, l => l.BookCode == "B001" && l.UserCode == "U001");
            Assert.Contains(loansWithInfo, l => l.BookCode == "B002" && l.UserCode == "U002");
        }

        [Fact]
        public void prestamosPorUsuarioHistorico()
        {
            
            LibraryService library = new LibraryService();

            library.RegisterBook("B001", "The Little Prince", "Antoine de Saint-Exupery", "Descripción", 2);
            library.RegisterBook("B002", "1984", "George Orwell", "Descripción", 1);
            library.RegisterUser("U001", "Maria", "Zapata");

            library.LendBook("L001","B001", "U001");
            library.LendBook("L002", "B002","U001");


            library.ReturnBook("L001");


            var history = library.ShowLoanHistoryByUser("U001");


            Assert.Equal(2, history.Length); 
            Assert.Contains(history, l => l.BookCode == "B001" && l.Returned == true);
            Assert.Contains(history, l => l.BookCode == "B002" && l.Returned == false);
        }

    }
}