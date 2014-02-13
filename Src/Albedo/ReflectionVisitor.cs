using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo
{
    /// <summary>
    /// Represents a Visitor base class which can visit
    /// <see cref="IReflectionElement" /> instances.
    /// </summary>
    /// <typeparam name="T">
    /// The type of observations or calculations the Visitor makes.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// Implementers should prefer deriving from this class instead of
    /// implementing <see cref="IReflectionVisitor{T}" /> directly. The reason
    /// for this is that future versions of Albedo may add more methods to the
    /// interface. This will be a breaking change, so will only happen on major
    /// releases, but still, deriving from the
    /// <strong>ReflectionVisitor&lt;T&gt;</strong> base class gives you a
    /// better chance that your implementation will be compatible across a
    /// major release.
    /// </para>
    /// <para>
    /// Consumers, on the other hand, should still rely on the
    /// <strong>IReflectionVisitor&lt;T&gt;</strong> interface.
    /// </para>
    /// </remarks>
    public abstract class ReflectionVisitor<T> : IReflectionVisitor<T>
    {
        /// <summary>
        /// Gets the observation or value produced by this instance.
        /// </summary>
        public abstract T Value { get; }

        /// <summary>
        /// Allows an <see cref="AssemblyElement"/> to be visited. 
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="assemblyElement">
        /// The <see cref="AssemblyElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation simply returns
        /// <paramref name="assemblyElement" /> without doing anything, but
        /// since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(
            AssemblyElement assemblyElement)
        {
            if (assemblyElement == null)
                throw new ArgumentNullException("assemblyElement");

            return Visit(assemblyElement.GetTypeElements());
        }

        public virtual IReflectionVisitor<T> Visit(params TypeElement[] typeElements)
        {
            if (typeElements == null)
                throw new ArgumentNullException("typeElements");

            return typeElements.Aggregate((IReflectionVisitor<T>)this, (v, t) => v.Visit(t));
        }

        /// <summary>
        /// Allows an <see cref="TypeElement"/> to be visited. This method is
        /// called when the element accepts this visitor instance.
        /// </summary>
        /// <param name="typeElement">
        /// The <see cref="TypeElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation simply returns <paramref name="typeElement" />
        /// without doing anything, but since the method is virtual, child
        /// classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(
            TypeElement typeElement)
        {
            return Visit(typeElement.GetFieldInfoElements())
                .Visit(typeElement.GetConstructorInfoElements())
                .Visit(typeElement.GetPropertyInfoElements())
                .Visit(typeElement.GetMethodInfoElements())
                .Visit(typeElement.GetEventInfoElements());
        }

        public virtual IReflectionVisitor<T> Visit(params FieldInfoElement[] fieldInfoElements)
        {
            if (fieldInfoElements == null)
                throw new ArgumentNullException("fieldInfoElements");

            return fieldInfoElements.Aggregate((IReflectionVisitor<T>)this, (v, f) => v.Visit(f));
        }

        public virtual IReflectionVisitor<T> Visit(params ConstructorInfoElement[] constructorInfoElements)
        {
            if (constructorInfoElements == null)
                throw new ArgumentNullException("constructorInfoElements");

            return constructorInfoElements.Aggregate((IReflectionVisitor<T>)this, (v, c) => v.Visit(c));
        }

        public virtual IReflectionVisitor<T> Visit(params PropertyInfoElement[] propertyInfoElements)
        {
            if (propertyInfoElements == null)
                throw new ArgumentNullException("propertyInfoElements");

            return propertyInfoElements.Aggregate((IReflectionVisitor<T>)this, (v, p) => v.Visit(p));
        }

        public virtual IReflectionVisitor<T> Visit(params MethodInfoElement[] methodInfoElements)
        {
            if (methodInfoElements == null)
                throw new ArgumentNullException("methodInfoElements");

            return methodInfoElements.Aggregate((IReflectionVisitor<T>)this, (v, m) => v.Visit(m));
        }

        public virtual IReflectionVisitor<T> Visit(params EventInfoElement[] eventInfoElements)
        {
            if (eventInfoElements == null)
                throw new ArgumentNullException("eventInfoElements");

            return eventInfoElements.Aggregate((IReflectionVisitor<T>)this, (v, e) => v.Visit(e));
        }

        /// <summary>
        /// Allows an <see cref="FieldInfoElement"/> to be visited. 
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="fieldInfoElement">
        /// The <see cref="FieldInfoElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation simply returns
        /// <paramref name="fieldInfoElement" /> without doing anything,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(
            FieldInfoElement fieldInfoElement)
        {
            return this;
        }

        /// <summary>
        /// Allows an <see cref="ConstructorInfoElement"/> to be visited. 
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="constructorInfoElement">
        /// The <see cref="ConstructorInfoElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation simply returns
        /// <paramref name="constructorInfoElement" /> without doing anything,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(
            ConstructorInfoElement constructorInfoElement)
        {
            return this;
        }

        /// <summary>
        /// Allows an <see cref="PropertyInfoElement"/> to be visited. 
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="propertyInfoElement">
        /// The <see cref="PropertyInfoElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation simply returns
        /// <paramref name="propertyInfoElement" /> without doing anything,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(
            PropertyInfoElement propertyInfoElement)
        {
            return this;
        }

        /// <summary>
        /// Allows an <see cref="MethodInfoElement"/> to be visited. 
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="methodInfoElement">
        /// The <see cref="MethodInfoElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation simply returns
        /// <paramref name="methodInfoElement" /> without doing anything,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(
            MethodInfoElement methodInfoElement)
        {
            return this;
        }

        /// <summary>
        /// Allows an <see cref="EventInfoElement"/> to be visited. This method
        /// is called when the element accepts this visitor instance.
        /// </summary>
        /// <param name="eventInfoElement">
        /// The <see cref="EventInfoElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation simply returns
        /// <paramref name="eventInfoElement" /> without doing anything, but
        /// since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(
            EventInfoElement eventInfoElement)
        {
            return this;
        }

        public IReflectionVisitor<T> Visit(params ParameterInfoElement[] parameterInfoElements)
        {
            throw new NotImplementedException();
        }

        public virtual IReflectionVisitor<T> Visit(params LocalVariableInfoElement[] localVariableInfoElements)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows an <see cref="ParameterInfoElement"/> to be visited. 
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="parameterInfoElement">
        /// The <see cref="ParameterInfoElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation simply returns
        /// <paramref name="parameterInfoElement" /> without doing anything,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(
            ParameterInfoElement parameterInfoElement)
        {
            return this;
        }

        /// <summary>
        /// Allows an <see cref="LocalVariableInfoElement"/> to be visited. 
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="localVariableInfoElement">
        /// The <see cref="LocalVariableInfoElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation simply returns
        /// <paramref name="localVariableInfoElement" /> without doing
        /// anything, but since the method is virtual, child classes can
        /// override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(
            LocalVariableInfoElement localVariableInfoElement)
        {
            return this;
        }
    }
}
