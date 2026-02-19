using System;
using DesignPatternChallenge.Interfaces;
using DesignPatternChallenge.Legacy;
using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Adapters
{
    public class LegacyPaymentAdapter : IPaymentProcessor
    {
        private readonly LegacyPaymentSystem _legacySystem;

        public LegacyPaymentAdapter(LegacyPaymentSystem legacySystem)
        {
            _legacySystem = legacySystem;
        }

        public PaymentResult ProcessPayment(PaymentRequest request)
        {
            Console.WriteLine("[Adapter] Converting call to legacy system...");

            // Data adaptation
            // Convert CVV from string to int
            int.TryParse(request.Cvv, out int cvvCode);

            // Convert Amount to double in cents
            double amountInCents = (double)(request.Amount * 100);

            // Call to legacy system
            var response = _legacySystem.AuthorizeTransaction(
                request.CreditCardNumber,
                cvvCode,
                request.ExpirationDate.Month,
                request.ExpirationDate.Year,
                amountInCents,
                request.CustomerEmail
            );

            // Response adaptation
            return new PaymentResult
            {
                Success = response.ResponseCode == "00",
                TransactionId = response.TransactionRef,
                Message = response.ResponseMessage
            };
        }

        public bool RefundPayment(string transactionId, decimal amount)
        {
            Console.WriteLine("[Adapter] Converting refund request...");
            double amountInCents = (double)(amount * 100);
            return _legacySystem.ReverseTransaction(transactionId, amountInCents);
        }

        public PaymentStatus CheckStatus(string transactionId)
        {
            Console.WriteLine("[Adapter] Converting status check...");
            var status = _legacySystem.QueryTransactionStatus(transactionId);

            return status == "APPROVED" ? PaymentStatus.Approved :
                   status == "DECLINED" ? PaymentStatus.Declined :
                   PaymentStatus.Pending;
        }
    }
}
