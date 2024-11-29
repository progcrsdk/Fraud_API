using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.Swagger;


namespace Fraud_API.Models
{
    public class TransactionRequestExample : IExamplesProvider<TransactionRequest>
    {
        
        //public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        //{
        //    if (type == typeof(TransactionRequest))
        //    {
        //        schema.example = new TransactionRequest
        //        {
        //            TransactionId = "TRX12345678",
        //            Amount = 250.75M,
        //            CardNumber = "4111111111111111",
        //            Date = DateTime.Now,
        //            Location = "New York, USA"
        //        };
        //    }
            
        //}

        public TransactionRequest GetExamples()
        {
            return new TransactionRequest
            {
                TransactionId = "TRX12345678",
                Amount = 250.75M,
                CardNumber = "4111111111111111",
                Date = DateTime.Now,
                Location = "New York, USA"
            };
        }
    }
}
