using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Lingva.DataAccessLayer.Exceptions
{
    public class StatisticsServiceException : Exception
    {
        public StatisticsServiceException()
        {
        }

        public StatisticsServiceException(string message) : base(message)
        {
        }

        public StatisticsServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StatisticsServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
