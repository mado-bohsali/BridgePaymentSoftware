using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace team5_payment.Data
{
    public interface IPayment
    {

        public Dictionary<string, string> deleteCard(string cardID);
    }
}
