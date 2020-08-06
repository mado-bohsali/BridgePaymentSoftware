using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Moneris;
using team5_payment.Models;

namespace team5_payment.Data
{
    public class MockVaultRepo : IVaultRepo
    {

        public object SaveCard(SaveCard saveCard)
        {
            /*
         * For Testing:
         * 
         * pan: "4242424242424242";
         * expDate: "2022"
         * streetNumber: "212"
         * streetName: "Payton Street"
         * zipCode: "M1M1M1"
         */

            //Mandatory info given by the store
            string store_id = "store5";
            string api_token = "yesguy";

            string pan = saveCard.Pan;
            string expdate = saveCard.ExpDate;
            string crypt_type = "7"; //crypt type 7 for ecommerce

            AvsInfo avsCheck = new AvsInfo();
            avsCheck.SetAvsStreetNumber(saveCard.StreetNumber);
            avsCheck.SetAvsStreetName(saveCard.StreetName);
            avsCheck.SetAvsZipCode(saveCard.ZipCode);

            ResAddCC resaddcc = new ResAddCC();
            resaddcc.SetPan(pan);
            resaddcc.SetExpDate(expdate);
            resaddcc.SetCryptType(crypt_type);

            //Mandatory - Credential on File details
            CofInfo cof = new CofInfo();
            cof.SetIssuerId("139X3130ASCXAS9"); //can be obtained by performing card verification

            resaddcc.SetCofInfo(cof);

            HttpsPostRequest mpgReq = new HttpsPostRequest();
            mpgReq.SetTestMode(true); //false or comment out this line for production transactions
            mpgReq.SetStoreId(store_id);
            mpgReq.SetApiToken(api_token);
            mpgReq.SetProcCountryCode("CA");
            mpgReq.SetTransaction(resaddcc);
            mpgReq.Send();



            Receipt receipt = mpgReq.GetReceipt();
            var response = new { Id = receipt.GetDataKey(), Success = Boolean.Parse(receipt.GetResSuccess()) };

            if (response.Success)
                return response;
            else
                throw new Exception();
            
        }

        public object RetreiveCard(string data_key)
        {

            // data_key =  "Bcjvur5vxbOMzSaDH43BfFwS2"

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

            Receipt receipt = mpgReq.GetReceipt();
            var response = new 
            {
                Id = receipt.GetDataKey(),
                Success = Boolean.Parse(receipt.GetResSuccess()),
                FirstName = receipt.GetResCustFirstName(),
                LastName = receipt.GetResCustLastName(),
                MaskedPan = receipt.GetResMaskedPan(),
                ExpDate = receipt.GetResExpDate()


            };

            if (response.Success)
                return response;
            else
                throw new Exception();
        }
    }
}
