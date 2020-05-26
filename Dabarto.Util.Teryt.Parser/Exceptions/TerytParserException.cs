using System;
using System.Runtime.Serialization;

namespace Dabarto.Util.Teryt.Parser.Exceptions
{
    [Serializable]
    public class TerytParserException : Exception
    {
        public TerytParserException()
        {
        }

        public TerytParserException(string message) : base(message)
        {
        }

        public TerytParserException(string message, Exception inner) : base(message, inner)
        {
        }

        protected TerytParserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}