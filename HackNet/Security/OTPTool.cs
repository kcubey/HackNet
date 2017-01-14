using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace HackNet.Security
{
	public class OTPTool : IDisposable
	{
		private bool disposed = false;
		private string _identity;
		private byte[] _secret = new byte[14];
		private byte[] _hmac;
		private int _offset;

		public OTPTool()
		{
			SecretBase32 = RandomiseSecret();
		}

		public OTPTool(string base32sec)
		{
			SecretBase32 = base32sec;
		}

		public OtpResult Validate(string input)
		{
			List<int> range = OTPRange;
			int intput = 0;
			if (input.Length != 6)
				return OtpResult.WrongLength;
			if (!int.TryParse(input, out intput))
				return OtpResult.NotInt;
			if (range.Contains(intput))
				return OtpResult.Success;
			else
				return OtpResult.WrongOtp;
		}


		internal string Identity
		{
			get
			{
				return "HackNet";
			}
			set
			{
				_identity = value;
			}
		}


		internal string SecretBase32
		{
			get
			{
				return Base32.ToString(Secret).Substring(0, 16);
			}
			set
			{
				try
				{
					Secret = Base32.ToBytes(value);
				} catch { };
			}
		}


		private byte[] Secret
		{
			get { return _secret; }
			set
			{
				if (_secret == null)
				{
					_secret = new byte[14];
				}
				_secret = value;
			}
		}


		private byte[] Hmac
		{
			get { return _hmac; }
			set { _hmac = value; }
		}


		private byte[] HmacPart1
		{
			get { return _hmac.Take(Offset).ToArray(); }
		}

		private byte[] HmacPart2
		{
			get { return _hmac.Skip(Offset).Take(4).ToArray(); }
		}

		private byte[] HmacPart3
		{
			get { return _hmac.Skip(Offset + 4).ToArray(); }
		}


		private int Offset
		{
			get { return _offset; }
			set { _offset = value; }
		}


		internal string RandomiseSecret()
		{
			using (RNGCryptoServiceProvider cryptrng = new RNGCryptoServiceProvider())
			{
				cryptrng.GetBytes(Secret);
			}
			return SecretBase32;
		}

		// Two intervals before and after
		private List<int> OTPRange
		{
			get
			{
				List<int> range = new List<int>();
				long timestamp = Convert.ToInt64(GetUnixTimestamp() / 30) - 2;
				for (int i = 0; i < 5; i++)
				{
					var data = BitConverter.GetBytes(timestamp + i).Reverse().ToArray();
					Hmac = new HMACSHA1(Secret).ComputeHash(data);
					Offset = Hmac.Last() & 0x0F;
					range.Add((
						((Hmac[Offset + 0] & 0x7f) << 24) |
						((Hmac[Offset + 1] & 0xff) << 16) |
						((Hmac[Offset + 2] & 0xff) << 8) |
						(Hmac[Offset + 3] & 0xff)
						) % 1000000);
				}
				return range;
			}
		}

		internal int OTPNow
		{
			get
			{
				// https://tools.ietf.org/html/rfc4226
				long timestamp = Convert.ToInt64(GetUnixTimestamp() / 30);
				var data = BitConverter.GetBytes(timestamp).Reverse().ToArray();
				Hmac = new HMACSHA1(Secret).ComputeHash(data);
				Offset = Hmac.Last() & 0x0F;
				int otpnow = (
					((Hmac[Offset + 0] & 0x7f) << 24) |
					((Hmac[Offset + 1] & 0xff) << 16) |
					((Hmac[Offset + 2] & 0xff) << 8) |
					(Hmac[Offset + 3] & 0xff)
					) % 1000000;

				return otpnow;
			}
		}

		private static long GetUnixTimestamp()
		{
			return Convert.ToInt64(Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds));
		}

		public static string QrCodeUrl(string b32sec)
		{
			return string.Format("https://chart.googleapis.com/chart?chs=500x500&chld=M|0&cht=qr&chl=otpauth://totp/HackNet?secret={0}",
								b32sec);
		}

		#region IDisposable Implementation
		~OTPTool()
		{
			Dispose();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{

			if (disposed)
				throw new ObjectDisposedException("OTPTool");

			if (disposing)
			{
				if (_secret != null)
					Array.Clear(_secret, 0, _secret.Length);
				if (_hmac != null)
					Array.Clear(_hmac, 0, _hmac.Length);
			}

			disposed = true;
		}

		#endregion
	}
}