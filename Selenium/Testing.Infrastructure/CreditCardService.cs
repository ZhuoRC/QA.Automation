using Bogus;
using Bogus.DataSets;
using System;

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

        public CreditCard()
        {

        }

        public CreditCard(CardType cardType, int validYear = 4)
        {
            var faker = new Faker();

            this.PAN = faker.Finance.CreditCardNumber(cardType).Replace("-", "");
            this.MaskedPAN = CreditCardService.MaskPAN(this.PAN);

            this.ExpiryDate = DateTime.Now.AddYears(validYear);
            this.ExpiryMonth = faker.Random.Int(1, 12);
            this.ExpiryYear = this.ExpiryDate.Year;
            this.CVV = faker.Finance.CreditCardCvv();
        }

        public CreditCard(string pan, int expiryMonth, int expiryYear)
        {

            this.PAN = pan;
            this.MaskedPAN = CreditCardService.MaskPAN(this.PAN);
            this.ExpiryMonth = expiryMonth;
            this.ExpiryYear = expiryYear;
        }

    }
}
