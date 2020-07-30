using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team5_payment.Models
{
    public class SaveCard
    {
        public string Pan { get; set; }
        public string ExpDate { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string ZipCode { get; set; }

    }
}
