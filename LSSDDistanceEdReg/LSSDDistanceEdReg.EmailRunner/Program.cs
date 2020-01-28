using LSSD.DistanceEdReg;
using LSSD.DistanceEdReg.Data;
using LSSD.DistanceEdReg.Util;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using System;
using System.Collections.Generic;
using System.Net.Mail;
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
                    using (DistanceEdRequestRepository requestRepository = new DistanceEdRequestRepository(dbConnectionString))
                    {
                        List<DistanceEdRequest> requestsNeedingNotification = requestRepository.GetRequestsRequiringNotification();

                        EmailHelper email = new EmailHelper(smtpConfig["hostname"], smtpConfig["port"].ToInt(), smtpConfig["username"], smtpConfig["password"], smtpConfig["fromaddress"], smtpConfig["replytoaddress"]);                        

                        if (requestsNeedingNotification.Count > 0)
                        {
                            ConsoleWrite("Found " + requestsNeedingNotification.Count + " requests needing notifications");
                            foreach(DistanceEdRequest r in requestsNeedingNotification )
                            {
                                ConsoleWrite("Enqueing notifcation for request ID " + r.ID);
                                email.NewMessage(r);
                            }                            
                        } else
                        {
                            ConsoleWrite("No new requests found");
                        }

                        Console.WriteLine("Sending...");
                        List<int> successfulIDs = email.SendAll();

                        Console.WriteLine("Marking " + successfulIDs.Count + " as successes");
                        requestRepository.MarkAsHelpDeskNotified(successfulIDs);
                    }

                    ConsoleWrite("Done!");
                    ConsoleWrite("Sleeping for " + sleepTimeMinutes + " minutes...");

                    // Sleep
                    Task.Delay(sleepTimeMinutes * 60 * 1000).Wait();
                }
            }
        }
    }
}
