using System;
using Moneris;


namespace team5_payment.Data
{
    public class Card
    {
        private string firstName;
        private string lastName;
        private string cardNumber;
        public string maskCard;
        public int cvvNumber;
        public string expiryDate;


        public Card(string firstName, string lastName, string cardNumber, int cvvNumber, string expiryDate)
        {
            this.cardNumber = cardNumber;
            this.firstName = firstName;
            this.lastName = lastName;
            this.cvvNumber = cvvNumber;
            this.expiryDate = expiryDate;
            this.maskCard = (new HttpsPostRequest()).GetReceipt().GetMaskedPan();
        }
    }
}
