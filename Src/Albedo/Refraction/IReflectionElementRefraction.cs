using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo.Refraction
{
    /// <summary>
    /// Creates a sequence of <see cref="IReflectionElement" /> instances from
    /// a sequence of source objects.
    /// </summary>
    /// <typeparam name="T">The type of source objects.</typeparam>
    /// <remarks>
    /// <para>
    /// The purpose of the
    /// <strong>IReflectionElementRefraction&lt;T&gt;</strong> interface is
    /// to enable clients to easily transform one or more source objects into
    /// <see cref="IReflectionElement" /> instances. A common use case is to
    /// use implementations of this interface to transform one or more
    /// Reflection instances, such as
    /// <see cref="System.Reflection.PropertyInfo" /> or
    /// <see cref="System.Reflection.ParameterInfo" />, into their respective
    /// <strong>IReflectionElement</strong> Adapters. However, because of the
    /// existense of <see cref="NullReflectionElement" />, an optional strategy
    /// might be to return a <strong>NullReflectionElement</strong> instance
    /// for any source object that doesn't have an appropriate
    /// <strong>IReflectionElement</strong> Adapter.
    /// </para>
    /// </remarks>
    /// <seealso cref="Refract(IEnumerable{T})" />
    public interface IReflectionElementRefraction<T>
    {
        /// <summary>
        /// Creates a sequence of <see cref="IReflectionElement" /> instances
        /// from a sequence of source objects.
        /// </summary>
        /// <param name="source">The source objects.</param>
        /// <returns>
        /// A sequence of <see cref="IReflectionElement" /> instances created
        /// from <paramref name="source" />.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method doesn't guarantee that the returned sequence has the
        /// same number of items as <paramref name="source" />; in fact, an
        /// implementation is allowed to return an empty sequence if no items
        /// in the input sequence could be refracted.
        /// </para>
        /// <para>
        /// Note to implementers:
        /// </para>
        /// <para>
        /// It's perfectly fine to return an empty sequence if no items in
        /// <paramref name="source" /> can be refracted. However,
        /// <see langword="null" /> is never considered an appropriate return
        /// value.
        /// </para>
        /// <para>
        /// Implementations are allowed to filter the input sequence, or even
        /// add new items in the output sequence.
        /// </para>
        /// </remarks>
        /// <seealso cref="IReflectionElementRefraction{T}" />
        IEnumerable<IReflectionElement> Refract(IEnumerable<T> source);
    }
}
