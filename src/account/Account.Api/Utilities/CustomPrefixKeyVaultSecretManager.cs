﻿using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;

namespace Account.Api.Utilities
{
    public class CustomPrefixKeyVaultSecretManager : KeyVaultSecretManager
    {
        const string appName = "Account";
        public override string GetKey(KeyVaultSecret secret)
        {
            return secret.Name.StartsWith(appName) ? secret.Name.Replace(appName + "-", "").Replace("-", ConfigurationPath.KeyDelimiter) : secret.Name.Replace("-", ConfigurationPath.KeyDelimiter);
        }
    }
}
