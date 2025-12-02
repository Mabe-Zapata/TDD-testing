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
                LibraryService library = new LibraryService(); library = new LibraryService();
	            library.RegisterUser("U1805789234", "Carlos", "Sanchez");
	            Assert.Throws<InvalidOperationException>(() =>
	            library.RegisterUser("U1805789234", "Ana", "Perez"));
	        }

}
}