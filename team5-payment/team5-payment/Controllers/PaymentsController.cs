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
        private static readonly HttpClient client = new HttpClient();
        private static Dictionary<string, string> response = new Dictionary<string, string>();
        
        private readonly IPayment _repository;

        public PaymentsController(IPayment repository)
        {
            _repository = repository;
        }

        [HttpPost("delete/{cardID}")]
        public IActionResult DeleteCard(string cardID)
        {
            try
            {
                cardID = "PjVKjtEmc1FvFyjxHE4EwBMxi";
                response = _repository.deleteCard(cardID);
                return Ok(response);
            }
            catch (Exception exception)
            {
                response = new Dictionary<string, string>
                {
                    {"Card ID", $"{cardID}"},
                    {"Valut Success", "Failed"},
                    {"Exception", $"{exception.Message}"}
                   
                };

            }

            return Ok(response);
        }
    }


}
