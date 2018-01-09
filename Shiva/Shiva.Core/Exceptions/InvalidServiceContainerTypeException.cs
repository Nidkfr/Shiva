using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Exceptions
{
    /// <summary>
    /// Exception : Type constructor is invalid for service container
    /// </summary>
    /// <seealso cref="System.Exception" />
    public sealed class InvalidTypeConstructorForServiceContainerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTypeConstructorForServiceContainerException"/> class.
        /// </summary>
        /// <param name="invalidType">Type of the invalid.</param>
        public InvalidTypeConstructorForServiceContainerException(Type invalidType)
        {
            this.InvalidType = invalidType ?? throw new ArgumentNullException(nameof(invalidType));
        }

        /// <summary>
        /// Gets the type of the invalid.
        /// </summary>
        /// <value>
        /// The type of the invalid.
        /// </value>
        public Type InvalidType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message => $"{this.InvalidType} is not valide for service container. Constructor need have not parameter.";
    }
}
