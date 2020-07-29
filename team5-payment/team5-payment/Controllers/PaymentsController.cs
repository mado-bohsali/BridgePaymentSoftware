using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            string store_id = saveCard.StoreId;
            //string store_id = "store5";
            string api_token = "yesguy";
            string pan = saveCard.Pan;
            //string pan = "4242424242424242";
            string expdate = saveCard.ExpDate;
            //string expdate = "1912";
            string phone = saveCard.Phone;
            //string phone = "0000000000";
            string email = saveCard.Email;
            //string email = "bob@smith.com";
            string note = saveCard.Note;
            //string note = "my note";
            string cust_id = saveCard.CustomerId;
            //string cust_id = "customer1";
            string crypt_type = "7";
            string data_key_format = "0";
            string processing_country_code = "CA";
            bool status_check = false;

            AvsInfo avsCheck = new AvsInfo();
            avsCheck.SetAvsStreetNumber("212");
            avsCheck.SetAvsStreetName("Payton Street");
            avsCheck.SetAvsZipCode("M1M1M1");

            CofInfo cof = new CofInfo();
            cof.SetIssuerId("168451306048014");

            ResAddCC resaddcc = new ResAddCC();
            resaddcc.SetPan(pan);
            resaddcc.SetExpDate(expdate);
            resaddcc.SetCryptType(crypt_type);
            resaddcc.SetCustId(cust_id);
            resaddcc.SetPhone(phone);
            resaddcc.SetEmail(email);
            resaddcc.SetNote(note);
            resaddcc.SetAvsInfo(avsCheck);
            resaddcc.SetGetCardType("true");
            //resaddcc.SetDataKeyFormat(data_key_format); //optional
            resaddcc.SetCofInfo(cof);

            HttpsPostRequest mpgReq = new HttpsPostRequest();
            mpgReq.SetProcCountryCode(processing_country_code);
            mpgReq.SetTestMode(true); //false or comment out this line for production transactions
            mpgReq.SetStoreId(store_id);
            mpgReq.SetApiToken(api_token);
            mpgReq.SetTransaction(resaddcc);
            mpgReq.SetStatusCheck(status_check);
            mpgReq.Send();


            return Ok("Card Created");

        }
    }
}
