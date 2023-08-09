using Azure.Security.KeyVault.Secrets;

namespace EmployeeWebApi.AzureKeyVaultUtility
{
    public class KeyVaultManager : IKeyVaultManager
    {
        private readonly SecretClient _secretClient;
        public KeyVaultManager(SecretClient secretClient)
        {
           _secretClient= secretClient;
        }
        public  async Task<string> GetSecret(string secretName)
        {
            try
            {
                KeyVaultSecret keyVaultSecret = await _secretClient.GetSecretAsync(secretName);
                return keyVaultSecret.Value;
            }
            catch
            {
                throw;
            }
        }
    }
}
