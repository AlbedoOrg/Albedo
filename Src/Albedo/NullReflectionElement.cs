using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo
{
    /// <summary>
    /// An implementation of IReflectionElement that does nothing.
    /// </summary>
    public class NullReflectionElement : IReflectionElement
    {
        /// <summary>
        /// Accepts the <see cref="IReflectionVisitor{T}" /> as per the
        /// Visitor pattern http://en.wikipedia.org/wiki/Visitor_pattern.
        /// </summary>
        /// <typeparam name="T">
        /// The type of observation(s) the visitor might collect.
        /// </typeparam>
        /// <param name="visitor">The visitor to accept.</param>
        /// <returns><paramref name="visitor" /></returns>
        /// <remarks>
        /// <para>
        /// While <strong>NullReflection</strong> partakes in a Visitor
        /// hierarchy, this particular implementation follows the Null Object
        /// pattern, by doing nothing. The way it does nothing is by returning
        /// <paramref name="visitor" />.
        /// </para>
        /// </remarks>
        public IReflectionVisitor<T> Accept<T>(IReflectionVisitor<T> visitor)
        {
            return visitor;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object" /> is equal to
        /// this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this
        /// instance.</param>
        /// <returns>
        /// <see langword="true" /> if the specified <see cref="Object" /> is
        /// another <see cref="NullReflectionElement" />; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is NullReflectionElement;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing
        /// algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return 64506597;
        }
    }
}
