using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team5_payment.Models
{
    public class PaymentProccess
    {
        public string OrderId { get; set; }
        public string Amount { get; set; }
        public string Pan { get; set; }
        public string ExpDate { get; set; }
    }
}
