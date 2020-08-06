using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moneris;
using team5_payment.Data;
using team5_payment.Models;

namespace team5_payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseRepo _repository;

        public PurchasesController(IPurchaseRepo repository)
        {
            _repository = repository;
        }

        //POST: /api/purchases/saved
        [HttpPost("Saved")]
        public ActionResult ProcessSavedPayment(PaymentProccess paymentProcess)
        {
            try
            {
                dynamic response = _repository.ProcessSavedPayment(paymentProcess);
                return Ok(response);

            }

            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        //POST: /api/purchases
        [HttpPost]
        public ActionResult ProcessPayment()
        {
            try
            {
                dynamic response = _repository.ProcessPayment();
                return Ok(response);

            }

            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        //POST: /api/purchases/refund
        [HttpPost]
        public ActionResult Refund()
        {
            throw new NotImplementedException();
        }
    }
}
