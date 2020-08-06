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
    public class UpdateDetailsController : ControllerBase
    {
        //POST: /api/UpdateDetails
        public ActionResult RetrieveCard(string data_key)
		{ // data_key =  "vthBJyN1BicbRkdWFZ9flyDP2"
			String store_id = "moneris";
			String api_token = "hurgle";
			String pan = "4242424242424242";
			String expdate = "1901";
			String phone = "0000000000";
			String email = "bob@smith.com";
			String note = "my note";
			String cust_id = "customer1";
			String crypt_type = "7";
			String processing_country_code = "CA";
			Boolean status_check = false;

			AvsInfo avsCheck = new AvsInfo();
			avsCheck.SetAvsStreetNumber("212");
			avsCheck.SetAvsStreetName("Payton Street");
			avsCheck.SetAvsZipCode("M1M1M1");

			//Credential on File details
			CofInfo cof = new CofInfo();
			cof.SetIssuerId("139X3130ASCXAS9");

			ResUpdateCC resUpdateCC = new ResUpdateCC();
			resUpdateCC.SetDataKey(data_key);
			resUpdateCC.SetAvsInfo(avsCheck);
			resUpdateCC.SetCustId(cust_id);
			resUpdateCC.SetPan(pan);
			resUpdateCC.SetExpDate(expdate);
			resUpdateCC.SetPhone(phone);
			resUpdateCC.SetEmail(email);
			resUpdateCC.SetNote(note);
			resUpdateCC.SetCryptType(crypt_type);
			resUpdateCC.SetCofInfo(cof);

			HttpsPostRequest mpgReq = new HttpsPostRequest();
			mpgReq.SetProcCountryCode(processing_country_code);
			mpgReq.SetTestMode(true); //false or comment out this line for production transactions
			mpgReq.SetStoreId(store_id);
			mpgReq.SetApiToken(api_token);
			mpgReq.SetTransaction(resUpdateCC);
			mpgReq.SetStatusCheck(status_check);
			mpgReq.Send();

			try
            {
                Receipt receipt = mpgReq.GetReceipt();
                var response = new
                {
                    Id = receipt.GetDataKey(),
                    Success = receipt.GetResSuccess(),
                    FirstName = receipt.GetResCustFirstName(),
                    LastName = receipt.GetResCustLastName(),
                    MaskedPan = receipt.GetResMaskedPan(),
                    ExpDate = receipt.GetResExpDate()

                };

                if (response.Success == "true")
                    return Ok(response);
                else
                    return BadRequest("one");

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }


        }
    }
}
