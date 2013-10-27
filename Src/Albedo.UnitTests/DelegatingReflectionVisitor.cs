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
        }

        public Func<AssemblyElement, IReflectionVisitor<T>> OnVisitAssemblyElement { get; set; }
        public Func<ConstructorInfoElement, IReflectionVisitor<T>> OnVisitConstructorInfoElement { get; set; }
        public Func<FieldInfoElement, IReflectionVisitor<T>> OnVisitFieldInfoElement { get; set; }
        public Func<MethodInfoElement, IReflectionVisitor<T>> OnVisitMethodInfoElement { get; set; }
        public Func<ParameterInfoElement, IReflectionVisitor<T>> OnVisitParameterInfoElement { get; set; }
        public Func<PropertyInfoElement, IReflectionVisitor<T>> OnVisitPropertyInfoElement { get; set; }
        public Func<TypeElement, IReflectionVisitor<T>> OnVisitTypeElement { get; set; }

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
    }
}
