using Moneris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team5_payment.Models;

namespace team5_payment.Data
{
    public class MockPreAuthorization : IPreAuthorization
    {
        public object PreAuthWithVault(PaymentProccess paymentProccess)
        {
            /*
             * orderId: "test1231"
             * amount: "1.00"
             * dataKey: "HWjc2epTDXmMRwEErYQGYNQ73"
             */
            string store_id = "store5";
            string api_token = "yesguy";
            string crypt_type = "7";
            string processing_country_code = "CA";

            string order_id = paymentProccess.OrderId;
            string amount = paymentProccess.Amount;
            string data_key = paymentProccess.DataKey;

            CofInfo cof = new CofInfo();
            cof.SetPaymentIndicator("U");
            cof.SetPaymentInformation("2");
            cof.SetIssuerId("168451306048014");

            ResPreauthCC resPreauthCC = new ResPreauthCC();
            resPreauthCC.SetDataKey(data_key);
            resPreauthCC.SetOrderId(order_id);
            resPreauthCC.SetAmount(amount);
            resPreauthCC.SetCryptType(crypt_type);
            resPreauthCC.SetCofInfo(cof);

            HttpsPostRequest mpgReq = new HttpsPostRequest();
            mpgReq.SetProcCountryCode(processing_country_code);
            mpgReq.SetTestMode(true); //false or comment out this line for production transactions
            mpgReq.SetStoreId(store_id);
            mpgReq.SetApiToken(api_token);
            mpgReq.SetTransaction(resPreauthCC);
            mpgReq.Send();


            Receipt receipt = mpgReq.GetReceipt();

            var response = new { Id = receipt.GetDataKey(), Success = Boolean.Parse(receipt.GetResSuccess()), Txn = receipt.GetTxnNumber() };
            
            if (response.Success)
                return response;
            else
                throw new Exception();


        }

        public object PreAuth()
        {
            throw new NotImplementedException();
        }

        public object PreAuthCompletion(string txn)
        {
            throw new NotImplementedException();
        }
    }
}
