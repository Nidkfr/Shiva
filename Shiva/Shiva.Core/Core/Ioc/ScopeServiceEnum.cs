namespace Shiva.Core.Ioc
{
    /// <summary>
    /// Scope mode for service container
    /// </summary>
    public enum ScopeServiceEnum : byte
    {
        /// <summary>
        /// Return a new Instance of object at each Resolve
        /// </summary>
        TRANSIENT = 0,

        /// <summary>
        /// Return the same instance of object at each Resolve
        /// </summary>
        SINGLETON = 1,
    }
}