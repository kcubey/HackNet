using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public class Confirmations
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ConfirmationId { get; set; }

		public int UserId { get; set; }

		public string Email { get; set; }

		public string Code { get; set; }

		public DateTime Expiry { get; set; }

		public ConfirmType Type { get; set; }
	}

	public enum ConfirmType
	{
		EmailConfirm = 0,
		PasswordReset = 1
	}
}