using System;
using System.Linq;

namespace Ploeh.Albedo
{
    /// <summary>
    /// Allows composing an arbitrary list of <see cref="IReflectionElement"/>
    /// instances, providing a way to <see cref="Accept{T}"/> and visit them all.
    /// </summary>
    public class CompositeReflectionElement : IReflectionElement
    {
        private readonly IReflectionElement[] elements;

        /// <summary>
        /// Constructs a new instance of the <see cref="CompositeReflectionElement"/> which represents
        /// contains the list of <paramref name="elements"/>.
        /// </summary>
        /// <param name="elements">The elements this element represents.</param>
        public CompositeReflectionElement(params IReflectionElement[] elements)
        {
            this.elements = elements;
        }

        /// <summary>
        /// Accepts the provided <see cref="IReflectionVisitor{T}"/>, by calling the
        /// <see cref="IReflectionElement.Accept{T}"/> method on all contained elements
        /// passed to the constructor.
        /// </summary>
        /// <typeparam name="T">The type of observation or result which the
        /// <see cref="IReflectionVisitor{T}"/> instance produces when visiting nodes.</typeparam>
        /// <param name="visitor">The <see cref="IReflectionVisitor{T}"/> instance.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        public IReflectionVisitor<T> Accept<T>(IReflectionVisitor<T> visitor)
        {
            if (visitor == null) throw new ArgumentNullException("visitor");
            return this.elements.Aggregate(visitor, (v, e) => e.Accept(v));
        }
    }
}