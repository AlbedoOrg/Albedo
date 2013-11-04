﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ploeh.Albedo
{
    public class MethodInfoElementMaterializer<T> : IReflectionElementMaterializer<T>
    {
        public IEnumerable<IReflectionElement> Materialize(IEnumerable<T> source)
        {
            return source
                .OfType<MethodInfo>()
                .Select(mi => new MethodInfoElement(mi))
                .Cast<IReflectionElement>();
        }
    }
}