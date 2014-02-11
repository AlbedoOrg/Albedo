using System;

namespace Ploeh.Albedo.UnitTests
{
    internal class DelegatingReflectionVisitor<T> : IReflectionVisitor<T>
    {
        public T Value { get; set; }

        public DelegatingReflectionVisitor()
        {
            this.OnVisitAssemblyElement = e => this;
            this.OnVisitConstructorInfoElement = e => this;
            this.OnVisitFieldInfoElement = e => this;
            this.OnVisitMethodInfoElement = e => this;
            this.OnVisitParameterInfoElement = e => this;
            this.OnVisitPropertyInfoElement = e => this;
            this.OnVisitTypeElement = e => this;
            this.OnVisitLocalVariableInfoElement = e => this;
            this.OnVisitEventInfoElement = e => this;
        }

        public Func<AssemblyElement, IReflectionVisitor<T>> OnVisitAssemblyElement { get; set; }
        public Func<ConstructorInfoElement, IReflectionVisitor<T>> OnVisitConstructorInfoElement { get; set; }
        public Func<FieldInfoElement, IReflectionVisitor<T>> OnVisitFieldInfoElement { get; set; }
        public Func<MethodInfoElement, IReflectionVisitor<T>> OnVisitMethodInfoElement { get; set; }
        public Func<ParameterInfoElement, IReflectionVisitor<T>> OnVisitParameterInfoElement { get; set; }
        public Func<PropertyInfoElement, IReflectionVisitor<T>> OnVisitPropertyInfoElement { get; set; }
        public Func<TypeElement, IReflectionVisitor<T>> OnVisitTypeElement { get; set; }
        public Func<LocalVariableInfoElement, IReflectionVisitor<T>> OnVisitLocalVariableInfoElement { get; set; }
        public Func<EventInfoElement, IReflectionVisitor<T>> OnVisitEventInfoElement { get; set; }

        public virtual IReflectionVisitor<T> Visit(AssemblyElement assemblyElement)
        {
            return OnVisitAssemblyElement(assemblyElement);
        }

        public virtual IReflectionVisitor<T> Visit(ConstructorInfoElement constructorInfoElement)
        {
            return OnVisitConstructorInfoElement(constructorInfoElement);
        }

        public virtual IReflectionVisitor<T> Visit(FieldInfoElement fieldInfoElement)
        {
            return OnVisitFieldInfoElement(fieldInfoElement);
        }

        public virtual IReflectionVisitor<T> Visit(MethodInfoElement methodInfoElement)
        {
            return OnVisitMethodInfoElement(methodInfoElement);
        }

        public virtual IReflectionVisitor<T> Visit(ParameterInfoElement parameterInfoElement)
        {
            return OnVisitParameterInfoElement(parameterInfoElement);
        }

        public virtual IReflectionVisitor<T> Visit(PropertyInfoElement propertyInfoElement)
        {
            return OnVisitPropertyInfoElement(propertyInfoElement);
        }

        public virtual IReflectionVisitor<T> Visit(TypeElement typeElement)
        {
            return OnVisitTypeElement(typeElement);
        }

        public virtual IReflectionVisitor<T> Visit(LocalVariableInfoElement localVariableInfoElement)
        {
            return OnVisitLocalVariableInfoElement(localVariableInfoElement);
        }

        public virtual IReflectionVisitor<T> Visit(EventInfoElement eventInfoElement)
        {
            return OnVisitEventInfoElement(eventInfoElement);
        }

        public IReflectionVisitor<T> Visit(params TypeElement[] typeElements)
        {
            throw new NotSupportedException();
        }

        public IReflectionVisitor<T> Visit(params FieldInfoElement[] fieldInfoElements)
        {
            throw new NotSupportedException();
        }

        public IReflectionVisitor<T> Visit(params ConstructorInfoElement[] constructorInfoElements)
        {
            throw new NotSupportedException();
        }

        public IReflectionVisitor<T> Visit(params PropertyInfoElement[] propertyInfoElements)
        {
            throw new NotSupportedException();
        }

        public IReflectionVisitor<T> Visit(params MethodInfoElement[] methodInfoElements)
        {
            throw new NotSupportedException();
        }

        public IReflectionVisitor<T> Visit(params EventInfoElement[] eventInfoElements)
        {
            throw new NotSupportedException();
        }

        public IReflectionVisitor<T> Visit(params LocalVariableInfoElement[] localVariableInfoElements)
        {
            throw new NotSupportedException();
        }
    }
}
