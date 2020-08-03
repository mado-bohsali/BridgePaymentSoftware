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

        public static Receipt receipt;

        public Dictionary<string, string> deleteCard(string cardID)
        {
            string store_id = "store5";
            string api_token = "yesguy";

            //Vault Look Up Masked transaction object
            //returned to the merchant (via the Receipt object) when the profile is first registered.
            //ResLookupMasked resLookupMasked = new ResLookupMasked();
            //resLookupMasked.SetDataKey(cardID);

            //Vault Delete transaction object
            ResDelete resDelete = new ResDelete(cardID);

            HttpsPostRequest postRequest = new HttpsPostRequest();

            string processing_country_code = "CA";
            bool status_check = false;

            postRequest.SetProcCountryCode(processing_country_code);
            postRequest.SetTestMode(true); //false or comment out this line for production transactions
            postRequest.SetStoreId(store_id);
            postRequest.SetApiToken(api_token);
            postRequest.SetStatusCheck(status_check);
            postRequest.SetTransaction(resDelete);
            postRequest.Send();

            //To collect the applicable response details
            receipt = postRequest.GetReceipt();

            var result = new Dictionary<string, string>
            {
                {"Card ID", "DxwdemrvfnoXO1HhmRikfw3gA" },
                {"Vault ResSuccess", $"{receipt.GetResSuccess()}" }
            };

            return result;
        }

        
    }
}
