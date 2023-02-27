
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;

namespace K8sStarter.Services
{
    public interface IKeyVaultService
    {
        SecretClient KVClient {get;set;}
        Task<string> SetSecretAsync(string secretValue);
        Task<string> GetSecretAsync(string secretName);
        string CreateNewSecret(string secretName);
    }
}