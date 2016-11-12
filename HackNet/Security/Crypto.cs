using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HackNet.Security
{
	internal class AesProvider
	{
		private byte[] _globalKey;
		private byte[] _initVector;

		internal AesProvider(string initVector = null)
		{
			_globalKey = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["AesUsersCryptKey"]);
			if (initVector != null)
				_initVector = Encoding.UTF8.GetBytes(initVector);
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
				using (var encryptor = aes.CreateEncryptor(_globalKey, _initVector))
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
				using (var decryptor = aes.CreateDecryptor(_globalKey, _initVector))
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



		//
		// Utilities for encryption and decryption
		//
		public static string GenerateEntropy(int length)
		{
			const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
			StringBuilder res = new StringBuilder();
			using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
			{
				byte[] uintBuffer = new byte[sizeof(uint)];
				while (length-- > 0)
				{
					rng.GetBytes(uintBuffer);
					uint num = BitConverter.ToUInt32(uintBuffer, 0);
					res.Append(valid[(int)(num % (uint)valid.Length)]);
				}
			}
			return res.ToString();
		}

		#region Implementation of interfaces
		~AesProvider()
		{
			Dispose();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			_globalKey = null;
		}

		public bool Equals(AesProvider uc)
		{
			if (_initVector.Equals(uc._initVector))
				return true;
			else
				return false;
		}

		#endregion
	}
}