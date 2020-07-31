using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moneris;
using team5_payment.Models;

namespace team5_payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {

        //service provider ID

        //POST: /api/payments
        [HttpPost("SaveCard")]
        public ActionResult SaveCard(SaveCard saveCard)
        {
            /*
             * For Testing:
             * 
             * pan: "4242424242424242";
             * expDate: "2022"
             * streetNumber: "212"
             * streetName: "Payton Street"
             * zipCode: "M1M1M1"
             */

            //Mandatory info given by the store
            string store_id = "store5";
            string api_token = "yesguy";

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

            HttpsPostRequest mpgReq = new HttpsPostRequest();
            mpgReq.SetTestMode(true); //false or comment out this line for production transactions
            mpgReq.SetStoreId(store_id);
            mpgReq.SetApiToken(api_token);
            mpgReq.SetProcCountryCode("CA");
            mpgReq.SetTransaction(resaddcc);
            mpgReq.Send();

            try
            {

                Receipt receipt = mpgReq.GetReceipt();
                var response = new { Id = receipt.GetDataKey(), Success = receipt.GetResSuccess() };

                if (response.Success == "true")
                    return Created(new Uri(Request.GetDisplayUrl() + "/" + response.Id), response);
                else
                    return BadRequest();
            }

            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost("ProcessSavedPayment")]
        public ActionResult ProcessSavedPayment(PaymentProccess paymentProcess)
        {

            /* For Testing:
             * 
             * "orderId": Anything,
             * "dataKey": "Q9obPBulomKb7Heck8R8qdFr2",
             * "amount": anything 2.00
             */

            string store_id = "store5";
            string api_token = "yesguy";
            string crypt_type = "7";
            string processing_country_code = "CA";

            string order_id = paymentProcess.OrderId;
            string data_key = paymentProcess.DataKey;
            string amount = paymentProcess.Amount;


            CofInfo cof = new CofInfo();
            cof.SetPaymentIndicator("U");
            cof.SetPaymentInformation("2");
            cof.SetIssuerId("168451306048014");

            ResPurchaseCC resPurchaseCC = new ResPurchaseCC();
            resPurchaseCC.SetDataKey(data_key);
            resPurchaseCC.SetOrderId(order_id);
            resPurchaseCC.SetAmount(amount);
            resPurchaseCC.SetCryptType(crypt_type);
            resPurchaseCC.SetCofInfo(cof);

            HttpsPostRequest mpgReq = new HttpsPostRequest();
            mpgReq.SetProcCountryCode(processing_country_code);
            mpgReq.SetTestMode(true); //false or comment out this line for production transactions
            mpgReq.SetStoreId(store_id);
            mpgReq.SetApiToken(api_token);
            mpgReq.SetTransaction(resPurchaseCC);
            mpgReq.Send();

            try
            {
                Receipt receipt = mpgReq.GetReceipt();
                var response = new { Id = receipt.GetDataKey(), Success = Boolean.Parse(receipt.GetResSuccess()) };

                if (response.Success)
                    return Ok(response);
                else
                    return BadRequest(response.Success);
            }

            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
