using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo.Refraction
{
    public class CompositeReflectionElementRefraction<T> : 
        IReflectionElementRefraction<T>
    {
        public IEnumerable<IReflectionElement> Refract(IEnumerable<T> source)
        {
            throw new NotImplementedException();
        }
    }
}
