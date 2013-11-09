using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo.Refraction
{
    public class CompositeReflectionElementRefraction<T> : 
        IReflectionElementRefraction<T>, IEnumerable<IReflectionElementRefraction<T>>
    {
        private readonly IEnumerable<IReflectionElementRefraction<T>> refractions;

        public CompositeReflectionElementRefraction(
            params IReflectionElementRefraction<T>[] refractions)
        {
            this.refractions = refractions;
        }

        public IEnumerable<IReflectionElement> Refract(IEnumerable<T> source)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IReflectionElementRefraction<T>> GetEnumerator()
        {
            return this.refractions.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
