using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ploeh.Albedo
{
    public class ConstructorInfoElementMaterializer<T> : IReflectionElementMaterializer<T>
    {
        public IEnumerable<IReflectionElement> Materialize(IEnumerable<T> source)
        {
            return source
                .OfType<ConstructorInfo>()
                .Select(ci => new ConstructorInfoElement(ci))
                .Cast<IReflectionElement>();
        }
    }
}
