using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RjamSoft.Tamil.Grammar.Prosody.Infrastructure.Contract
{
    public interface IDistributedCachingService : IDisposable
    {
        /// <summary>
		/// Removes the specified item from the cache.
		/// </summary>
		/// <param name="key">The identifier for the item to delete.</param>
		/// <returns>
		/// true if the item was successfully removed from the cache; false otherwise.
		/// </returns>
		bool Remove(string key);

        void RemoveAll(System.Collections.Generic.IEnumerable<string> keys);

        T Get<T>(string key);

        /// <summary>
        /// Increments the value of the specified key by the given amount.
        /// The operation is atomic and happens on the server.
        /// A non existent value at key starts at 0
        /// </summary>
        /// <param name="key">The identifier for the item to increment.</param>
        /// <param name="amount">The amount by which the client wants to increase the item.
        /// </param>
        /// <returns>
        /// The new value of the item or -1 if not found.
        /// </returns>
        /// <remarks>The item must be inserted into the cache before it can be changed. The
        /// item must be inserted as a <see cref="T:System.String"/>. The operation only
        /// works with <see cref="T:System.UInt32"/> values, so -1 always indicates that
        /// the item was not found.</remarks>
        long Increment(string key, uint amount);

        /// <summary>
        /// Increments the value of the specified key by the given amount.
        /// The operation is atomic and happens on the server.
        /// A non existent value at key starts at 0
        /// </summary>
        /// <param name="key">The identifier for the item to increment.</param>
        /// <param name="amount">The amount by which the client wants to decrease the item.
        /// </param>
        /// <returns>
        /// The new value of the item or -1 if not found.
        /// </returns>
        /// <remarks>The item must be inserted into the cache before it can be changed. The
        /// item must be inserted as a <see cref="T:System.String"/>. The operation only
        /// works with <see cref="T:System.UInt32"/> values, so -1 always indicates that
        /// the item was not found.</remarks>
        long Decrement(string key, uint amount);

        bool Add<T>(string key, T value);

        bool Set<T>(string key, T value);

        bool Replace<T>(string key, T value);

        bool Add<T>(string key, T value, System.DateTime expiresAt);

        bool Set<T>(string key, T value, System.DateTime expiresAt);

        bool Replace<T>(string key, T value, System.DateTime expiresAt);

        bool Add<T>(string key, T value, System.TimeSpan expiresIn);

        bool Set<T>(string key, T value, System.TimeSpan expiresIn);

        bool Replace<T>(string key, T value, System.TimeSpan expiresIn);

        /// <summary>
        /// Invalidates all data on the cache.
        /// </summary>
        void FlushAll();

        System.Collections.Generic.IDictionary<string, T> GetAll<T>(System.Collections.Generic.IEnumerable<string> keys);

        void SetAll<T>(System.Collections.Generic.IDictionary<string, T> values);
    }
}
