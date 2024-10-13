# Intro
HashiCorpEnvExtension is a middleware extension for .NET that simplifies integrating HashiCorp Vault with your .NET application. It allows you to automatically fetch secrets from Vault and load them as environment variables in your application with just a single line of code.

# Install
```bash
Install-Package HashiCorpEnvExtension -Version 1.0.1
```
```bash
dotnet add package HashiCorpEnvExtension --version 1.0.1
```


# Features
Fetches secrets from HashiCorp Vault and sets them as environment variables in your .NET app.
Easy one-line integration into any .NET project.
Securely pulls environment variables from Vault for flexible secret management.
Prerequisites
Before using Hashikeys, ensure the following:

You have a HashiCorp Vault server running and accessible.
You have installed the necessary .NET SDK (6.0 or later).
You have stored secrets in your Vault at the desired path.


# Enable KV v2 secrets engine:

```bash
vault secrets enable -path=newkv kv-v2
```

```bash
vault kv put newkv/mysecrets accessKey="new-access-key" secretKey="new-secret-key"
```

```bash
vault kv get newkv/mysecrets
```



# Middleware 

```bash
services.WireupHashi("http://localhost:8200", "myroot", "mysecrets", "newkv");
```


# Test

```bash
using Microsoft.Extensions.DependencyInjection;
using System;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace TestHashiCorpEnv
{
    static class Program
    {
        static void Main(string[] args)
        {
            string token = Environment.GetEnvironmentVariable("VAULT_TOKEN") ?? 
                throw new ArgumentNullException("Required Vault Token");

            var services = new ServiceCollection();

            // Configure services with HashiCorp Vault
	        services.WireupHashi("http://localhost:8200", token, "mysecrets", "newkv");

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Test to see if the environment variables are set
            Console.WriteLine("Environment Variables from HashiCorp Vault:");
            Console.WriteLine($"ACCESS_KEY: {Environment.GetEnvironmentVariable("accessKey")}");
            Console.WriteLine($"SECRET_KEY: {Environment.GetEnvironmentVariable("secretKey")}");
        }
    }
}

```

# Setting up a Development Mode HasiCrop Vault Server

```bash
docker pull hashicorp/vault
docker run --cap-add=IPC_LOCK -d --name=dev-vault hashicorp/vault

```
This runs a completely in-memory Vault server, which is useful for development but should not be used in production.

When running in development mode, two additional options can be set via environment variables:

- VAULT_DEV_ROOT_TOKEN_ID: This sets the ID of the initial generated root token to the given value
- VAULT_DEV_LISTEN_ADDRESS: This sets the IP:port of the development server listener (defaults to 0.0.0.0:8200)
As an example:
```bash
docker run --cap-add=IPC_LOCK -e 'VAULT_DEV_ROOT_TOKEN_ID=myroot' -e 'VAULT_DEV_LISTEN_ADDRESS=0.0.0.0:8200' hashicorp/vault
```

