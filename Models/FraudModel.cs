using Swashbuckle.AspNetCore.Annotations;

namespace Fraud_API.Models
{
    /// <summary>
    /// класс для запроса
    /// </summary>
    public class TransactionRequest
    {
        [SwaggerSchema("Уникальный идентификатор транзакции")]        
        public string TransactionId { get; set; }
        [SwaggerSchema("Сумма транзакции в десятичном формате")]
        public decimal Amount { get; set; }
        [SwaggerSchema("Номер банковской карты в формате строки")]
        public string CardNumber { get; set; }
        [SwaggerSchema("Дата и время транзакции", Format = "date-time")]
        public DateTime Date { get; set; }
        [SwaggerSchema("Локация проведения транзакции")]
        public string Location { get; set; }
                
        public string Validate()
        {
            if (string.IsNullOrWhiteSpace(TransactionId))
                return "TransactionId is required.";

            if (Amount <= 0)
                return "Amount must be greater than zero.";

            if (string.IsNullOrWhiteSpace(CardNumber) || CardNumber.Length != 16) // Номер карты должен быть 16 символов
                return "CardNumber must be 16 digits long.";

            if (Date > DateTime.Now)
                return "Date cannot be in the future.";

            if (string.IsNullOrWhiteSpace(Location))
                return "Location is required.";

            return null; // Все поля валидны
        }


    }

    /// <summary>
    /// класс для ответа
    /// </summary>
    public class TransactionResponse
    {
        public bool Success { get; set; }
    }

}
