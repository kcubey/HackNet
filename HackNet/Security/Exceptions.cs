using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackNet.Security
{
	[Serializable]
    internal class AuthException : Exception
    {
        internal AuthException() : base() { }
        internal AuthException(string str) : base(str) { }
        internal AuthException(string str, Exception inner) : base(str, inner) { }
    }

	[Serializable]
	internal class RegistrationException : Exception
	{
		internal RegistrationException() : base() { }
		internal RegistrationException(string str) : base(str) { }
		internal RegistrationException(string str, Exception inner) : base(str, inner) { }
	}

	[Serializable]
	internal class UserException : Exception
    {
        internal UserException() : base() { }
        internal UserException(string str) : base(str) { }
        internal UserException(string str, Exception inner) : base(str, inner) { }
    }

	[Serializable]
	internal class InputException : Exception
    {
        internal InputException() : base() { }
        internal InputException(string str) : base(str) { }
        internal InputException(string str, Exception inner) : base(str, inner) { }
    }

	[Serializable]
	internal class ConnectionException : Exception
	{
		internal ConnectionException() : base() { }
		internal ConnectionException(string str) : base(str) { }
		internal ConnectionException(string str, Exception inner) : base(str, inner) { }
	}

	[Serializable]
	internal class MailException : Exception
	{
		internal MailException() : base() { }
		internal MailException(string str) : base(str) { }
		internal MailException(string str, Exception inner) : base(str, inner) { }
	}

	[Serializable]
	internal class KeyStoreException : Exception
	{
		internal KeyStoreException() : base() { }
		internal KeyStoreException(string str) : base(str) { }
		internal KeyStoreException(string str, Exception inner) : base(str, inner) { }
	}
}