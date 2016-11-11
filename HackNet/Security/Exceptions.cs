using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackNet.Security
{
    internal class AuthException : Exception
    {
        internal AuthException() : base() { }
        internal AuthException(string str) : base(str) { }
        internal AuthException(string str, Exception inner) : base(str, inner) { }
    }

    internal class UserException : Exception
    {
        internal UserException() : base() { }
        internal UserException(string str) : base(str) { }
        internal UserException(string str, Exception inner) : base(str, inner) { }
    }

    internal class InputException : Exception
    {
        internal InputException() : base() { }
        internal InputException(string str) : base(str) { }
        internal InputException(string str, Exception inner) : base(str, inner) { }
    }
}