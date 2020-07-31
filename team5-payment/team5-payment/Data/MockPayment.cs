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

        public Dictionary<string, string> deleteCard(Card card)
        {
            Receipt receipt = postRequest.GetReceipt();

            ResDelete resDelete = new ResDelete(receipt.GetDataKey());

            string store_id = "store5";
            string api_token = "yesguy";
            string data_key = "PjVKjtEmc1FvFyjxHE4EwBMxi";

            ResLookupMasked resLookupMasked = new ResLookupMasked();
            resLookupMasked.SetDataKey(data_key);
            card.cardID = data_key;

            string processing_country_code = "CA";
            bool status_check = false;

            postRequest.SetProcCountryCode(processing_country_code);
            postRequest.SetTestMode(true); //false or comment out this line for production transactions
            postRequest.SetStoreId(store_id);
            postRequest.SetApiToken(api_token);
            postRequest.SetTransaction(resDelete);
            postRequest.SetStatusCheck(status_check);
            postRequest.Send();
            postRequest.SetTransaction(resDelete);
                    

            var result = new Dictionary<string, string>
            {
                {"Card ID", $"{data_key}" },
                {"Vault Success", $"{card.cardID}" }
            };

            return result;
        }

        
    }
}
