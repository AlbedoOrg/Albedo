using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ploeh.Albedo.Refraction
{
    /// <summary>
    /// Creates <see cref="FieldInfoElement" /> instances from a sequence of
    /// source objects.
    /// </summary>
    /// <typeparam name="T">The type of source objects.</typeparam>
    /// <seealso cref="Materialize(IEnumerable{T})" />
    public class FieldInfoElementMaterializer<T> : IReflectionElementRefraction<T>
    {
        /// <summary>
        /// Creates <see cref="FieldInfoElement" /> instances from a sequence
        /// of source objects.
        /// </summary>
        /// <param name="source">The source objects.</param>
        /// <returns>
        /// A sequence of <see cref="FieldInfoElement" /> instances created
        /// from <paramref name="source" />.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source" /> is null.
        /// </exception>
        /// <remarks>
        /// <para>
        /// This method creates <see cref="FieldInfoElement" /> instances from
        /// all matching elements in <paramref name="source" />. An element is
        /// matching if it's a <see cref="FieldInfo" /> instance, in which case
        /// a corresponding <strong>FieldInfoElement</strong> is created and
        /// returned.
        /// </para>
        /// </remarks>
        /// <seealso cref="IReflectionElementRefraction{T}" />
        public IEnumerable<IReflectionElement> Materialize(IEnumerable<T> source)
        {
            return source
                .OfType<FieldInfo>()
                .Select(fi => new FieldInfoElement(fi))
                .Cast<IReflectionElement>();
        }
    }
}
