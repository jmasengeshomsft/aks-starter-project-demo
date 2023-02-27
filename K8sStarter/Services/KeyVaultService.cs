
using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace K8sStarter.Services
{
    public class KeyVaultService : IKeyVaultService
    {
        public SecretClient KVClient {get;set;}
        public KeyVaultService(IConfiguration configuration)
        {
            string keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME");
            var kvUri = "https://" + keyVaultName + ".vault.azure.net";
            KVClient = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());          
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            var result = await KVClient.GetSecretAsync(secretName);
            return result.Value.Value;
        }

        public async Task<string> SetSecretAsync(string secretValue)
        {
            var result = await KVClient.SetSecretAsync("csiRotatingSecret", secretValue);

            return result.Value.Value;
        }

        public string CreateNewSecret(string secretName)
        {
            var newSecret = DateTime.Now.ToString();
            return newSecret;
        }
    }
}