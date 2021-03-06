﻿using Moneris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team5_payment.Models;

namespace team5_payment.Data
{
    public interface IPurchaseRepo
    {
        public object ProcessSavedPayment(PaymentProccess paymentProcess);
        public object ProcessPayment();
        public object Refund();
    }
}
