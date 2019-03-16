using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lingva.DataAccessLayer
{
    public class LingvaException : Exception
    {
        public LingvaException() : base() { }

        public LingvaException(string message) : base(message) { }

        public LingvaException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
