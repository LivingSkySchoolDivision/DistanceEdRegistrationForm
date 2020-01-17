using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using System;
using System.Threading.Tasks;

namespace LSSDDistanceEdReg.EmailRunner
{
    class Program
    {
        private const int sleepTimeMinutes = 15;

        private static void ConsoleWrite(string message)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm K") + ": " + message);
        }

        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                   .AddJsonFile($"appsettings.json", true, true)
                   .AddEnvironmentVariables()
                   .Build();

            string keyvault_endpoint = configuration["KEYVAULT_ENDPOINT"];
            if (!string.IsNullOrEmpty(keyvault_endpoint))
            {
                ConsoleWrite("Loading configuration from Azure Key Vault: " + keyvault_endpoint);
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var keyVaultClient = new KeyVaultClient(
                                new KeyVaultClient.AuthenticationCallback(
                                    azureServiceTokenProvider.KeyVaultTokenCallback));

                configuration = new ConfigurationBuilder()
                    .AddConfiguration(configuration)
                    .AddAzureKeyVault(keyvault_endpoint, keyVaultClient, new DefaultKeyVaultSecretManager())
                    .Build();
            }

            IConfigurationSection smtpConfig = configuration.GetSection("SMTP");
            string dbConnectionString = configuration.GetConnectionString(RunnerSettings.ConnectionStringName) ?? string.Empty;

            if (string.IsNullOrEmpty(dbConnectionString))
            {
                ConsoleWrite("ConnectionString can't be empty");
            }
            else
            {
                ConsoleWrite("EmailRunner starting main loop...");
                while (true)
                {


                    ConsoleWrite("Done!");
                    ConsoleWrite("Sleeping for " + sleepTimeMinutes + " minutes...");

                    // Sleep
                    Task.Delay(sleepTimeMinutes * 60 * 1000).Wait();
                }
            }
        }
    }
}
