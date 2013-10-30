using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo
{
    public class CompositeReflectionElement : IReflectionElement
    {
        public IReflectionVisitor<T> Accept<T>(IReflectionVisitor<T> visitor)
        {
            throw new NotImplementedException();
        }
    }
}