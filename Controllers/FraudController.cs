using Microsoft.AspNetCore.Mvc;
using Fraud_API.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;


namespace Fraud_API.Controllers
{
    [Route("api")]
    [ApiController]
    public class FraudController : ControllerBase
    {
        public FraudController()
        {
        }
        /// <summary>
        /// Создание продукта
        /// </summary>
        [HttpPost("GetFraud")] //api/GetFraud
        [SwaggerOperation(
            Summary = "Получить пользователя по ID", 
            Description = "Возвращает данные пользователя, если он найден."
        )]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(typeof(TransactionRequest), typeof(TransactionRequestExample))]
        public ActionResult GetFraudByName([FromBody] TransactionRequest fr)
        {
            if (fr.Validate() != null)
            {
                return BadRequest( fr.Validate() );
            }

            else
            {
                var result = Services.FraudDetectionService.IsFraudulent(fr);
                return Ok(new TransactionResponse() { Success = result });
            }
            
        }

        /// <summary>
        /// Получение журнала проверок
        /// </summary>
        [HttpGet("GetLog")] // api/GetLog
        [SwaggerOperation(
            Summary = "Получить журнал проверок",
            Description = "Возвращает журнал проверок по указанному номеру карты."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AuditLogEntry>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetLog([FromQuery] string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                return BadRequest("Номер карты не может быть пустым.");
            }

            var logEntries = Services.FraudDetectionService.GetLogByCardNumber(cardNumber);
            return Ok(logEntries);
        }

    }
}
