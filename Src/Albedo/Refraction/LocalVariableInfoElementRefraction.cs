using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ploeh.Albedo.Refraction
{
    /// <summary>
    /// Creates <see cref="LocalVariableInfoElement" /> instances from a sequence of
    /// source objects.
    /// </summary>
    /// <typeparam name="T">The type of source objects.</typeparam>
    /// <seealso cref="Materialize(IEnumerable{T})" />
    public class LocalVariableInfoElementRefraction<T> : IReflectionElementRefraction<T>
    {
        /// <summary>
        /// Creates <see cref="LocalVariableInfoElement" /> instances from a sequence of
        /// source objects.
        /// </summary>
        /// <param name="source">The source objects.</param>
        /// <returns>
        /// A sequence of <see cref="LocalVariableInfoElement" /> instances created from
        /// a <paramref name="source" />.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source" /> is null.
        /// </exception>
        /// <remarks>
        /// <para>
        /// This method creates <see cref="LocalVariableInfoElement" /> instances from
        /// all matching elements in <paramref name="source" />. An element is
        /// matching if it's an <see cref="LocalVariableInfo" /> instance, in which case
        /// a corresponding <strong>LocalVariableInfoElement</strong> is created and
        /// returned.
        /// </para>
        /// </remarks>
        /// <seealso cref="IReflectionElementRefraction{T}" />
        public IEnumerable<IReflectionElement> Materialize(IEnumerable<T> source)
        {
            return source
                .OfType<LocalVariableInfo>()
                .Select(a => new LocalVariableInfoElement(a))
                .Cast<IReflectionElement>();
        }
    }
}