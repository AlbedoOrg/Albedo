namespace Ploeh.Albedo
{
    /// <summary>
    /// An implementation of the <see cref="IReflectionVisitor{T}"/> which does
    /// not visit any type of element. This allows easier implementation of the
    /// <see cref="IReflectionVisitor{T}"/> interface, when only certain element
    /// types need to be visited.
    /// </summary>
    public abstract class ReflectionVisitor<T> : IReflectionVisitor<T>
    {
        /// <summary>
        /// Gets the observations or values produced by this visitor instance.
        /// </summary>
        public abstract T Value { get; }

        /// <summary>
        /// Allows an <see cref="AssemblyElement"/> to be 'visited'. Override this method to allow
        /// the derived visitor to observe the element.
        /// </summary>
        /// <param name="assemblyElement">The <see cref="AssemblyElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        public virtual IReflectionVisitor<T> Visit(AssemblyElement assemblyElement)
        {
            return this;
        }
    }
}
