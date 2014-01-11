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
            return new AssemblyElement(assembly);
        }

        public static ConstructorInfoElement ToElement(this ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null) throw new ArgumentNullException("constructorInfo");
            return new ConstructorInfoElement(constructorInfo);
        }

        public static EventInfoElement ToElement(this EventInfo eventInfo)
        {
            if (eventInfo == null) throw new ArgumentNullException("eventInfo");
            return new EventInfoElement(eventInfo);
        }

        public static FieldInfoElement ToElement(this FieldInfo fieldInfo)
        {
            if (fieldInfo == null) throw new ArgumentNullException("fieldInfo");
            return new FieldInfoElement(fieldInfo);
        }

        public static LocalVariableInfoElement ToElement(this LocalVariableInfo localVariableInfo)
        {
            if (localVariableInfo == null) throw new ArgumentNullException("localVariableInfo");
            return new LocalVariableInfoElement(localVariableInfo);
        }

        public static MethodInfoElement ToElement(this MethodInfo methodInfo)
        {
            if (methodInfo == null) throw new ArgumentNullException("methodInfo");
            return new MethodInfoElement(methodInfo);
        }

        public static ParameterInfoElement ToElement(this ParameterInfo parameterInfo)
        {
            if (parameterInfo == null) throw new ArgumentNullException("parameterInfo");
            return new ParameterInfoElement(parameterInfo);
        }

        public static PropertyInfoElement ToElement(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null) throw new ArgumentNullException("propertyInfo");
            return new PropertyInfoElement(propertyInfo);
        }

        public static TypeElement ToElement(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return new TypeElement(type);
        }
    }
}
