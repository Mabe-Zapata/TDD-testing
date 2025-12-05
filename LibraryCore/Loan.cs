using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore
{
    public class Loan
    {
        public string IdLoan { get; set; }
        public string BookCode { get; set; }

        public string UserCode { get; set; }
        public DateTime LoanDate { get; set; }
        public bool Returned { get; set; } = false;

        public Loan(string IdLoan, string bookCode, string userCode) {
            this.IdLoan = IdLoan;
            this.BookCode = bookCode;
            this.UserCode = userCode;
            this.LoanDate = DateTime.Now;
        
        }
    }
}
