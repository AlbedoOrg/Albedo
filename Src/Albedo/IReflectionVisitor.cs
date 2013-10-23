namespace Ploeh.Albedo
{
    /// <summary>
    /// Represents a visitor which can visit <see cref="IReflectionElement"/> nodes.
    /// </summary>
    /// <seealso cref="ReflectionVisitor{T}"/>
    /// <typeparam name="T">The type of observations or calculations the 
    /// visitor makes</typeparam>
    public interface IReflectionVisitor<T>
    {
        /// <summary>
        /// Gets the observations or values produced by this visitor instance.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Allows an <see cref="AssemblyElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="assemblyElement">The <see cref="AssemblyElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(AssemblyElement assemblyElement);
    }
}
