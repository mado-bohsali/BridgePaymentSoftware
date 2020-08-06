using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Moneris;
using team5_payment.Models;
using System.Net.Http;
using System.Net;

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
            
            //--------

            //Vault Delete transaction object
            ResDelete resDelete = new ResDelete(cardID);

            HttpsPostRequest postRequest = new HttpsPostRequest();

            string processing_country_code = "CA";
            bool status_check = false;
            string store_id = "store5";
            string api_token = "yesguy";
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
                {"Card ID", $"{receipt.GetDataKey()}" },
                {"Vault ResSuccess", $"{receipt.GetResSuccess()}" }
            };

            return result;
        }

        HttpWebResponse IPayment.deletePayPalCard(string credit_card_id)
        {
            string sandboxAPIURL = "https://api.sandbox.paypal.com/v2/vault/credit-cards/"+$"{credit_card_id}";
            WebRequest request = WebRequest.Create(sandboxAPIURL);
            request.Method = "DELETE";

            return (HttpWebResponse)request.GetResponse();
        }

    }
}
