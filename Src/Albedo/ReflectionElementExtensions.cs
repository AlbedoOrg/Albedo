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
        public static IReflectionVisitor<T> Accept<T>(
            this IEnumerable<IReflectionElement> elements,
            IReflectionVisitor<T> visitor)
        {
            if (elements == null) throw new ArgumentNullException("elements");
            return new CompositeReflectionElement(elements.ToArray()).Accept(visitor);
        }
    }
}