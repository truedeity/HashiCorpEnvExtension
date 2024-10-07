using System;
using Microsoft.Extensions.DependencyInjection;  
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;

public static class HashiCorpEnvMiddleware
{
    public static IServiceCollection WireupHashi(this IServiceCollection services, string vaultUrl, string token = "myroot", string secretsPath = "kv/mysecrets")
    {
        // Set up Vault client
        var authMethod = new TokenAuthMethodInfo(token);
        var vaultClientSettings = new VaultClientSettings(vaultUrl, authMethod);
        IVaultClient vaultClient = new VaultClient(vaultClientSettings);

        // Read secrets from Vault and set them as environment variables
        var secret = vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(secretsPath).Result;

        foreach (var keyValue in secret.Data.Data)
        {
            Environment.SetEnvironmentVariable(keyValue.Key, keyValue.Value.ToString());
        }

        return services;
    }
}
