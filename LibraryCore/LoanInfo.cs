using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore
{
    public class LoanInfo
    {
        public string BookCode { get; set; }
        public string BookTitle{get;set;}
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public DateTime LoanDate { get; set; }

        public bool Returned { get; set; }



    }
}
