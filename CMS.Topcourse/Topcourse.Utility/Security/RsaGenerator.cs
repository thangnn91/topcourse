// Class create rsa key
// Create by : Dang Thanh.

using PaygateRSALib;
using System.Security.Cryptography;

namespace Topcourse.Utility
{
    public class RsaGenerator
    {
        const int NUM_BITS = 1024;
        public RsaGenerator()
        {
        }

        public string CreatePrivateKeyRsa()
        {
            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(NUM_BITS);
            //return rsa.ToXmlString(true);

            CspParameters _cpsParameter;
            RSACryptoServiceProvider RSAProvider;

            _cpsParameter = new CspParameters();
            _cpsParameter.Flags = CspProviderFlags.UseMachineKeyStore;
            RSAProvider = new RSACryptoServiceProvider(NUM_BITS, _cpsParameter);
            return RSAProvider.ToXmlString(true);
        }

        public string CreatePrivateKeyPem(string privateKeyRsa)
        {
            CspParameters _cpsParameter;
            RSACryptoServiceProvider rsa;

            _cpsParameter = new CspParameters();
            _cpsParameter.Flags = CspProviderFlags.UseMachineKeyStore;
            rsa = new RSACryptoServiceProvider(_cpsParameter);
            rsa.FromXmlString(privateKeyRsa);
            PaygateRSAHelper PaygateRSA = new PaygateRSAHelper();
            return PaygateRSA.CreatePrivateKeyPEM(rsa.ExportParameters(true));
        }


        public byte[] CreatePrivateKeyDer(string privateKeyRsa)
        {
            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();


            CspParameters _cpsParameter;
            RSACryptoServiceProvider rsa;

            _cpsParameter = new CspParameters();
            _cpsParameter.Flags = CspProviderFlags.UseMachineKeyStore;
            rsa = new RSACryptoServiceProvider(_cpsParameter);


            rsa.FromXmlString(privateKeyRsa);
            PaygateRSAHelper PaygateRSA = new PaygateRSAHelper();
            return PaygateRSA.CreatePrivateKeyDER(rsa.ExportParameters(true));
        }

        public string CreatePublicKeyRsa(string privateKeyRsa)
        {
            CspParameters _cpsParameter;
            RSACryptoServiceProvider rsa;

            _cpsParameter = new CspParameters();
            _cpsParameter.Flags = CspProviderFlags.UseMachineKeyStore;
            rsa = new RSACryptoServiceProvider(_cpsParameter);
            rsa.FromXmlString(privateKeyRsa);
            return rsa.ToXmlString(false);
        }

        public string CreatePublicKeyPem(string privateKeyRsa)
        {
            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //rsa.FromXmlString(privateKeyRsa);
            //PaygateRSAHelper PaygateRSA = new PaygateRSAHelper();
            //return PaygateRSA.CreatePrivateKeyPEM(rsa.ExportParameters(true));
            CspParameters _cpsParameter;
            RSACryptoServiceProvider rsa;

            _cpsParameter = new CspParameters();
            _cpsParameter.Flags = CspProviderFlags.UseMachineKeyStore;
            rsa = new RSACryptoServiceProvider(_cpsParameter);
            rsa.FromXmlString(privateKeyRsa);
            PaygateRSAHelper PaygateRSA = new PaygateRSAHelper();
            return PaygateRSA.CreatePublicKeyPEM(rsa.ExportParameters(false));
        }

        public byte[] CreatePublicKeyDer(string privateKeyRsa)
        {
            CspParameters _cpsParameter;
            RSACryptoServiceProvider rsa;

            _cpsParameter = new CspParameters();
            _cpsParameter.Flags = CspProviderFlags.UseMachineKeyStore;
            rsa = new RSACryptoServiceProvider(_cpsParameter);
            rsa.FromXmlString(privateKeyRsa);
            PaygateRSAHelper PaygateRSA = new PaygateRSAHelper();
            return PaygateRSA.CreatePublicKeyDER(rsa.ExportParameters(false));
        }
    }
}
