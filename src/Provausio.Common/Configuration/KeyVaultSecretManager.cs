﻿using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace Provausio.Common.Configuration
{
    public class KeyVaultSecretManager : IKeyVaultSecretManager
    {
        private const string SharedPrefix = "Shared";
        private readonly string _servicePrefix;

        public KeyVaultSecretManager(string prefix)
        {
            _servicePrefix = prefix;
        }

        public bool Load(SecretItem secret)
        {
            return secret.Identifier.Name.StartsWith(_servicePrefix) ||
                   secret.Identifier.Name.StartsWith(SharedPrefix);
        }

        // Remove the prefix from the secret name and replace two 
        // dashes in any name with the KeyDelimiter, which is the 
        // delimiter used in configuration (usually a colon). Azure 
        // Key Vault doesn't allow a colon in secret names.
        public string GetKey(SecretBundle secret)
        {
            var secretName = secret.SecretIdentifier.Name;
            var key = string.Empty;

            if (secretName.StartsWith(_servicePrefix))
                key = GetConfigurationKey(_servicePrefix, secretName);

            if (secretName.StartsWith(SharedPrefix))
                key = GetConfigurationKey(SharedPrefix, secretName);

            return key;
        }

        private static string GetConfigurationKey(string prefix, string secretName)
        {
            return secretName.Substring(prefix.Length + 1).Replace("--", ConfigurationPath.KeyDelimiter);
        }
    }
}