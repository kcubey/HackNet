using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HackNet.Security
{
	public class KeyStore
	{
		internal byte[] aesKey { get; set; }

		internal byte[] aesIv { get; set; }

		internal string rsaPrivate { get; set; }

		internal string rsaPublic { get; set; }

		internal string TOTPSecret { get; set; }

		private byte[] rsaBytes;

		private int UserId;

		internal KeyStore(UserKeyStore uks, string password, byte[] salt) {
			// Should only execute when user has a keystore
			if (uks is UserKeyStore)
			{
				byte[] aesIv = uks.AesIv;
				byte[] aesKey = Crypt.Instance.DeriveKey(password, salt, aesIv);
				byte[] rsaBytes = Crypt.Instance.DecryptAes(uks.RsaPriv, aesKey);
				string rsaPrivate = Encoding.UTF8.GetString(rsaBytes);

				this.aesIv = uks.AesIv;
				this.aesKey = aesKey;
				this.rsaPrivate = rsaPrivate;
				this.rsaPublic = uks.RsaPub;
				this.TOTPSecret = uks.TOTPSecret;
				this.rsaBytes = uks.RsaPriv;
				this.UserId = uks.UserId;

			} else
			{
				throw new KeyStoreException("KeyStore is in invalid state");
			}
		}

		internal static UserKeyStore DefaultDbKeyStore(string password, byte[] salt, int userId)
		{
			UserKeyStore uks;

			byte[] iv = Crypt.Instance.Generate(8);
			byte[] aesKey = Crypt.Instance.DeriveKey(password, salt, iv);
			string rsaString = Crypt.Instance.GenerateRsaParameters();
			byte[] rsaStringBytes = Encoding.UTF8.GetBytes(rsaString);
			byte[] rsaEncrypted = Crypt.Instance.EncryptAes(rsaStringBytes, aesKey);
			string rsaPublic = Crypt.Instance.RemovePrivateKey(rsaString);

			uks = new UserKeyStore()
			{
				AesIv = iv,
				TOTPSecret = null,
				RsaPub = rsaPublic,
				RsaPriv = rsaEncrypted,
				UserId = userId
			};

			return uks;
		}

	}
}