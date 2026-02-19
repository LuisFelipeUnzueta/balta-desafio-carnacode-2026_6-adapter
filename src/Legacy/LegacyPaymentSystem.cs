using System;

namespace DesignPatternChallenge.Legacy
{
    // Legacy system with completely different interface
    public class LegacyPaymentSystem
    {
        // Methods with incompatible signatures
        public LegacyTransactionResponse AuthorizeTransaction(
            string cardNum,
            int cvvCode,
            int expMonth,
            int expYear,
            double amountInCents,
            string customerInfo)
        {
            Console.WriteLine($"[Legacy System] Authorizing transaction...");
            Console.WriteLine($"Card: {cardNum}");
            Console.WriteLine($"Amount: {amountInCents / 100:C}");

            // Simulation of processing
            var response = new LegacyTransactionResponse
            {
                AuthCode = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                ResponseCode = "00",
                ResponseMessage = "TRANSACTION APPROVED",
                TransactionRef = $"LEG{DateTime.Now.Ticks}"
            };

            return response;
        }

        public bool ReverseTransaction(string transRef, double amountInCents)
        {
            Console.WriteLine($"[Legacy System] Reversing transaction {transRef}");
            Console.WriteLine($"Amount: {amountInCents / 100:C}");
            return true;
        }

        public string QueryTransactionStatus(string transRef)
        {
            Console.WriteLine($"[Legacy System] Querying transaction {transRef}");
            return "APPROVED";
        }
    }
}
