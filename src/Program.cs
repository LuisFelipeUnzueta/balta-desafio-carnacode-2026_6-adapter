using System;
using DesignPatternChallenge.Adapters;
using DesignPatternChallenge.Legacy;
using DesignPatternChallenge.Services;

namespace DesignPatternChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Checkout ===\n");

            // CENÁRIO 1: Sistema Moderno
            Console.WriteLine("--- Cenário 1: Usando Processador Moderno ---");
            var modernProcessor = new ModernPaymentProcessor();
            var checkoutWithModern = new CheckoutService(modernProcessor);
            checkoutWithModern.CompleteOrder("cliente@email.com", 150.00m, "4111111111111111");

            Console.WriteLine("\n" + new string('-', 60) + "\n");

            // CENÁRIO 2: Sistema Legado via Adapter
            Console.WriteLine("--- Cenário 2: Usando Sistema Legado via Adapter ---");

            // 1. Criamos a instância do sistema legado (Adapter)
            var legacySystem = new LegacyPaymentSystem();

            // 2. Criamos o Adapter, passando o sistema legado
            var legacyAdapter = new LegacyPaymentAdapter(legacySystem);

            // 3. Injetamos o Adapter no serviço de checkout
            // O CheckoutService nem sabe que está falando com um sistema legado!
            var checkoutWithLegacy = new CheckoutService(legacyAdapter);

            checkoutWithLegacy.CompleteOrder("cliente.legado@email.com", 200.00m, "4222222222222222");

            Console.WriteLine("\n" + new string('=', 60) + "\n");
        }
    }
}
