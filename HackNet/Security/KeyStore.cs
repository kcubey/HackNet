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

			} else {
				// Should only execute when user does not have a keystore
				string keyPair = Crypt.Instance.GenerateRsaParameters();
				byte[] aesIv = Crypt.Instance.Generate(16);

				byte[] aesKey = Crypt.Instance.DeriveKey(password, salt, aesIv);
				byte[] keyPairBytes = Encoding.UTF8.GetBytes(keyPair);

				this.rsaPublic = Crypt.Instance.RemovePrivateKey(keyPair);
				this.rsaPrivate = keyPair;
				this.aesKey = aesKey;
				this.aesIv = aesIv;
				this.TOTPSecret = null;
				this.rsaBytes = Crypt.Instance.EncryptAes(keyPairBytes, aesKey);
				this.UserId = uks.UserId;
			}
		}


	}
}