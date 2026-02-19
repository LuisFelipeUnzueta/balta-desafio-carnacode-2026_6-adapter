using DesignPatternChallenge.Adapters;
using DesignPatternChallenge.Legacy;
using DesignPatternChallenge.Services;

Console.WriteLine("=== Checkout System ===\n");

// SCENARIO 1: Modern System
Console.WriteLine("--- Scenario 1: Using Modern Processor ---");
var modernProcessor = new ModernPaymentProcessor();
var checkoutWithModern = new CheckoutService(modernProcessor);
checkoutWithModern.CompleteOrder("customer@email.com", 150.00m, "4111111111111111");

Console.WriteLine("\n" + new string('-', 60) + "\n");

// SCENARIO 2: Legacy System via Adapter
Console.WriteLine("--- Scenario 2: Using Legacy System via Adapter ---");

// 1. Create instance of legacy system
var legacySystem = new LegacyPaymentSystem();

// 2. Create the Adapter, passing the legacy system
var legacyAdapter = new LegacyPaymentAdapter(legacySystem);

// 3. Inject the Adapter into the checkout service
// CheckoutService doesn't even know it's talkting to a legacy system!
var checkoutWithLegacy = new CheckoutService(legacyAdapter);

checkoutWithLegacy.CompleteOrder("legacy.customer@email.com", 200.00m, "4222222222222222");

Console.WriteLine("\n" + new string('=', 60) + "\n");
