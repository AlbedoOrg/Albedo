using System;
using System.Collections.Generic;
using System.Linq;

namespace Albedo
{
    /// <summary>
    /// An implementation of a polymorphic <see cref="IReflectionElement"/> 
    /// that composes other <see cref="IReflectionElement"/> instances which
    /// can be visited by an <see cref="IReflectionVisitor{T}"/> instance.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class CompositeReflectionElement : IReflectionElement, IEnumerable<IReflectionElement>
    {
        private readonly IEnumerable<IReflectionElement> elements;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CompositeReflectionElement"/> class.
        /// </summary>
        /// <param name="elements">
        /// The <see cref="IReflectionElement"/> instances to compose.</param>
        public CompositeReflectionElement(params IReflectionElement[] elements)
        {
            this.elements = elements;
        }

        /// <summary>
        /// Accepts the <see cref="IReflectionVisitor{T}"/> visitor.
        /// </summary>
        /// <typeparam name="T">
        /// The type of observation(s) of the visitor.
        /// </typeparam>
        /// <param name="visitor">The visitor to accept.</param>
        /// <returns></returns>
        public IReflectionVisitor<T> Accept<T>(IReflectionVisitor<T> visitor)
        {
            return this.elements.Aggregate(visitor, (v, e) => e.Accept(v));
        }

        /// <summary>
        /// Returns an enumerator that iterates through the supplied 
        /// <see cref="IReflectionElement"/> instances.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{IReflectionElement}" /> that can be used to
        /// iterate through the supplied <see cref="IReflectionElement"/> 
        /// instances.
        /// </returns>
        public IEnumerator<IReflectionElement> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object" />, is equal to
        /// this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this
        /// instance.</param>
        /// <returns>
        /// <see langword="true" /> if the specified <see cref="Object" /> is
        /// equal to this instance; otherwise, <see langword="false" />.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Two instances of <see cref="CompositeReflectionElement" /> are
        /// considered to be equal if their <see cref="IReflectionElement" /> 
        /// sequences are equal.
        /// </para>
        /// </remarks>
        public override bool Equals(object obj)
        {
            var other = obj as CompositeReflectionElement;
            if (other == null)
                return false;

            return this.SequenceEqual(other);
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
            return (from element in this select element.GetHashCode())
                .Aggregate((x, y) => x ^ y);
        }
    }
}