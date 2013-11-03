using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo
{
    public class ConstructorInfoElementMaterializer<T> : IReflectionElementMaterializer<T>
    {
        public IEnumerable<IReflectionElement> Materialize(IEnumerable<T> source)
        {
            throw new NotImplementedException();
        }
    }
}
