using System;
using System.Globalization;

namespace TestApp.Core.Dtos.Exception
{
    public class AppException : System.Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}