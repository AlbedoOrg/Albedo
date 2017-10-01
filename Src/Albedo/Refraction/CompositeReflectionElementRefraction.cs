using System;
using System.Collections.Generic;
using System.Linq;

namespace Albedo.Refraction
{
    /// <summary>
    /// Composes multiple <see cref="IReflectionElementRefraction{T}" />
    /// instances, so that they look like a single instance of
    /// <strong>IReflectionElementRefraction&lt;T&gt;</strong>.
    /// </summary>
    /// <typeparam name="T">The type of source objects.</typeparam>
    /// <remarks>
    /// <para>
    /// This is a standard implementation of the Composite design pattern.
    /// </para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is not a 'collection', it's an Iterator. Reported as a CA bug at https://connect.microsoft.com/VisualStudio/feedback/details/771480/code-analysis-rule-about-ienumerable-leads-to-misleading-naming.")]
    public class CompositeReflectionElementRefraction<T> : 
        IReflectionElementRefraction<T>,
        IEnumerable<IReflectionElementRefraction<T>>
    {
        private readonly IEnumerable<IReflectionElementRefraction<T>> refractions;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="CompositeReflectionElementRefraction{T}"/> class.
        /// </summary>
        /// <param name="refractions">The refractions to compose.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="refractions" /> is null.
        /// </exception>
        /// <remarks>
        /// <para>
        /// After construction, the injected <paramref name="refractions" />
        /// are available by enumerating over the new instance.
        /// </para>
        /// </remarks>
        /// <seealso cref="GetEnumerator" />
        public CompositeReflectionElementRefraction(
            params IReflectionElementRefraction<T>[] refractions)
        {
            if (refractions == null)
                throw new ArgumentNullException("refractions");

            this.refractions = refractions;
        }

        /// <summary>
        /// Creates a sequence of <see cref="IReflectionElement" /> instances
        /// from a sequence of source objects, but composing the result of
        /// invoking <strong>Refract</strong> on all composed refractions.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <returns>
        /// A a sequence of <see cref="IReflectionElement" /> instances from a
        /// sequence of source objects, but composing the result of invoking 
        /// <strong>Refract</strong> on all composed refractions.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source" /> is null.
        /// </exception>
        public IEnumerable<IReflectionElement> Refract(IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return from r in this.refractions
                   from re in r.Refract(source)
                   select re;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the composed
        /// refractions.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that
        /// can be used to iterate through the composed refractions.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The elements returned by this method are the refractions passed to
        /// the constructor when the instance was created.
        /// </para>
        /// </remarks>
        /// <seealso cref="CompositeReflectionElementRefraction(IReflectionElementRefraction{T}[])" />
        public IEnumerator<IReflectionElementRefraction<T>> GetEnumerator()
        {
            return this.refractions.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
