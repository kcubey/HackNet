using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

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


		public byte[] Encrypt(string plainText)
		{
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			using (AesManaged aes = new AesManaged())
			{
				aes.BlockSize = 128;
				aes.KeySize = 256;
				aes.Mode = CipherMode.CBC;
				aes.Padding = PaddingMode.PKCS7;
				using (var encryptor = aes.CreateEncryptor())
				using (var memoryStream = new MemoryStream())
				using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
				{
					cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
					cryptoStream.FlushFinalBlock();
					// Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
					var cipherBytes = memoryStream.ToArray();
					memoryStream.Close();
					cryptoStream.Close();
					return cipherBytes;
				}
			}
		}

		public string Decrypt(byte[] cipherBytes)
		{
			using (AesManaged aes = new AesManaged())
			{
				aes.BlockSize = 128;
				aes.Mode = CipherMode.CBC;
				aes.Padding = PaddingMode.PKCS7;
				using (var decryptor = aes.CreateDecryptor())
				using (var memoryStream = new MemoryStream(cipherBytes))
				using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
				{
					var plainTextBytes = new byte[cipherBytes.Length];
					var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
					memoryStream.Close();
					cryptoStream.Close();
					return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
				}
			}
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