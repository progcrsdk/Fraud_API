namespace Fraud_API.Models
{
    public class AuditLogEntry
    {
        public string TransactionId { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public bool IsFraudulent { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
