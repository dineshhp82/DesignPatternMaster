using SuperMarket.ValueObjects;
using System.Text.RegularExpressions;

namespace SuperMarket.Payment
{
    public class CreditCardPayment : PaymentDetails
    {
        public CreditCardPayment(Money amount,
            string cardNumber,
            string expiryDate,
            string cvv,
            string cardHolderName) :
            base(amount, "CreditCard")
        {
            CardNumber = cardNumber ?? throw new ArgumentNullException(nameof(cardNumber));
            ExpiryDate = expiryDate ?? throw new ArgumentNullException(nameof(expiryDate));
            CVV = cvv ?? throw new ArgumentNullException(nameof(cvv));
            CardHolderName = cardHolderName ?? throw new ArgumentNullException(nameof(cardHolderName));
            CardType = DetermineCardType(cardNumber);
            
        }

        public string CardNumber { get; private set; }
        public string ExpiryDate { get; private set; }
        public string CVV { get; private set; }
        public string CardHolderName { get; private set; }
        public CardType CardType { get; private set; }

        private CardType DetermineCardType(string cardNumber)
        {
            var cleanNumber = Regex.Replace(cardNumber, @"[^\d]", "");

            if (cleanNumber.StartsWith("4")) return CardType.Visa;
            if (cleanNumber.StartsWith("5")) return CardType.MasterCard;
            if (cleanNumber.StartsWith("34") || cleanNumber.StartsWith("37")) return CardType.AmericanExpress;
            if (cleanNumber.StartsWith("6")) return CardType.Discover;

            return CardType.Unknown;
        }

        public override string GetMaskedDetails()
        {
            var maskedCardNumber = CardNumber; //MaskCardNumber(CardNumber);
            return $"Credit Card: {maskedCardNumber}, Exp: {ExpiryDate}, Holder: {CardHolderName}";
        }

        public override bool Validate()
        {
            if (string.IsNullOrWhiteSpace(CardNumber))// || !IsValidCardNumber(CardNumber))
                return false;

            if (string.IsNullOrWhiteSpace(ExpiryDate))// || !IsValidExpiryDate(ExpiryDate))
                return false;

            if (string.IsNullOrWhiteSpace(CVV))// || !IsValidCVV(CVV, CardType))
                return false;

            if (string.IsNullOrWhiteSpace(CardHolderName))
                return false;

            if (Amount.Amount <= 0)
                return false;

            return true;
        }
    }
}
