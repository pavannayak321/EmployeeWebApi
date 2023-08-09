namespace EmployeeWebApi.AzureKeyVaultUtility
{
    public interface IKeyVaultManager
    {
        public  Task<string> GetSecret(string secretName);
    }
}
