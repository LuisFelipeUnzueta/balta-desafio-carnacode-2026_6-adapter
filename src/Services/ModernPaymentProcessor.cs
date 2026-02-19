using System;
using DesignPatternChallenge.Interfaces;
using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Services
{
    // Modern implementation that works well
    public class ModernPaymentProcessor : IPaymentProcessor
    {
        public PaymentResult ProcessPayment(PaymentRequest request)
        {
            Console.WriteLine("[Modern Processor] Processing payment...");
            return new PaymentResult
            {
                Success = true,
                TransactionId = Guid.NewGuid().ToString(),
                Message = "Payment approved"
            };
        }

        public bool RefundPayment(string transactionId, decimal amount)
        {
            Console.WriteLine($"[Modern Processor] Refunding {amount:C}");
            return true;
        }

        public PaymentStatus CheckStatus(string transactionId)
        {
            return PaymentStatus.Approved;
        }
    }
}
