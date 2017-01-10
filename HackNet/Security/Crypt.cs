using System;
using System.Diagnostics;
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
					cipherBytes = rsa.Encrypt(plainBytes, false);
				}
			return cipherBytes;
		}
		
		// Decrypt using a known private key
		public byte[] DecryptRsa(byte[] cipherBytes, string parameters)
		{
			byte[] plainBytes = new byte[0];
			if (cipherBytes.Length > 0)
				using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
				{
					rsa.FromXmlString(parameters);
					plainBytes = rsa.Decrypt(cipherBytes, false);
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

		// Derive AES key from plaintext
		public byte[] DeriveKey(string passwd, byte[] salt, byte[] initvector)
		{
			byte[] derivedKey;

			if (passwd == null || salt == null || initvector == null)
				throw new ArgumentNullException("Key derivation failed due to null argument");

			using (var kdf = new Rfc2898DeriveBytes(passwd, salt))
				derivedKey = kdf.CryptDeriveKey("AES", "SHA1", 128, initvector);

			return derivedKey;
		}


		public byte[] EncryptAes(byte[] plainBytes, byte[] keyBytes)
		{
			byte[] cipherBytes = new byte[0];
			try
			{
				using (AesManaged aes = new AesManaged())
				{
					aes.BlockSize = 128;
					aes.Mode = CipherMode.CBC;
					aes.Padding = PaddingMode.PKCS7;
					aes.Key = keyBytes;
					ICryptoTransform transform = aes.CreateEncryptor();
					cipherBytes = transform.TransformFinalBlock(plainBytes, 0, cipherBytes.Length);
				}
			} catch (Exception e)
			{
				Debug.WriteLine("Exception caught: " + e.StackTrace);
			}
			return cipherBytes;
		}

		public byte[] DecryptAes(byte[] cipherBytes, byte[] keyBytes)
		{
			byte[] plainBytes = new byte[0];
			try
			{
				using (AesManaged aes = new AesManaged())
				{
					aes.BlockSize = 128;
					aes.Mode = CipherMode.CBC;
					aes.Padding = PaddingMode.PKCS7;
					aes.Key = keyBytes;
					ICryptoTransform transform = aes.CreateDecryptor();
					plainBytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
				}
			} catch (Exception e)
			{
				Debug.WriteLine("Exception caught: " + e.StackTrace);
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
			Debug.WriteLine("TIME ELAPSED" + sw.Elapsed);
			// Return the hash
			return hashedbytes;
		}

		public byte[] SHA512Hash(string plaintext, byte[] salt = null)
		{
			// Obtain base variables
			byte[] ptBytes = Encoding.UTF8.GetBytes(plaintext);
			byte[] combinedBytes;
			byte[] newHash;

			// If salt is present, append it to plaintext
			if (salt == null)
			{
				combinedBytes = new byte[ptBytes.Length];
				ptBytes.CopyTo(combinedBytes, 0);
			}
			else
			{
				combinedBytes = new byte[ptBytes.Length + salt.Length];
				ptBytes.CopyTo(combinedBytes, 0);
				salt.CopyTo(combinedBytes, ptBytes.Length);
			}

			// Do the hashing
			using (SHA512 shaCalc = new SHA512Managed())
			{
				newHash = shaCalc.ComputeHash(combinedBytes);
			}

			// Return the hash
			return newHash;

		}

		// Generate bytes from RNGCryptoServiceProvider
		internal byte[] Generate(int size)
		{
			if (size == 0) // Guard clause
				return null;
			byte[] random = new byte[size];
			using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
				rng.GetBytes(random);
			return random;
		}


	}
}