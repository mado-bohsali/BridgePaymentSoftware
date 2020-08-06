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
    public class GetDetailsController : ControllerBase
    {
        //POST: /api/getDetails
        public ActionResult RetrieveCard(string data_key)
        { // data_key =  "Bcjvur5vxbOMzSaDH43BfFwS2"
            string store_id = "store5";
            string api_token = "yesguy";
            string processing_country_code = "CA";
            bool status_check = false;

            ResLookupMasked resLookupMasked = new ResLookupMasked();
            resLookupMasked.SetDataKey(data_key);

            HttpsPostRequest mpgReq = new HttpsPostRequest();
            mpgReq.SetProcCountryCode(processing_country_code);
            mpgReq.SetTestMode(true); //false or comment out this line for production transactions
            mpgReq.SetStoreId(store_id);
            mpgReq.SetApiToken(api_token);
            mpgReq.SetTransaction(resLookupMasked);
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
