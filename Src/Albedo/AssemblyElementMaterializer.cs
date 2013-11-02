using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ploeh.Albedo
{
    public class AssemblyElementMaterializer<T> : IReflectionElementMaterializer<T>
    {
        public IEnumerable<IReflectionElement> Materialize(IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return this.Materialazy(source);
        }

        private IEnumerable<IReflectionElement> Materialazy(IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                var element = item as IReflectionElement;
                if (element != null)
                    yield return element;

                var a = item as Assembly;
                if (a != null)
                    yield return new AssemblyElement(a);
            }
        }
    }
}
