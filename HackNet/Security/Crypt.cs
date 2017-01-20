using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HackNet.Security
{
	internal class Crypt
	{

		private static Crypt inst = null;
		private Crypt() {
			// Singleton class pattern constructor
		}
	
		public static Crypt Instance
		{
			get
			{
				if (inst == null)
					inst = new Crypt();
				return inst;
			}
		}

		// Encrypt using a known public key
		public byte[] EncryptRsa(byte[] plainBytes, string parameters)
		{
			byte[] cipherBytes = new byte[0];
			if (plainBytes.Length > 0)
				using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
				{
					rsa.FromXmlString(parameters);
					cipherBytes = rsa.Encrypt(plainBytes, true);
				}
			return cipherBytes;
		}
		
		// Decrypt using a known private key
		public byte[] DecryptRsa(byte[] cipherBytes, string parameters)
		{
			byte[] plainBytes = new byte[0];
			RSAParameters rparam = new RSAParameters();
			if (cipherBytes.Length > 0)
				using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
				{
					rsa.FromXmlString(parameters);
					rparam = rsa.ExportParameters(true);
					plainBytes = rsa.Decrypt(cipherBytes, true);
				}
			using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
				{
					RSACryptoServiceProvider.UseMachineKeyStore = true;
					rsa.ImportParameters(rparam);
					plainBytes = rsa.Decrypt(cipherBytes, true);
				}
			return plainBytes;
		}

		// Generate RSA Keys
		public string GenerateRsaParameters()
		{
			string parameters;

			using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
				parameters = rsa.ToXmlString(true);

			return parameters;
		}

		// Get public key parameter from a key pair
		public string RemovePrivateKey(string keypair)
		{
			string publicXml;
			using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
			{
				rsa.FromXmlString(keypair);
				publicXml = rsa.ToXmlString(false);
			}
			return publicXml;
		}

		// Derive AES key from plaintext (64 bytes, 512 bits)
		public byte[] DeriveKey(string passwd, byte[] salt, byte[] initvector)
		{
			byte[] derivedKey;

			if (passwd == null || salt == null || initvector == null)
				throw new ArgumentNullException("Key derivation failed due to null argument");

			using (var kdf = new Rfc2898DeriveBytes(passwd, salt, 1000))
				derivedKey = kdf.CryptDeriveKey("3DES", "SHA1", 192, initvector);

			Debug.WriteLine("Derived Key Length: " + derivedKey.Length);

			return derivedKey;
		}


		public byte[] EncryptAes(byte[] plainBytes, byte[] keyBytes, byte[] ivBytes)
		{
			byte[] cipherBytes = new byte[0];
			using (AesManaged aes = new AesManaged())
			{
				aes.BlockSize = 128;
				aes.Mode = CipherMode.CBC;
				aes.Padding = PaddingMode.PKCS7;
				aes.Key = keyBytes;
				aes.IV = ivBytes;

				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(plainBytes, 0, plainBytes.Length);
					}
					cipherBytes = ms.ToArray();
				}
			}

			return cipherBytes;
		}

		public byte[] DecryptAes(byte[] cipherBytes, byte[] keyBytes, byte[] ivBytes)
		{
			byte[] plainBytes = new byte[0];
			using (AesManaged aes = new AesManaged())
			{
				aes.BlockSize = 128;
				aes.Mode = CipherMode.CBC;
				aes.Padding = PaddingMode.PKCS7;
				aes.Key = keyBytes;
				aes.IV = ivBytes;

				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(cipherBytes, 0, cipherBytes.Length);
					}
					plainBytes = ms.ToArray();
				}
			}
			return plainBytes;
		}

		/*
		 * HACKNET HASH FUNCTIONS
		 * RFC2898 PBKDF2 AND SHA512
		 */

		internal byte[] Hash(byte[] password, byte[] salt = null)
		{
			// Start the stopwatch
			Stopwatch sw = new Stopwatch();
			sw.Start();
			byte[] hashedbytes;
			// If no salt specified, use default salt value
			if (salt == null)
				salt = Convert.FromBase64String("DefaultSalt=");
			// RFC2898 Implements HMAC Based SHA1, which is FIPS Compliant
			using (var kdf = new Rfc2898DeriveBytes(password, salt, 5000))
				hashedbytes = kdf.GetBytes(128);
			// Stop the stopwatch
			sw.Stop();
			Debug.WriteLine("Hashing time elapsed: " + sw.ElapsedMilliseconds + "ms");
			// Return the hash
			return hashedbytes;
		}

		public byte[] Hash(string plaintext, string algorithm = "SHA1")
		{
			// Obtain base variables
			byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
			byte[] hashBytes;

			// Do the hashing
			using (HashAlgorithm calc = HashAlgorithm.Create(algorithm))
			{
				if (calc is HashAlgorithm)
					hashBytes = calc.ComputeHash(plainBytes);
				else
					hashBytes = new SHA1Managed().ComputeHash(plainBytes);
			}

			// Return the hash
			return hashBytes;
		}

		// Generate bytes from RNGCryptoServiceProvider
		internal byte[] Generate(int bytes)
		{
			if (bytes < 1) // Guard clause
				return null;
			byte[] random = new byte[bytes];
			using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
				rng.GetBytes(random);
			return random;
		}

		internal byte[] GenerateIv(string alg)
		{
			switch (alg.ToUpper()) {
				case "DES":
					return Generate(8); // 64 bits
				case "AES":
				default:
					return Generate(16); // 128 bits
			}
		}

		internal byte[] GenerateAesKey()
		{
			return Generate(24); // 256 bits
		}

		internal byte[] GenerateSalt()
		{
			return Generate(64); // 512 bits
		}
	}
}