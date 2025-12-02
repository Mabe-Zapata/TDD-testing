using LibraryCore;
namespace LibraryTests
{
    public class LibraryServiceTest
    {
        [Fact]
        public void Test1()
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
    }
}