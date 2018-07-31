using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Shiva.Core.Identities
{
    /// <summary>
    /// Cached list Identifiable
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    ///
    public class IdentifiableList<T> : IEnumerable<T> where T:IIdentifiable
    {
        #region Private Fields

        private readonly IDictionary<Identity, T> _addedElement = new Dictionary<Identity, T>();
        private readonly IList<Identity> _removedElement = new List<Identity>();

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Gets count Elements.
        /// </summary>
        /// <value>
        /// The count Element.
        /// </value>
        public int Count => this._addedElement.Count;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        public void Add(T element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            if (!this._addedElement.ContainsKey(element.Id))
                this._addedElement.Add(element.Id, element);
            else
                this._addedElement[element.Id] = element;

            this._removedElement.Remove(element.Id);

        }

        /// <summary>
        /// Removes the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        public void Remove(T element)
        {
            this.Remove(element?.Id);
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Remove(Identity id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (!this._removedElement.Contains(id))
            {                
                this._addedElement.Remove(id);
            }
            this._removedElement.Add(id);
        }

        /// <summary>
        /// Gets the removed element.
        /// </summary>
        /// <value>
        /// The removed element.
        /// </value>
        public IEnumerable<Identity> RemovedElement => this._removedElement.ToList();

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this._addedElement.Clear();
            this._removedElement.Clear();
        }

        /// <summary>
        /// Gets the T with the specified identifier.
        /// </summary>
        /// <value>
        /// The T element.
        /// </value>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T this[Identity id]
        {
            get
            {
                return this._addedElement[id];
            }
        }

        /// <summary>
        /// Determines whether [contains] [the specified identifier].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(Identity id)
        {
            return this._addedElement.ContainsKey(id);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this._addedElement.Values.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"> </see> object that can be used to
        /// iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._addedElement.Values.GetEnumerator();
        }

        #endregion Public Methods

        /// <summary>
        /// Gets the ids.
        /// </summary>
        /// <value>
        /// The ids.
        /// </value>
        public IEnumerable<Identity> Ids => this._addedElement.Keys;

    }
}