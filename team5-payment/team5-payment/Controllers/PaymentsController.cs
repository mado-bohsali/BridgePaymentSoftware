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
using team5_payment.Models;
using System.Net;

namespace team5_payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PaymentsController : ControllerBase
    {
        Completion completion = new Completion();
        public static string clientID = "ARWu1ooXkC4RQ7aoiLlXher4Qh01RMRHVuw53unLj4m2hlCchzl5U3M48gCzMQEGyHvpQlfKlKTviLEi";
        private static string sandboxToken = "access_token$sandbox$6ywm8z7hhkqm953m$1d9dc1c4da6bd0b1416439948e76e61d";

        private static readonly HttpClient client = new HttpClient();
        private static Dictionary<string, string> response = new Dictionary<string, string>();
        
        private readonly IPayment _repository;

        public PaymentProccess paymentProccess = new PaymentProccess();

        public PaymentsController(IPayment repository)
        {
            _repository = repository;
        }

        [HttpPost("delete/{cardID}")]
        public IActionResult DeleteCard(string cardID)
        {
            try
            {
                //------Add Card 
                SaveCard saveCard = new SaveCard();
    
                string pan = saveCard.Pan;
                string expdate = saveCard.ExpDate;
                string crypt_type = "7"; //crypt type 7 for ecommerce

                AvsInfo avsCheck = new AvsInfo();
                avsCheck.SetAvsStreetNumber(saveCard.StreetNumber);
                avsCheck.SetAvsStreetName(saveCard.StreetName);
                avsCheck.SetAvsZipCode(saveCard.ZipCode);

                ResAddCC resaddcc = new ResAddCC();
                resaddcc.SetPan(pan);
                resaddcc.SetExpDate(expdate);
                resaddcc.SetCryptType(crypt_type);

                //Mandatory - Credential on File details
                CofInfo cof = new CofInfo();
                cof.SetIssuerId("139X3130ASCXAS9"); //can be obtained by performing card verification

                resaddcc.SetCofInfo(cof);

                string store_id = "store5";
                string api_token = "yesguy";
                //string crypt_type = "7";
                string processing_country_code = "CA";

                string order_id = paymentProccess.OrderId;
                string amount = paymentProccess.Amount;
                string data_key = paymentProccess.DataKey;


                //CofInfo cof = new CofInfo();
                cof.SetPaymentIndicator("U");
                cof.SetPaymentInformation("2");
                cof.SetIssuerId("168451306048014");

                ResPreauthCC resPreauthCC = new ResPreauthCC();
                resPreauthCC.SetDataKey(data_key);
                resPreauthCC.SetOrderId(order_id);
                resPreauthCC.SetAmount(amount);
                resPreauthCC.SetCryptType(crypt_type);
                resPreauthCC.SetCofInfo(cof);
                //--------
                response = _repository.deleteCard(cardID);
            }
            catch (Exception exception)
            {
                response = new Dictionary<string, string>
                {
                    {"Card ID", $"{cardID}"},
                    {"Valut Success", $"{MockPayment.receipt.GetResSuccess()}"},
                    {"Exception", $"{exception.StackTrace}"}
                   
                };

            }

            return Ok(response);
        }

        //PayPal Integration
        [HttpDelete]

        public HttpWebResponse deletePayPalCard(string credit_card_id)
        {
            HttpWebResponse response = new HttpWebResponse();

            try
            {
                response =  _repository.deletePayPalCard(credit_card_id);
            } catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
            }

            return response;

        }
        
    }


}
