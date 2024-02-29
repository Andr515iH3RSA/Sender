using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Sender.Encrypter;
namespace Sender
{
	internal class Program
	{
		static string KeyContainerName = "RSAKey";
		static void Main(string[] args)
		{
			try
			{





				Console.WriteLine("Sender");
				//Create a UnicodeEncoder to convert between byte array and string.
				UnicodeEncoding byteConverter = new UnicodeEncoding();

				Console.WriteLine("Write your message:\n");
				string message = "test";
				//Create byte arrays to hold original, encrypted, and decrypted data.
				byte[] dataToEncrypt = byteConverter.GetBytes(message);
				byte[] encryptedData;

				//Create a new instance of RSACryptoServiceProvider to generate
				//public and private key data.
				using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024, new CspParameters() { KeyContainerName = KeyContainerName }))
				{

					//export the public key to export the modulus and exponent.
					RSAParameters publicKey = rsa.ExportParameters(false);
					Console.WriteLine("reciever's modulus: {0}", BitConverter.ToString(publicKey.Modulus));
					Console.WriteLine("recierver's exponent: {0}", BitConverter.ToString(publicKey.Exponent));



					//Pass the data to ENCRYPT, the public key information 
					//(using RSACryptoServiceProvider.ExportParameters(false),
					//and a boolean flag specifying no OAEP padding.
					encryptedData = RSAEncrypter.RSAEncrypt(dataToEncrypt, rsa.ExportParameters(true), false);



					//Display the decrypted plaintext and the encrypted bytes to the console. 
					Console.WriteLine("Plaintext: {0}", message);
					Console.WriteLine("Encrypted bytes: \n{0}", BitConverter.ToString(encryptedData));
				}
			}
			catch (ArgumentNullException)
			{
				//Catch this exception in case the encryption did
				//not succeed.
				Console.WriteLine("Encryption failed.");
			}
			Console.Read();
		}
	}
}
