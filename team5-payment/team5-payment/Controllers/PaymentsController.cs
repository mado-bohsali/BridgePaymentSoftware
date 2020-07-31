using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moneris;
using System.Net.Http;
using System.Text;
using System.Collections;
using team5_payment.Data;

namespace team5_payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PaymentsController : ControllerBase
    {
        Completion completion = new Completion();
        private static readonly HttpClient client = new HttpClient();
        private static Dictionary<string, string> response = new Dictionary<string, string>();
        
        private readonly IPayment _repository;

        public PaymentsController(IPayment repository)
        {
            _repository = repository;
        }

        [HttpPost("delete")]
        public ActionResult<Dictionary<string,string>> DeleteCard(Card card)
        {
            try
            {   
                response = _repository.deleteCard(card);
            }
            catch (Exception exception)
            {
                response = new Dictionary<string, string>
                {
                    {"Card ID", $"{exception.Message}"},
                    {"Valut Success", $"{exception.Message}" }
                };

            }

            return Ok(response);
        }
    }


}
