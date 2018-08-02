using System;

namespace Shiva.Exceptions
{
    /// <summary>
    /// Exception : Invalid Enum Otpion
    /// </summary>
    /// <seealso cref="System.Exception" />
    ///
    public sealed class InvalidEnumOptionException : Exception
    {
        #region Private Fields

        private const string EMPTYVALUE = "[Empty]";

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEnumOptionException" /> class.
        /// </summary>
        /// <param name="enumValue">
        /// The enum value.
        /// </param>
        public InvalidEnumOptionException(string enumValue) : base($"You have a new Enum Value {(string.IsNullOrWhiteSpace(enumValue) ? EMPTYVALUE : enumValue)} in Switch statement , it's not implemented.")
        {
            this.EnumValue = enumValue;
        }

        #endregion Public Constructors

        #region Public Properties

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

        #endregion Public Properties
    }
}