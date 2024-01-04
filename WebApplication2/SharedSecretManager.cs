namespace MrezeBackend
{
    public class SharedSecretManager
    {
        private static readonly Dictionary<string, string> _clientSharedSecrets = new Dictionary<string, string>();

        public static void SetSharedSecret(string clientId, string sharedSecret)
        {
            _clientSharedSecrets[clientId] = sharedSecret;
        }

        public static string GetSharedSecret(string clientId)
        {
            if (_clientSharedSecrets.TryGetValue(clientId, out string sharedSecret))
            {
                return sharedSecret;
            }

            return null;
        }
    }


}
