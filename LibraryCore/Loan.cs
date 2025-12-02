using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore
{
    internal class Loan
    {
        public string BookCode { get; set; }

        public string UserCode { get; set; }
        public DateTime LoanDate { get; set; }
        public bool Returned { get; set; } = false;
    }
}
