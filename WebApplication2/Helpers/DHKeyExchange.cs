using System;
using System.Security.Cryptography;

namespace MrezeBackend.Helpers
{
    public class DHKeyExchange
    {
        private ECDiffieHellmanCng serverECDH;

        public DHKeyExchange()
        {
            serverECDH = new ECDiffieHellmanCng();
            serverECDH.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            serverECDH.HashAlgorithm = CngAlgorithm.Sha256;
        }

        public string GetServerPublicKey()
        {
            return Convert.ToBase64String(serverECDH.PublicKey.ToByteArray());
        }

        public string GenerateSharedSecret(string clientPublicKey)
        {
            try
            {
                byte[] clientPublicKeyBytes = Convert.FromBase64String(clientPublicKey);
                ECDiffieHellmanPublicKey clientPublicKey1 = ECDiffieHellmanCngPublicKey.FromByteArray(clientPublicKeyBytes, CngKeyBlobFormat.EccPublicBlob);
                byte[] sharedSecret = serverECDH.DeriveKeyMaterial(clientPublicKey1);
                return Convert.ToBase64String(sharedSecret);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error occurred while converting public key: {ex.Message}");
                return null;
            }
        }
    }

}
