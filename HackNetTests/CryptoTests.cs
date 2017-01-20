using HackNet.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackNetTests
{
	[TestClass]
	public class CryptoTests
	{
		[TestMethod]
		public void CryptAes()
		{
			string before = "This is a very secret AES message";
			byte[] testKey = Crypt.Instance.GenerateAesKey();
			byte[] randomIv = Crypt.Instance.GenerateIv("AES");
			byte[] beforeEnc = Encoding.UTF8.GetBytes(before);
			byte[] encrypted = Crypt.Instance.EncryptAes(beforeEnc, testKey, randomIv);
			byte[] decrypted = Crypt.Instance.DecryptAes(encrypted, testKey, randomIv);
			string after = Encoding.UTF8.GetString(decrypted);
			Assert.IsTrue(before.Equals(after));
		}

		[TestMethod]
		public void CryptRsa()
		{
			string keypair = Crypt.Instance.GenerateRsaParameters();
			string pubonly = Crypt.Instance.RemovePrivateKey(keypair);

			string plainText = "Super secret string for encryption";

			byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
			byte[] cipherBytes = Crypt.Instance.EncryptRsa(plainBytes, pubonly);
			byte[] decipheredBytes = Crypt.Instance.DecryptRsa(cipherBytes, keypair);
			string decipheredText = Encoding.UTF8.GetString(decipheredBytes);

			Assert.IsTrue(plainText.Equals(decipheredText));
		}

		[TestMethod]
		public void CryptDeriveKey()
		{
			// Ensure no randomness in key derivation using the same parameters
			string pwd = "SuperSecretPassword@!";
			byte[] salt = Crypt.Instance.Generate(16); // Generate salt required
			byte[] iv = Crypt.Instance.Generate(8); // 64 bits for DES iv
			byte[] key1 = Crypt.Instance.DeriveKey(pwd, salt, iv);
			byte[] key2 = Crypt.Instance.DeriveKey(pwd, salt, iv);
			Assert.IsTrue(key1.SequenceEqual(key2));
		}
	}
}
