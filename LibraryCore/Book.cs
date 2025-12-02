using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore
{
    public class Book
    {
        public string Code { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public string Author { get; set; }

        public int Copies { get; set; }

    }
}
