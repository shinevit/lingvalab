using System;
using System.Globalization;

namespace Lingva.DataAccessLayer
{
    public class LingvaException : Exception
    {
        public LingvaException()
        {
        }

        public LingvaException(string message) : base(message)
        {
        }

        public LingvaException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}