using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo
{
    public class ReflectionVisitor<T> : IReflectionVisitor<T>
    {
        public T Value
        {
            get { throw new NotImplementedException(); }
        }

        public IReflectionVisitor<T> Visit(AssemblyElement assemblyElement)
        {
            throw new NotImplementedException();
        }

        public IReflectionVisitor<T> Visit(ConstructorInfoElement constructorInfoElement)
        {
            throw new NotImplementedException();
        }

        public IReflectionVisitor<T> Visit(FieldInfoElement fieldInfoElement)
        {
            throw new NotImplementedException();
        }

        public IReflectionVisitor<T> Visit(MethodInfoElement methodInfoElement)
        {
            throw new NotImplementedException();
        }

        public IReflectionVisitor<T> Visit(ParameterInfoElement parameterInfoElement)
        {
            throw new NotImplementedException();
        }

        public IReflectionVisitor<T> Visit(PropertyInfoElement propertyInfoElement)
        {
            throw new NotImplementedException();
        }

        public IReflectionVisitor<T> Visit(TypeElement typeElement)
        {
            throw new NotImplementedException();
        }

        public IReflectionVisitor<T> Visit(LocalVariableInfoElement localVariableInfoElement)
        {
            throw new NotImplementedException();
        }

        public IReflectionVisitor<T> Visit(EventInfoElement eventInfoElement)
        {
            throw new NotImplementedException();
        }
    }
}
