namespace Tickets.Application.Models.Payment
{
    public class StripeSettings
    {
        public string? PublishableKey { get; set; }
        public string? SecretKey { get; set; }
        public string? Currency { get; set; }
    }
}
