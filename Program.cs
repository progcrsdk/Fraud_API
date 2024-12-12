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
                    Description = "���� API ������������� ����������� �������� �� ������������� ���������� �� ������ ���������������� ������. �� ��������� �������� �������������� ���������� � ������� ������� ��������� ����������, ����� ��� ���-��, ����� �����, �������������� � ������� ����������. API ���������� � ����� ������ ��������� � ������������ ������� ���� ������������� � ��������-���� ������������ ����� �������� � ��������",
                    
                });
            });
            // ��������� CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // ��������� �����������
            builder.Services.AddControllers();
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // �������� ������������� CORS
            app.UseCors("AllowAllOrigins");
            // �������� �������������
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // ����������� �������� ��� ������������
            app.MapControllers();

            // ��������� ����������
            app.Run();
        }
        private static void RunExternalApp(string filePath, string arguments)
        {
            try
            {
                // ����������� ������� ��� ������� �������� ����������
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = filePath, // ��������� ���� � ngrok.exe
                    Arguments = arguments, // ��������� ��� ngrok
                    UseShellExecute = true, // ��������� ��������� ���������� � ��������� �������
                    CreateNoWindow = false // ������� ��������� ���� ��� ����������
                };
                // ��������� �������
                Process.Start(processStartInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to run external app: " + ex.Message);
            }
        }
    }
}



