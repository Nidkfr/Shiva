using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Exceptions
{
    /// <summary>
    /// Exception : Invalid Enum Otpion
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidEnumOptionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEnumOptionException"/> class.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        public InvalidEnumOptionException(string enumValue):base($"You have a new Enum Value {enumValue} in Switch statement , it's not implemented.")
        {
            this.EnumValue = enumValue;
        }

        /// <summary>
        /// Gets the enum value.
        /// </summary>
        /// <value>
        /// The enum value.
        /// </value>
        public string EnumValue
        {
            get;
            private set;
        }
    }
}
