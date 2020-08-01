using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team5_payment.Models;

namespace team5_payment.Data
{
    public interface IVaultRepo
    {
        public object SaveCard(SaveCard saveCard);
    }
}
