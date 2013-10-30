using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo
{
    public class CompositeReflectionElement : IReflectionElement, IEnumerable<IReflectionElement>
    {
        private readonly IEnumerable<IReflectionElement> elements;

        public CompositeReflectionElement(params IReflectionElement[] elements)
        {
            this.elements = elements;
        }

        public IReflectionVisitor<T> Accept<T>(IReflectionVisitor<T> visitor)
        {
            return this.elements.Aggregate(visitor, (v, e) => e.Accept(v));
        }

        public IEnumerator<IReflectionElement> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}