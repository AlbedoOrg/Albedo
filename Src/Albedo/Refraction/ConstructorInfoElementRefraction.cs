using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Albedo.Refraction
{
    /// <summary>
    /// Creates <see cref="ConstructorInfoElement" /> instances from a sequence
    /// of source objects.
    /// </summary>
    /// <typeparam name="T">The type of source objects.</typeparam>
    /// <seealso cref="Refract(IEnumerable{T})" />
    public class ConstructorInfoElementRefraction<T> : IReflectionElementRefraction<T>
    {
        /// <summary>
        /// Creates <see cref="ConstructorInfoElement" /> instances from a
        /// sequence of source objects.
        /// </summary>
        /// <param name="source">The source objects.</param>
        /// <returns>
        /// A sequence of <see cref="ConstructorInfoElement" /> instances
        /// created from <paramref name="source" />.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source" /> is null.
        /// </exception>
        /// <remarks>
        /// <para>
        /// This method creates <see cref="ConstructorInfoElement" /> instances
        /// from all matching elements in <paramref name="source" />. An
        /// element is matching if it's a <see cref="ConstructorInfo" />
        /// instance, in which case a corresponding
        /// <strong>ConstructorInfoElement</strong> is created and returned.
        /// </para>
        /// </remarks>
        /// <seealso cref="IReflectionElementRefraction{T}" />
        public IEnumerable<IReflectionElement> Refract(IEnumerable<T> source)
        {
            return source
                .OfType<ConstructorInfo>()
                .Select(ci => new ConstructorInfoElement(ci))
                .Cast<IReflectionElement>();
        }
    }
}
