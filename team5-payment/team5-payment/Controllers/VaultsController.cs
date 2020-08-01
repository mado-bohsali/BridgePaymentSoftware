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
    public class VaultsController : ControllerBase
    {
        private readonly IVaultRepo _repository;

        public VaultsController(IVaultRepo repository)
        {
            _repository = repository;
        }


        [HttpPost("Save")]
        public ActionResult SaveCard(SaveCard saveCard)
        {
            try
            {
                dynamic response = _repository.SaveCard(saveCard);
                return Created(new Uri(Request.GetDisplayUrl() + "/" + response.Id), response);

            }

            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
