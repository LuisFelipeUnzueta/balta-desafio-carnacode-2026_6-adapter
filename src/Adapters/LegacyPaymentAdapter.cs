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
            Console.WriteLine("[Adapter] Convertendo chamada para sistema legado...");

            // Adaptação dos dados
            // Converter CVV de string para int
            int.TryParse(request.Cvv, out int cvvCode);
            
            // Converter Amount para double em centavos
            double amountInCents = (double)(request.Amount * 100);

            // Chamada ao sistema legado
            var response = _legacySystem.AuthorizeTransaction(
                request.CreditCardNumber,
                cvvCode,
                request.ExpirationDate.Month,
                request.ExpirationDate.Year,
                amountInCents,
                request.CustomerEmail
            );

            // Adaptação da resposta
            return new PaymentResult
            {
                Success = response.ResponseCode == "00",
                TransactionId = response.TransactionRef,
                Message = response.ResponseMessage
            };
        }

        public bool RefundPayment(string transactionId, decimal amount)
        {
            Console.WriteLine("[Adapter] Convertendo solicitação de reembolso...");
            double amountInCents = (double)(amount * 100);
            return _legacySystem.ReverseTransaction(transactionId, amountInCents);
        }

        public PaymentStatus CheckStatus(string transactionId)
        {
            Console.WriteLine("[Adapter] Convertendo verificação de status...");
            var status = _legacySystem.QueryTransactionStatus(transactionId);
            
            return status == "APPROVED" ? PaymentStatus.Approved : 
                   status == "DECLINED" ? PaymentStatus.Declined : 
                   PaymentStatus.Pending;
        }
    }
}
