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

        /// <summary>
        /// Allows an <see cref="TypeElement"/> to be 'visited'. Override this method to allow
        /// the derived visitor to observe the element.
        /// </summary>
        /// <param name="typeElement">The <see cref="TypeElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        public virtual IReflectionVisitor<T> Visit(TypeElement typeElement)
        {
            return this;
        }

        /// <summary>
        /// Allows an <see cref="ParameterInfoElement"/> to be 'visited'. Override this method to allow
        /// the derived visitor to observe the element.
        /// </summary>
        /// <param name="parameterInfoElement">The <see cref="ParameterInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        public virtual IReflectionVisitor<T> Visit(ParameterInfoElement parameterInfoElement)
        {
            return this;
        }

        /// <summary>
        /// Allows an <see cref="PropertyInfoElement"/> to be 'visited'. Override this method to allow
        /// the derived visitor to observe the element.
        /// </summary>
        /// <param name="propertyInfoElement">The <see cref="PropertyInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        public virtual IReflectionVisitor<T> Visit(PropertyInfoElement propertyInfoElement)
        {
            return this;
        }

        /// <summary>
        /// Allows an <see cref="FieldInfoElement"/> to be 'visited'. Override this method to allow
        /// the derived visitor to observe the element.
        /// </summary>
        /// <param name="fieldInfoElement">The <see cref="FieldInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        public virtual IReflectionVisitor<T> Visit(FieldInfoElement fieldInfoElement)
        {
            return this;
        }

        /// <summary>
        /// Allows an <see cref="ConstructorInfoElement"/> to be 'visited'. Override this method to allow
        /// the derived visitor to observe the element.
        /// </summary>
        /// <param name="constructorInfoElement">The <see cref="ConstructorInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        public virtual IReflectionVisitor<T> Visit(ConstructorInfoElement constructorInfoElement)
        {
            return this;
        }

        /// <summary>
        /// Allows an <see cref="MethodInfoElement"/> to be 'visited'. Override this method to allow
        /// the derived visitor to observe the element.
        /// </summary>
        /// <param name="methodInfoElement">The <see cref="MethodInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        public virtual IReflectionVisitor<T> Visit(MethodInfoElement methodInfoElement)
        {
            return this;
        }
    }
}
