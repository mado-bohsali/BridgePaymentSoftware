using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace team5_payment.Data
{
    public interface IPayment
    {
        
        public Dictionary<string, string> deleteCard(string cardID);
        public HttpWebResponse deletePayPalCard(string credit_card_id);
    }
}
