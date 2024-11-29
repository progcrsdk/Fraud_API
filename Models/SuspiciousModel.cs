namespace Fraud_API.Models
{
    public class SuspiciousModel
    {
        public class SuspiciousTransaction
        {
            public string TID { get; set; }
            public decimal SumAm { get; set; }
            public string CN { get; set; }
            public DateTime Date { get; set; }
            public string LocationL { get; set; }
        }
    }
}
