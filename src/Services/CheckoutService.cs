using System;
using DesignPatternChallenge.Interfaces;
using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Services
{
    // Application class that uses the modern interface
    public class CheckoutService
    {
        private readonly IPaymentProcessor _paymentProcessor;

        public CheckoutService(IPaymentProcessor paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        public void CompleteOrder(string customerEmail, decimal amount, string cardNumber)
        {
            Console.WriteLine($"\n=== Completing Order ===");
            Console.WriteLine($"Customer: {customerEmail}");
            Console.WriteLine($"Amount: {amount:C}\n");

            var request = new PaymentRequest
            {
                CustomerEmail = customerEmail,
                Amount = amount,
                CreditCardNumber = cardNumber,
                Cvv = "123",
                ExpirationDate = new DateTime(2026, 12, 31),
                Description = "Product purchase"
            };

            var result = _paymentProcessor.ProcessPayment(request);

            if (result.Success)
            {
                Console.WriteLine($"✅ Order approved! ID: {result.TransactionId}");
            }
            else
            {
                Console.WriteLine($"❌ Payment declined: {result.Message}");
            }
        }
    }
}
