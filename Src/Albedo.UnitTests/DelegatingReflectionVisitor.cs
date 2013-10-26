using System;

namespace Ploeh.Albedo.UnitTests
{
    internal class DelegatingReflectionVisitor<T> : IReflectionVisitor<T>
    {
        public T Value { get; set; }

        public DelegatingReflectionVisitor()
        {
            this.OnVisitAssemblyElement = e => this;
        }

        public Func<AssemblyElement, IReflectionVisitor<T>> OnVisitAssemblyElement { get; set; }

        public virtual IReflectionVisitor<T> Visit(AssemblyElement assemblyElement)
        {
            return OnVisitAssemblyElement(assemblyElement);
        }
    }
}