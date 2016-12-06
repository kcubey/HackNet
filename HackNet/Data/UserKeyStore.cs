using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public class UserKeyStore
	{
		[Key]
		[ForeignKey("User")]
		public int UserId { get; set; }

		public byte[] RsaPub { get; set; }

		public byte[] RsaPriv { get; set; }

		public byte[] aesIv { get; set; }

		// Foreign key reference
		public virtual Users User { get; set; }
	}
}