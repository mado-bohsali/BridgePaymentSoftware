using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Moneris;

namespace team5_payment.Data
{
    public class MockPayment : IPayment
    {
        public MockPayment()
        {
        }

        public static HttpsPostRequest postRequest = new HttpsPostRequest();

        public Dictionary<string, string> deleteCard(string cardID)
        {
            string store_id = "store5";
            string api_token = "yesguy";

            //Vault Look Up Masked transaction object
            //returned to the merchant (via the Receipt object) when the profile is first registered.
            //ResLookupMasked resLookupMasked = new ResLookupMasked();
            //resLookupMasked.SetDataKey(cardID);

            //To collect the applicable response details
            Receipt receipt = postRequest.GetReceipt();

            //Vault Delete transaction object
            ResDelete resDelete = new ResDelete(cardID);

            string processing_country_code = "CA";
            bool status_check = false;

            postRequest.SetProcCountryCode(processing_country_code);
            postRequest.SetTestMode(true); //false or comment out this line for production transactions
            postRequest.SetStoreId(store_id);
            postRequest.SetApiToken(api_token);
            postRequest.SetTransaction(resDelete);
            postRequest.SetStatusCheck(status_check);
            postRequest.SetTransaction(resDelete);
            postRequest.Send();


            var result = new Dictionary<string, string>
            {
                {"Card ID", $"{cardID}" },
                {"Vault Success", $"{receipt.GetResSuccess()}" }
            };

            return result;
        }


    }
}
