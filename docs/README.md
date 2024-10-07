# Intro
Hashikeys is a middleware extension for .NET that simplifies integrating HashiCorp Vault with your .NET application. It allows you to automatically fetch secrets from Vault and load them as environment variables in your application with just a single line of code.

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
```bash
services.WireupHashi("http://localhost:8200", "myroot", "newkv/mysecrets");

```


# Middleware 

```bash
services.WireupHashi("http://truedeity.online:8200", "myroot", "newkv/mysecrets");
```