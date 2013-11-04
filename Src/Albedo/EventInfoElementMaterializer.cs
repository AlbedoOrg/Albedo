using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ploeh.Albedo
{
    public class EventInfoElementMaterializer<T> : IReflectionElementMaterializer<T>
    {
        public IEnumerable<IReflectionElement> Materialize(IEnumerable<T> source)
        {
            return source
                .OfType<EventInfo>()
                .Select(ei => new EventInfoElement(ei))
                .Cast<IReflectionElement>();
        }
    }
}
