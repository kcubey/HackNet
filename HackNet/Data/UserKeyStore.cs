using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public class UserKeyStore
	{
		public int UserId;

		public byte[] RsaPub;

		public byte[] RsaPriv;

		public byte[] aesIv;
	}
}