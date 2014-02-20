using System;
using System.Linq;
using System.Reflection;

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
        /// Allows an <see cref="AssemblyElement"/> instances to be 'visited'.
        /// This method is called when the element 'accepts' this visitor instance.
        /// </summary>
        /// <param name="assemblyElements">
        /// The <see cref="AssemblyElement"/> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation relaies each <see cref="AssemblyElement"/> instance
        /// to <see cref="Visit(AssemblyElement)"/>,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(params AssemblyElement[] assemblyElements)
        {
            if (assemblyElements == null)
                throw new ArgumentNullException("assemblyElements");

            return assemblyElements.Aggregate((IReflectionVisitor<T>)this, (v, a) => v.Visit(a));
        }

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
        /// This implementation relaies <see cref="TypeElement"/> instances
        /// from <see cref="Assembly.GetTypes()"/>, to <see cref="Visit(TypeElement[])"/>,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(
            AssemblyElement assemblyElement)
        {
            if (assemblyElement == null)
                throw new ArgumentNullException("assemblyElement");

            return Visit(assemblyElement.GetTypeElements());
        }

        /// <summary>
        /// Allows <see cref="TypeElement"/> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="typeElements">
        /// The <see cref="TypeElement"/> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation relaies each <see cref="TypeElement"/> instance
        /// to <see cref="Visit(TypeElement)"/>,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
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
        /// This implementation relaies <see cref="FieldInfoElement"/>, <see cref="ConstructorInfoElement"/>,
        /// <see cref="PropertyInfoElement"/>, <see cref="MethodInfoElement"/>, and 
        /// <see cref="EventInfoElement"/> instances to corresponding Visit method.
        /// These semantic child elements are constructed from the <see cref="TypeElement"/> parameter,
        /// and since this method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(
            TypeElement typeElement)
        {
            if (typeElement == null)
                throw new ArgumentNullException("typeElement");

            return Visit(typeElement.GetFieldInfoElements())
                .Visit(typeElement.GetConstructorInfoElements())
                .Visit(typeElement.GetPropertyInfoElements())
                .Visit(typeElement.GetMethodInfoElements())
                .Visit(typeElement.GetEventInfoElements());
        }

        /// <summary>
        /// Allows <see cref="FieldInfoElement"/> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="fieldInfoElements">
        /// The <see cref="FieldInfoElement"/> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation relaies each <see cref="FieldInfoElement"/> instance
        /// to <see cref="Visit(FieldInfoElement)"/>,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(params FieldInfoElement[] fieldInfoElements)
        {
            if (fieldInfoElements == null)
                throw new ArgumentNullException("fieldInfoElements");

            return fieldInfoElements.Aggregate((IReflectionVisitor<T>)this, (v, f) => v.Visit(f));
        }

        /// <summary>
        /// Allows <see cref="ConstructorInfoElement"/> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="constructorInfoElements">
        /// The <see cref="ConstructorInfoElement"/> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation relaies each <see cref="ConstructorInfoElement"/> instance
        /// to <see cref="Visit(ConstructorInfoElement)"/>,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(params ConstructorInfoElement[] constructorInfoElements)
        {
            if (constructorInfoElements == null)
                throw new ArgumentNullException("constructorInfoElements");

            return constructorInfoElements.Aggregate((IReflectionVisitor<T>)this, (v, c) => v.Visit(c));
        }

        /// <summary>
        /// Allows <see cref="PropertyInfoElement"/> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="propertyInfoElements">
        /// The <see cref="PropertyInfoElement"/> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation relaies each <see cref="PropertyInfoElement"/> instance
        /// to <see cref="Visit(PropertyInfoElement)"/>,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(params PropertyInfoElement[] propertyInfoElements)
        {
            if (propertyInfoElements == null)
                throw new ArgumentNullException("propertyInfoElements");

            return propertyInfoElements.Aggregate((IReflectionVisitor<T>)this, (v, p) => v.Visit(p));
        }

        /// <summary>
        /// Allows <see cref="MethodInfoElement"/> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="methodInfoElements">
        /// The <see cref="MethodInfoElement"/> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation relaies each <see cref="MethodInfoElement"/> instance
        /// to <see cref="Visit(MethodInfoElement)"/>,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(params MethodInfoElement[] methodInfoElements)
        {
            if (methodInfoElements == null)
                throw new ArgumentNullException("methodInfoElements");

            return methodInfoElements.Aggregate((IReflectionVisitor<T>)this, (v, m) => v.Visit(m));
        }

        /// <summary>
        /// Allows <see cref="EventInfoElement" /> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="eventInfoElements">
        /// The <see cref="EventInfoElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation relaies each <see cref="EventInfoElement"/> instance
        /// to <see cref="Visit(EventInfoElement)"/>,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
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
        /// This implementation relaies the <see cref="ParameterInfoElement"/>
        /// and <see cref="LocalVariableInfoElement"/> instances
        /// from the <see cref="ConstructorInfoElement"/> parameter
        /// to <see cref="Visit(ParameterInfoElement[])"/> and <see cref="Visit(LocalVariableInfoElement[])"/> ,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(
            ConstructorInfoElement constructorInfoElement)
        {
            if (constructorInfoElement == null)
                throw new ArgumentNullException("constructorInfoElement");

            return Visit(constructorInfoElement.GetParameterInfoElements())
                .Visit(constructorInfoElement.GetLocalVariableInfoElements());
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
        /// This implementation relaies the two <see cref="MethodInfoElement"/> instances
        /// from the getter and setter of the <see cref="PropertyInfoElement"/> parameter,
        /// to <see cref="Visit(MethodInfoElement)"/>,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(
            PropertyInfoElement propertyInfoElement)
        {
            if (propertyInfoElement == null)
                throw new ArgumentNullException("propertyInfoElement");

            var getMethodInfoElement = propertyInfoElement.GetGetMethodInfoElement();
            var setMethodInfoElement = propertyInfoElement.GetSetMethodInfoElement();

            IReflectionVisitor<T> visitor = this;
            if (getMethodInfoElement != null)
                visitor = visitor.Visit(getMethodInfoElement);

            if (setMethodInfoElement != null)
                visitor = visitor.Visit(setMethodInfoElement);

            return visitor;
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
        /// This implementation relaies the <see cref="ParameterInfoElement"/>
        /// and <see cref="LocalVariableInfoElement"/> instances
        /// from the <see cref="MethodInfoElement"/> parameter
        /// to <see cref="Visit(ParameterInfoElement[])"/> and <see cref="Visit(LocalVariableInfoElement[])"/> ,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(
            MethodInfoElement methodInfoElement)
        {
            if (methodInfoElement == null)
                throw new ArgumentNullException("methodInfoElement");

            return Visit(methodInfoElement.GetParameterInfoElements())
                .Visit(methodInfoElement.GetLocalVariableInfoElements());
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

        /// <summary>
        /// Allows <see cref="ParameterInfoElement"/> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="parameterInfoElements">
        /// The <see cref="ParameterInfoElement"/> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation relaies each <see cref="ParameterInfoElement"/> instance
        /// to <see cref="Visit(ParameterInfoElement)"/>,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(params ParameterInfoElement[] parameterInfoElements)
        {
            if (parameterInfoElements == null)
                throw new ArgumentNullException("parameterInfoElements");

            return parameterInfoElements.Aggregate((IReflectionVisitor<T>)this, (v, pi) => v.Visit(pi));
        }

        /// <summary>
        /// Allows <see cref="LocalVariableInfoElement"/> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="localVariableInfoElements">
        /// The <see cref="LocalVariableInfoElement"/> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This implementation relaies each <see cref="LocalVariableInfoElement"/> instance
        /// to <see cref="Visit(LocalVariableInfoElement)"/>,
        /// but since the method is virtual, child classes can override it.
        /// </para>
        /// </remarks>
        public virtual IReflectionVisitor<T> Visit(params LocalVariableInfoElement[] localVariableInfoElements)
        {
            if (localVariableInfoElements == null)
                throw new ArgumentNullException("localVariableInfoElements");

            return localVariableInfoElements.Aggregate((IReflectionVisitor<T>)this, (v, l) => v.Visit(l));
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