using Newtonsoft.Json;
using Fraud_API.Models; 
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fraud_API.Services
{
    public class FraudDetectionService
    {
        private static Dictionary<string, SuspiciousModel.SuspiciousTransaction> _suspiciousTransactions = new Dictionary<string, SuspiciousModel.SuspiciousTransaction>();
        private static readonly decimal _transactionLimit = 10000m;

        // Метод для загрузки подозрительных транзакций из JSON
        public static void LoadSuspiciousTransactions(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var transactions = JsonConvert.DeserializeObject<List<SuspiciousModel.SuspiciousTransaction>>(json);

            foreach (var transaction in transactions)
            {
                if (!_suspiciousTransactions.ContainsKey(transaction.TID))
                {
                    _suspiciousTransactions[transaction.TID] = transaction;
                }
            }
        }

        // Метод для проверки на мошенничество
        public static bool IsFraudulent(TransactionRequest transaction)
        {
            //Проверка на большие суммы
            if (transaction.Amount > _transactionLimit)
            {
                return true;
            }

            //Проверка наличия подозрительной транзакции по уникальному ID
            if (_suspiciousTransactions.TryGetValue(transaction.TransactionId, out var suspiciousTransaction))
            {                
                if (suspiciousTransaction.SumAm == transaction.Amount &&
                    suspiciousTransaction.CN == transaction.CardNumber &&
                    suspiciousTransaction.LocationL == transaction.Location &&
                    suspiciousTransaction.Date == transaction.Date)
                {
                    return true; 
                }
            }

            //Проверка на частоту транзакций за последний час
            var recentTransactions = _suspiciousTransactions.Values
                .Where(t => t.CN == transaction.CardNumber && t.Date > DateTime.Now.AddHours(-1))
                .ToList();

            // Считаем только транзакции с текущим номером карты
            if (recentTransactions.Count >= 5) // Если более 5 транзакций за последний час
            {
                return true;
            }

            return false;
        }
    }
}
