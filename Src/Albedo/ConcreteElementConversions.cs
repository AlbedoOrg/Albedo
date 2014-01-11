using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ploeh.Albedo
{
    /// <summary>
    /// Provides extension methods which convert from System.Reflection instances to
    /// their Albedo element counterparts.
    /// </summary>
    public static class ConcreteElementConversions
    {
        /// <summary>
        /// Converts from an <see cref="Assembly"/> to an <see cref="AssemblyElement"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/></param>
        /// <returns>The <see cref="AssemblyElement"/></returns>
        public static AssemblyElement ToElement(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            return new AssemblyElement(assembly);
        }

        /// <summary>
        /// Converts from an <see cref="ConstructorInfo"/> to an <see cref="ConstructorInfoElement"/>.
        /// </summary>
        /// <param name="constructorInfo">The <see cref="ConstructorInfo"/></param>
        /// <returns>The <see cref="ConstructorInfoElement"/></returns>
        public static ConstructorInfoElement ToElement(this ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null) throw new ArgumentNullException("constructorInfo");
            return new ConstructorInfoElement(constructorInfo);
        }

        /// <summary>
        /// Converts from an <see cref="EventInfo"/> to an <see cref="EventInfoElement"/>.
        /// </summary>
        /// <param name="eventInfo">The <see cref="EventInfo"/></param>
        /// <returns>The <see cref="EventInfoElement"/></returns>
        public static EventInfoElement ToElement(this EventInfo eventInfo)
        {
            if (eventInfo == null) throw new ArgumentNullException("eventInfo");
            return new EventInfoElement(eventInfo);
        }

        /// <summary>
        /// Converts from an <see cref="FieldInfo"/> to an <see cref="FieldInfoElement"/>.
        /// </summary>
        /// <param name="fieldInfo">The <see cref="FieldInfo"/></param>
        /// <returns>The <see cref="FieldInfoElement"/></returns>
        public static FieldInfoElement ToElement(this FieldInfo fieldInfo)
        {
            if (fieldInfo == null) throw new ArgumentNullException("fieldInfo");
            return new FieldInfoElement(fieldInfo);
        }

        /// <summary>
        /// Converts from an <see cref="LocalVariableInfo"/> to an <see cref="LocalVariableInfoElement"/>.
        /// </summary>
        /// <param name="localVariableInfo">The <see cref="LocalVariableInfo"/></param>
        /// <returns>The <see cref="LocalVariableInfoElement"/></returns>
        public static LocalVariableInfoElement ToElement(this LocalVariableInfo localVariableInfo)
        {
            if (localVariableInfo == null) throw new ArgumentNullException("localVariableInfo");
            return new LocalVariableInfoElement(localVariableInfo);
        }

        /// <summary>
        /// Converts from an <see cref="MethodInfo"/> to an <see cref="MethodInfoElement"/>.
        /// </summary>
        /// <param name="methodInfo">The <see cref="MethodInfo"/></param>
        /// <returns>The <see cref="MethodInfoElement"/></returns>
        public static MethodInfoElement ToElement(this MethodInfo methodInfo)
        {
            if (methodInfo == null) throw new ArgumentNullException("methodInfo");
            return new MethodInfoElement(methodInfo);
        }

        /// <summary>
        /// Converts from an <see cref="ParameterInfo"/> to an <see cref="ParameterInfoElement"/>.
        /// </summary>
        /// <param name="parameterInfo">The <see cref="ParameterInfo"/></param>
        /// <returns>The <see cref="ParameterInfoElement"/></returns>
        public static ParameterInfoElement ToElement(this ParameterInfo parameterInfo)
        {
            if (parameterInfo == null) throw new ArgumentNullException("parameterInfo");
            return new ParameterInfoElement(parameterInfo);
        }

        /// <summary>
        /// Converts from an <see cref="PropertyInfo"/> to an <see cref="PropertyInfoElement"/>.
        /// </summary>
        /// <param name="propertyInfo">The <see cref="PropertyInfo"/></param>
        /// <returns>The <see cref="PropertyInfoElement"/></returns>
        public static PropertyInfoElement ToElement(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null) throw new ArgumentNullException("propertyInfo");
            return new PropertyInfoElement(propertyInfo);
        }

        /// <summary>
        /// Converts from an <see cref="Type"/> to an <see cref="TypeElement"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/></param>
        /// <returns>The <see cref="TypeElement"/></returns>
        public static TypeElement ToElement(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return new TypeElement(type);
        }
    }
}
