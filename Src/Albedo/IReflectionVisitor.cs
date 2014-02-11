namespace Ploeh.Albedo
{
    /// <summary>
    /// Represents a Visitor which can visit <see cref="IReflectionElement" />
    /// instances.
    /// </summary>
    /// <typeparam name="T">
    /// The type of observation or calculation the Visitor makes.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// <strong>Note to implementers:</strong>
    /// </para>
    /// <para>
    /// Prefer deriving from <see cref="ReflectionVisitor{T}" /> instead of
    /// directly implementing the <strong>IReflectionVisitor&lt;T&gt;</strong>
    /// interface. The reason for this is that future versions of Albedo may
    /// add more methods to the interface. This will be a breaking change, so
    /// will only happen on major releases, but still, deriving from the
    /// <strong>ReflectionVisitor&lt;T&gt;</strong> base class gives you a
    /// better chance that your implementation will be compatible across a
    /// major release.
    /// </para>
    /// </remarks>
    public interface IReflectionVisitor<out T>
    {
        /// <summary>
        /// Gets the observation or value produced by this Visitor instance.
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

        IReflectionVisitor<T> Visit(params TypeElement[] typeElements);

        /// <summary>
        /// Allows an <see cref="TypeElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="typeElement">The <see cref="TypeElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(TypeElement typeElement);

        IReflectionVisitor<T> Visit(params FieldInfoElement[] fieldInfoElements);
        IReflectionVisitor<T> Visit(params ConstructorInfoElement[] constructorInfoElements);
        IReflectionVisitor<T> Visit(params PropertyInfoElement[] propertyInfoElements);
        IReflectionVisitor<T> Visit(params MethodInfoElement[] methodInfoElements);
        IReflectionVisitor<T> Visit(params EventInfoElement[] eventInfoElements);

        /// <summary>
        /// Allows an <see cref="FieldInfoElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="fieldInfoElement">The <see cref="FieldInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(FieldInfoElement fieldInfoElement);

        /// <summary>
        /// Allows an <see cref="ConstructorInfoElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="constructorInfoElement">The <see cref="ConstructorInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(ConstructorInfoElement constructorInfoElement);

        /// <summary>
        /// Allows an <see cref="PropertyInfoElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="propertyInfoElement">The <see cref="PropertyInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(PropertyInfoElement propertyInfoElement);

        /// <summary>
        /// Allows an <see cref="MethodInfoElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="methodInfoElement">The <see cref="MethodInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(MethodInfoElement methodInfoElement);

        /// <summary>
        /// Allows an <see cref="EventInfoElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="eventInfoElement">The <see cref="EventInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(EventInfoElement eventInfoElement);

        IReflectionVisitor<T> Visit(params ParameterInfoElement[] parameterInfoElements);
        IReflectionVisitor<T> Visit(params LocalVariableInfoElement[] localVariableInfoElements);

        /// <summary>
        /// Allows an <see cref="ParameterInfoElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="parameterInfoElement">The <see cref="ParameterInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(ParameterInfoElement parameterInfoElement);

        /// <summary>
        /// Allows an <see cref="LocalVariableInfoElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="localVariableInfoElement">The <see cref="LocalVariableInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(LocalVariableInfoElement localVariableInfoElement);
    }
}
