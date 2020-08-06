using Microsoft.AspNetCore.Mvc;
using Moneris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team5_payment.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RefundController : ControllerBase
	{
		//POST: /api/getDetails
		public ActionResult Refund(String amount, String crypt, String order_id, String txn_number)
		{
			String store_id = "store1";
			String api_token = "yesguy";
			//String amount = "1.00"; //mandatory field
			//String crypt = "7"; //mandatory field
			String dynamic_descriptor = "123456";
			String custid = "mycust9";
			//String order_id = "mvt2713618548"; //mandatory field
			//String txn_number = "911464-0_10"; //mandatory field
			String processing_country_code = "CA";
			Boolean status_check = false;

			Refund refund = new Refund();
			refund.SetTxnNumber(txn_number);
			refund.SetOrderId(order_id);
			refund.SetAmount(amount);
			refund.SetCryptType(crypt);
			refund.SetCustId(custid);
			refund.SetDynamicDescriptor(dynamic_descriptor);

			HttpsPostRequest mpgReq = new HttpsPostRequest();
			mpgReq.SetProcCountryCode(processing_country_code);
			mpgReq.SetTestMode(true); //false or comment out this line for production transactions
			mpgReq.SetStoreId(store_id);
			mpgReq.SetApiToken(api_token);
			mpgReq.SetTransaction(refund);
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
				};
				if (response.Success == "true")
				{
					return Ok(response);
				}
				else
				{
					return BadRequest(response);
				}


			}
			catch (Exception e)
			{
				return BadRequest(e.ToString());
			}
		}
	}
}
