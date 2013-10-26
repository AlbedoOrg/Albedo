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
        }

        public Func<AssemblyElement, IReflectionVisitor<T>> OnVisitAssemblyElement { get; set; }

        public Func<ConstructorInfoElement, IReflectionVisitor<T>> OnVisitConstructorInfoElement { get; set; }
        public Func<FieldInfoElement, IReflectionVisitor<T>> OnVisitFieldInfoElement { get; set; }
        public Func<MethodInfoElement, IReflectionVisitor<T>> OnVisitMethodInfoElement { get; set; }

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
    }
}
