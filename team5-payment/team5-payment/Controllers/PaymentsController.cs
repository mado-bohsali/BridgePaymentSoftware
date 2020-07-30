using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        //POST: /api/payments
        [HttpPost]
        public ActionResult SaveCard(SaveCard saveCard)
        {
            /*
             * For Testing:
             * 
             * pan: "4242424242424242";
             * expDate: "1912"
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
                var success = receipt.GetResSuccess();

                if (success == "true")
                    return Created(new Uri(Request.GetDisplayUrl() + "/" + receipt.GetDataKey()), saveCard);
                else
                    return BadRequest();
            }

            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
