using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo.Refraction
{
    public static class Conversions
    {
        public static IReflectionElement AsReflectionElement<T>(this T source)
        {
            return new CompositeReflectionElementRefraction<T>(
                new AssemblyElementRefraction<T>(),
                new ConstructorInfoElementRefraction<T>(),
                new EventInfoElementRefraction<T>(),
                new FieldInfoElementRefraction<T>(),
                new LocalVariableInfoElementRefraction<T>(),
                new MethodInfoElementRefraction<T>(),
                new ParameterInfoElementRefraction<T>(),
                new PropertyInfoElementRefraction<T>(),
                new TypeElementRefraction<T>(),
                new ReflectionElementRefraction<T>())
                .Refract(new[] { source })
                .DefaultIfEmpty(new NullReflectionElement())
                .Single();
        }
    }
}
