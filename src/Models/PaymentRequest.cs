using System;

namespace DesignPatternChallenge.Models
{
    public class PaymentRequest
    {
        public string CustomerEmail { get; set; }
        public decimal Amount { get; set; }
        public string CreditCardNumber { get; set; }
        public string Cvv { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Description { get; set; }
    }
}
