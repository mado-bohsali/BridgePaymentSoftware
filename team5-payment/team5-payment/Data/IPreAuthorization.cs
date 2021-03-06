﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team5_payment.Models;

namespace team5_payment.Data
{
    public interface IPreAuthorization
    {
        public object PreAuthWithVault(PaymentProccess paymentProcess);
        public object PreAuth();
        public object PreAuthCompletion(string txn);
    }
}
