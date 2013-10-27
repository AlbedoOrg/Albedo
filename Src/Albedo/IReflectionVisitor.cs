﻿namespace Ploeh.Albedo
{
    /// <summary>
    /// Represents a visitor which can visit <see cref="IReflectionElement"/> nodes.
    /// </summary>
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

        /// <summary>
        /// Allows an <see cref="ConstructorInfoElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="constructorInfoElement">The <see cref="ConstructorInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(ConstructorInfoElement constructorInfoElement);

        /// <summary>
        /// Allows an <see cref="FieldInfoElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="fieldInfoElement">The <see cref="FieldInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(FieldInfoElement fieldInfoElement);

        /// <summary>
        /// Allows an <see cref="MethodInfoElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="methodInfoElement">The <see cref="MethodInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(MethodInfoElement methodInfoElement);

        /// <summary>
        /// Allows an <see cref="ParameterInfoElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="parameterInfoElement">The <see cref="ParameterInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(ParameterInfoElement parameterInfoElement);

        /// <summary>
        /// Allows an <see cref="PropertyInfoElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="propertyInfoElement">The <see cref="PropertyInfoElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(PropertyInfoElement propertyInfoElement);

        /// <summary>
        /// Allows an <see cref="TypeElement"/> to be 'visited'. This method is called when the
        /// element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="typeElement">The <see cref="TypeElement"/> being visited.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        IReflectionVisitor<T> Visit(TypeElement typeElement);
    }
}
