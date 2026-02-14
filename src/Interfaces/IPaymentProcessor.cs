using System;
using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Interfaces
{
    public interface IPaymentProcessor
    {
        PaymentResult ProcessPayment(PaymentRequest request);
        bool RefundPayment(string transactionId, decimal amount);
        PaymentStatus CheckStatus(string transactionId);
    }
}
