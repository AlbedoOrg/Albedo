using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo
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
        /// The type of observation(s) of the vistor.
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
    }
}