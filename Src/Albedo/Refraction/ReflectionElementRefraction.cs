using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo.Refraction
{
    /// <summary>
    /// Filters a sequence of source objects, allowing only
    /// <see cref="IReflectionElement" /> instances to pass through.
    /// </summary>
    /// <typeparam name="T">The type of source objects.</typeparam>
    public class ReflectionElementRefraction<T> : IReflectionElementRefraction<T>
    {
        /// <summary>
        /// Filters a sequence of source objects, allowing only
        /// <see cref="IReflectionElement" /> instances to pass through.
        /// </summary>
        /// <param name="source">The source objects.</param>
        /// <returns>
        /// A sequence of only those elements of <paramref name="source" />
        /// that are already <see cref="IReflectionElement" /> instances.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source" /> is null.
        /// </exception>
        public IEnumerable<IReflectionElement> Refract(IEnumerable<T> source)
        {
            return source.OfType<IReflectionElement>();
        }
    }
}
