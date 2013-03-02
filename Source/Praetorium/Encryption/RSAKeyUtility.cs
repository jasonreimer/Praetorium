using System.Security.Cryptography;

namespace Praetorium.Encryption
{
    /// <summary>
    /// Contains utility functions for working with the <see cref="RSACryptoServiceProvider"/>.
    /// </summary>
    public static class RSAKeyUtility
    {

        /// <summary>
        /// Creates a new RSA key and returns the key information in Xml format.
        /// </summary>
        /// <param name="keySize">Size of the key in bits.</param>
        /// <returns>
        /// Returns an Xml string containing the RSA key information.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The key that is created by this function is not saved to a key container.
        /// </para>
        /// </remarks>
        public static string CreateRSAKey(int keySize)
        {
            var parameters = CreateParameters(null, false);
            string keyXml = null;

            using (var rsaProvider = new RSACryptoServiceProvider(keySize, parameters))
            {
                rsaProvider.PersistKeyInCsp = false;
                keyXml = rsaProvider.ToXmlString(true);
                rsaProvider.Clear();
            }

            return keyXml;
        }

        /// <summary>
        /// Creates a new RSA key and returns the <see cref="RSACryptoServiceProvider"/> instance that is managing the key.
        /// </summary>
        /// <param name="keySize">Size of the key in bits.</param>
        /// <param name="keyContainerName">Name of the key container.</param>
        /// <param name="useMachineKeyStore">
        /// if set to <c>true</c>, the machine key store is used; otherwise, the user key store is used.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="RSACryptoServiceProvider"/> class.
        /// </returns>
        public static RSACryptoServiceProvider CreateRSAKey(int keySize, string keyContainerName, bool useMachineKeyStore)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(() => keyContainerName);

            var parameters = CreateParameters(keyContainerName, useMachineKeyStore);
            var rsaProvider = new RSACryptoServiceProvider(keySize, parameters);

            return rsaProvider;
        }

        /// <summary>
        /// Deletes a key container.
        /// </summary>
        /// <param name="keyContainerName">Name of the key container.</param>
        /// <param name="useMachineKeyStore">
        /// if set to <c>true</c>, the machine key store is used; otherwise, the user key store is used.
        /// </param>
        public static void DeleteKeyContainer(string keyContainerName, bool useMachineKeyStore)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(() => keyContainerName);

            var parameters = CreateParameters(keyContainerName, useMachineKeyStore);

            using (var rsaProvider = new RSACryptoServiceProvider(parameters))
            {
                rsaProvider.PersistKeyInCsp = false;
                rsaProvider.Clear();
            }
        }

        /// <summary>
        /// Exports a RSA key to an Xml string.
        /// </summary>
        /// <param name="keyContainerName">Name of the key container.</param>
        /// <param name="useMachineKeyStore">
        /// if set to <c>true</c>, the machine key store is used; otherwise, the user key store is used.
        /// </param>
        /// <param name="includePrivateParameters">
        /// <c>true</c> to export both public and private RSA keys; <c>false</c> to export only the public RSA key.
        /// </param>
        /// <returns>
        /// Returns an Xml string containing the RSA key information.
        /// </returns>
        /// <remarks>
        /// <para>
        /// If the specified <paramref name="keyContainerName"/> does not exist, it will be created by this method, and 
        /// a new key will be generated.
        /// </para>
        /// </remarks>
        public static string ExportRSAKeyToXml(string keyContainerName, bool useMachineKeyStore, bool includePrivateParameters)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(() => keyContainerName);

            string keyXml = null;
            var parameters = CreateParameters(keyContainerName, useMachineKeyStore);

            using (var rsaProvider = new RSACryptoServiceProvider(parameters))
                keyXml = rsaProvider.ToXmlString(includePrivateParameters);

            return Ensure.ReturnIsNotNull(keyXml);
        }

        /// <summary>
        /// Imports a RSA key into a key container.
        /// </summary>
        /// <param name="keyXml">
        /// An Xml string containing the RSA key information with both the public and private RSA keys.
        /// </param>
        /// <param name="keyContainerName">Name of the key container.</param>
        /// <param name="useMachineKeyStore">
        /// if set to <c>true</c>, the machine key store is used; otherwise, the user key store is used.
        /// </param>
        public static void ImportRSAKey(string keyXml, string keyContainerName, bool useMachineKeyStore)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(() => keyXml);
            Ensure.ArgumentNotNullOrWhiteSpace(() => keyContainerName);

            var parameters = CreateParameters(keyContainerName, useMachineKeyStore);

            using (RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(parameters))
            {
                rsaProvider.PersistKeyInCsp = true;
                rsaProvider.FromXmlString(keyXml);
            }
        }

        /// <summary>
        /// Creates the CSP parameters.
        /// </summary>
        /// <param name="keyContainerName">Name of the key container.</param>
        /// <param name="useMachineKeyStore">
        /// if set to <c>true</c>, the machine key store is used; otherwise, the user key store is used.
        /// </param>
        /// <returns>
        /// A new CspParameters instance.
        /// </returns>
        private static CspParameters CreateParameters(string keyContainerName, bool useMachineKeyStore)
        {
            var parameters = new CspParameters
            {
                KeyNumber = (int)KeyNumber.Exchange,
                Flags = CspProviderFlags.NoPrompt
            };

            if (useMachineKeyStore)
                parameters.Flags = parameters.Flags | CspProviderFlags.UseMachineKeyStore;

            if (keyContainerName != null)
                parameters.KeyContainerName = keyContainerName;

            return parameters;
        }
    }
}
