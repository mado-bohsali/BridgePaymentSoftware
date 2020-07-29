using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team5_payment.Models
{
    public class SaveCard
    {
        public string StoreId { get; set; }
        public string Pan { get; set; }
        public string ExpDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string CustomerId { get; set; }
    }
}
