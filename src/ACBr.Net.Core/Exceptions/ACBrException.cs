using System;
using System.Runtime.Serialization;

namespace ACBr.Net.Core
{
    public class ACBrException : ApplicationException
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ACBrException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ACBrException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ACBrException"/> class.
        /// </summary>
        public ACBrException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ACBrException"/> class.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public ACBrException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ACBrException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ACBrException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ACBrException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public ACBrException(Exception innerException, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ACBrException" /> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        public ACBrException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion Constructor
    }
}