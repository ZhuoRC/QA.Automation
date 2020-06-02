using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testing.Infrastructure
{
    static public class CreditCardService
    {

        static public string MaskPAN(string pan)
        {
            string maskedPan = pan;
            if (pan.Length > 4)
            {
                maskedPan = string.Concat(
                        "".PadLeft(pan.Length - 4, '*'),
                        pan.Substring(pan.Length - 4)
                );
            }

            if (pan.Length > 8)
            {
                maskedPan = string.Concat(
                    pan.Substring(0, 4),
                     "".PadLeft(pan.Length - 8, '*'),
                    maskedPan.Substring(pan.Length - 4)
                );
            }

            return maskedPan;
        }
    }

    public class CreditCard
    {
        public string PAN { get; set; }
        public string MaskedPAN { get { return CreditCardService.MaskPAN(this.PAN); } set {; } }

        public DateTime ExpiryDate { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CVV { get; set; }
        public string BillingCurrency { get; set; }

        public CreditCard(CardType cardType, int validYear=4)
        {
            var faker = new Faker();

            this.PAN = faker.Finance.CreditCardNumber(cardType).Replace("-", "");
            this.MaskedPAN = CreditCardService.MaskPAN(this.PAN);

            this.ExpiryDate = DateTime.Now.AddYears(validYear);
            this.ExpiryMonth = this.ExpiryDate.Month;
            this.ExpiryYear = this.ExpiryDate.Year;
            this.CVV = faker.Finance.CreditCardCvv();
        }


    }
}
