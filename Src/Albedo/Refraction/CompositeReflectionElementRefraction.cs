using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo.Refraction
{
    public class CompositeReflectionElementRefraction<T> : 
        IReflectionElementRefraction<T>, IEnumerable<IReflectionElementRefraction<T>>
    {
        public IEnumerable<IReflectionElement> Refract(IEnumerable<T> source)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IReflectionElementRefraction<T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
