// AXSharp.Abstractions
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Runtime.Serialization;

namespace AXSharp.Abstractions.Presentation
{
    public class PresentationProviderNotAssignedException : Exception
    {
        public PresentationProviderNotAssignedException(string message): base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="PresentationProviderNotAssignedException"></see> class.</summary>
        public PresentationProviderNotAssignedException()
        {

        }

        /// <summary>Initializes a new instance of the <see cref="PresentationProviderNotAssignedException"></see> class with serialized data.</summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="info">info</paramref> parameter is null.</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">The class name is null or <see cref="System.Exception.HResult"></see> is zero (0).</exception>
        protected PresentationProviderNotAssignedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        /// <summary>Initializes a new instance of the <see cref="PresentationProviderNotAssignedException"></see> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public PresentationProviderNotAssignedException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
