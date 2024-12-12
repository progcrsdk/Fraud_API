using Fraud_API.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Fraud_API.Services;
using Microsoft.OpenApi.Models;

namespace Fraud_API
{
    public class Program
    {
        private const string _commandTest = "http --url=busy-pet-rhino.ngrok-free.app 5053";             
        
        public static async Task Main(string[] args)
        {
            string fPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "suspicious_transactions.json");
            FraudDetectionService.LoadSuspiciousTransactions(fPath);
            string desktopPath = Path.Combine(Environment.CurrentDirectory, "ngrok.exe");
            RunExternalApp(desktopPath, _commandTest);

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();           
            });
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "Fraud API",
                    Description = "Этот API предоставляет возможность проверки на мошенничество транзакций на основе пользовательских данных. Он позволяет выявлять подозрительные транзакции с помощью анализа различных параметров, таких как сум-ма, номер карты, местоположение и частота транзакций. API разработан с целью помочь компаниям и организациям снижать риск мошенничества и обеспечи-вать безопасность своих клиентов и операций",
                    
                });
            });
            // Настройки CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // Добавляем контроллеры
            builder.Services.AddControllers();
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Включаем использование CORS
            app.UseCors("AllowAllOrigins");
            // Включаем маршрутизацию
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // Настраиваем маршруты для контроллеров
            app.MapControllers();

            // Запускаем приложение
            app.Run();
        }
        private static void RunExternalApp(string filePath, string arguments)
        {
            try
            {
                // Настраиваем процесс для запуска внешнего приложения
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = filePath, // Указанный путь к ngrok.exe
                    Arguments = arguments, // Аргументы для ngrok
                    UseShellExecute = true, // Позволяет запускать приложение в отдельной консоли
                    CreateNoWindow = false // Создает отдельное окно для приложения
                };
                // Запускаем процесс
                Process.Start(processStartInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to run external app: " + ex.Message);
            }
        }
    }
}



