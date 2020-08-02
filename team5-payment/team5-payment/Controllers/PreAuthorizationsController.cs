using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using team5_payment.Data;
using team5_payment.Models;

namespace team5_payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreAuthorizationsController : ControllerBase
    {
        private readonly IPreAuthorization _repository;

        public PreAuthorizationsController (IPreAuthorization repository)
        {
            _repository = repository;
        }

        [HttpPost("Vault")]
        public ActionResult PreAuthWithVault(PaymentProccess paymentProcess)
        {
            try
            {
                dynamic response = _repository.PreAuthWithVault(paymentProcess);
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
