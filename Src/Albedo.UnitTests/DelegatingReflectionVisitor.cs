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
        }

        public Func<AssemblyElement, IReflectionVisitor<T>> OnVisitAssemblyElement { get; set; }

        public Func<ConstructorInfoElement, IReflectionVisitor<T>> OnVisitConstructorInfoElement { get; set; }

        public virtual IReflectionVisitor<T> Visit(AssemblyElement assemblyElement)
        {
            return OnVisitAssemblyElement(assemblyElement);
        }

        public virtual IReflectionVisitor<T> Visit(ConstructorInfoElement constructorInfoElement)
        {
            return OnVisitConstructorInfoElement(constructorInfoElement);
        }
    }
}
