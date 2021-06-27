using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.Exceptions
{
    public class ErrorException:Exception
    {
        public ErrorException() : base() { }
        public ErrorException(string message) : base(message) { }
        public ErrorException(string message, params object[] args)
       : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
