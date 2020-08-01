using Microsoft.AspNetCore.Server.IIS.Core;
using Moneris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team5_payment.Models;

namespace team5_payment.Data
{
    public class MockPurchaseRepo : IPurchaseRepo
    {
        public object ProcessSavedPayment(PaymentProccess paymentProcess)
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


            Receipt receipt = mpgReq.GetReceipt();
            var response = new { Id = receipt.GetDataKey(), Success = Boolean.Parse(receipt.GetResSuccess()) };

            if (response.Success)
                return response;
            else
                throw new Exception();


        }
    }
}
