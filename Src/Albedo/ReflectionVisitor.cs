using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo
{
    public abstract class ReflectionVisitor<T> : IReflectionVisitor<T>
    {
        public abstract T Value { get; }

        public virtual IReflectionVisitor<T> Visit(
            AssemblyElement assemblyElement)
        {
            return this;
        }

        public virtual IReflectionVisitor<T> Visit(ConstructorInfoElement constructorInfoElement)
        {
            throw new NotImplementedException();
        }

        public virtual IReflectionVisitor<T> Visit(FieldInfoElement fieldInfoElement)
        {
            throw new NotImplementedException();
        }

        public virtual IReflectionVisitor<T> Visit(MethodInfoElement methodInfoElement)
        {
            throw new NotImplementedException();
        }

        public virtual IReflectionVisitor<T> Visit(ParameterInfoElement parameterInfoElement)
        {
            throw new NotImplementedException();
        }

        public virtual IReflectionVisitor<T> Visit(PropertyInfoElement propertyInfoElement)
        {
            throw new NotImplementedException();
        }

        public virtual IReflectionVisitor<T> Visit(TypeElement typeElement)
        {
            throw new NotImplementedException();
        }

        public virtual IReflectionVisitor<T> Visit(LocalVariableInfoElement localVariableInfoElement)
        {
            throw new NotImplementedException();
        }

        public virtual IReflectionVisitor<T> Visit(EventInfoElement eventInfoElement)
        {
            throw new NotImplementedException();
        }
    }
}
