using System.Security.Cryptography;
using System.Text;

namespace DataProtectionOnServer.Security
{
    public class DataProtector
    {
        /*
         * Encode: Kod formatını değiştir (64 bit string))
         * Encryption: Şifrele
         * 
         * Verilen adreste bir dosya oluşturan
         * ve değeri, şifreleyerek bu dosyaya yazan 
         * bir nesne yapıyoruz
         * 
         */

        private readonly string path;
        private readonly byte[] entropy;

        public DataProtector(string path)
        {
            this.path = path;
            entropy = RandomNumberGenerator.GetBytes(16);
        }

        public int EncryptData(string value)
        {           
            var encodedData = Encoding.UTF8.GetBytes(value);
            using var fileStream = new FileStream(path, FileMode.OpenOrCreate);
            var length = encryptDataToFile(encodedData, entropy, DataProtectionScope.CurrentUser, fileStream);
            return length;
        }

        private int encryptDataToFile(byte[] value, byte[] entropy, DataProtectionScope protectionScope, FileStream fileStream)
        {
          var encrypted =  ProtectedData.Protect(value, entropy, protectionScope);

            if (fileStream.CanWrite && encrypted != null)
            {
                fileStream.Write(encrypted, 0, encrypted.Length);
            }
            
         return encrypted.Length;

        }

        public string DecryptData(int length)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            byte[] decrypted = decryptDataFromFile(length,entropy, DataProtectionScope.CurrentUser, fileStream);

            return Encoding.UTF8.GetString(decrypted);
        }

        private byte[] decryptDataFromFile(int length, byte[] entropy, DataProtectionScope scope, FileStream fileStream )        {
            var input = new byte[length];
            var output = new byte[length];          

            if (fileStream.CanRead)
            {
                fileStream.Read(input, 0, input.Length);
                output = ProtectedData.Unprotect(input, entropy, scope);
            }
            return output;
        } 

    }
}
