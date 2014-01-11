using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ploeh.Albedo
{
    public static class ConcreteElementConversions
    {
        public static AssemblyElement ToElement(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            throw new NotImplementedException();
        }

        public static ConstructorInfoElement ToElement(this ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null) throw new ArgumentNullException("constructorInfo");
            throw new NotImplementedException();
        }

        public static EventInfoElement ToElement(this EventInfo eventInfo)
        {
            if (eventInfo == null) throw new ArgumentNullException("eventInfo");
            throw new NotImplementedException();
        }

        public static FieldInfoElement ToElement(this FieldInfo fieldInfo)
        {
            if (fieldInfo == null) throw new ArgumentNullException("fieldInfo");
            throw new NotImplementedException();
        }

        public static LocalVariableInfoElement ToElement(this LocalVariableInfo localVariableInfo)
        {
            if (localVariableInfo == null) throw new ArgumentNullException("localVariableInfo");
            throw new NotImplementedException();
        }

        public static MethodInfoElement ToElement(this MethodInfo methodInfo)
        {
            if (methodInfo == null) throw new ArgumentNullException("methodInfo");
            throw new NotImplementedException();
        }

        public static ParameterInfoElement ToElement(this ParameterInfo parameterInfo)
        {
            if (parameterInfo == null) throw new ArgumentNullException("parameterInfo");
            throw new NotImplementedException();
        }

        public static PropertyInfoElement ToElement(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null) throw new ArgumentNullException("propertyInfo");
            throw new NotImplementedException();
        }

        public static TypeElement ToElement(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            throw new NotImplementedException();
        }
    }
}
