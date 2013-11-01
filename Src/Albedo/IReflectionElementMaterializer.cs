using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo
{
    public interface IReflectionElementMaterializer<T>
    {
        IEnumerable<IReflectionElement> Materialize(IEnumerable<T> source);
    }
}
