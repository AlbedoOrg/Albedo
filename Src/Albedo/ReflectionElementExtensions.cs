using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ploeh.Albedo
{
    /// <summary>
    /// Contains extension methods for dealing with <see cref="IReflectionElement"/>
    /// instances, both producing and consuming them.
    /// </summary>
    public static class ReflectionElementExtensions
    {
        /// <summary>
        /// Accepts the <see cref="IReflectionVisitor{T}"/> visitor on each of the
        /// <paramref name="elements"/> in the sequence.
        /// </summary>
        /// <typeparam name="T">
        /// The type of observation or result which the <see cref="IReflectionVisitor{T}"/>
        /// instance produces when visiting nodes.
        /// </typeparam>
        /// <param name="elements">
        /// The sequence of <see cref="IReflectionElement"/> instances upon which 
        /// the <see cref="IReflectionElement.Accept{T}"/> method will be called.
        /// </param>
        /// <param name="visitor">The <see cref="IReflectionVisitor{T}"/> instance.</param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public static IReflectionVisitor<T> Accept<T>(
            this IEnumerable<IReflectionElement> elements,
            IReflectionVisitor<T> visitor)
        {
            if (elements == null) throw new ArgumentNullException("elements");
            return new CompositeReflectionElement(elements.ToArray()).Accept(visitor);
        }
    }
}