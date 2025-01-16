# Fraud_API
Этот API предоставляет возможность проверки на мошенничество транзакций на основе пользовательских данных. Он позволяет выявлять подозрительные транзакции с помощью анализа различных параметров, таких как сумма, номер карты, местоположение и частота транзакций. API разработан с целью помочь компаниям и организациям снижать риск мошенничества и обеспечивать безопасность своих клиентов и операций.

- Проверка транзакций на превышение установленного лимита.
- Анализ транзакций на содержание фрода.
### Request (запрос)
##### Конечная точка
**POST** [https://fraudapi-production.up.railway.app/api/GetFraud]
##### Описание
Эндпоинт для проверки транзакций на мошенничество на основе предоставленных данных. Пользователь отправляет запрос, который сравнивается с информацией о транзакциях в JSON-файле, а API возвращает результаты проверки.

### Параметры запроса
- **TransactionId** (string): уникальный идентификатор транзакции.
- **Amount** (decimal): сумма транзакции.
- **CardNumber** (string): номер карты, с которой проводилась транзакция.
- **Date** (string): дата и время транзакции в формате ISO 8601 (например, "2023-10-15T14:30:00").
- **Location** (string): местоположение, с которого проводилась транзакция.
### Пример запроса
``` json
{
    "transactionId": "TRX001",
    "amount": 1500.50,
    "cardNumber": "1234567890123456",
    "date": "2023-10-15T14:30:00",
    "location": "LocationA"
}
```

### Response (ответ)
### Параметры ответа
- **success** (bool): флаг, указывающий является ли транзакция мошеннической.

### Пример ответа
``` json
{
    "success": true,
}
```
