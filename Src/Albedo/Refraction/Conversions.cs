using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo.Refraction
{
    /// <summary>
    /// Contains conversion methods to turn objects into
    /// <see cref="IReflectionElement" /> instances.
    /// </summary>
    public static class Conversions
    {
        /// <summary>
        /// Converts an object to an <see cref="IReflectionElement" />
        /// instance. This method is guaranteed to always return a proper
        /// instance.
        /// </summary>
        /// <typeparam name="T">The type of the object to convert.</typeparam>
        /// <param name="source">The object to convert.</param>
        /// <returns>
        /// An <see cref="IReflectionElement" /> instance that adapts
        /// <paramref name="source" />, if possibly; otherwise, a
        /// <see cref="NullReflectionElement" />.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method is guaranteed to return an
        /// <see cref="IReflectionElement" /> instance. However, if
        /// <paramref name="source" /> can't properly be adapted, a
        /// <see cref="NullReflectionElement" /> is returned.
        /// </para>
        /// <para>
        /// For a stricter converion, you can use
        /// <see cref="ToReflectionElement{T}(T)" />.
        /// </para>
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source" /> is null.
        /// </exception>
        /// <seealso cref="ToReflectionElement{T}(T)" />
        public static IReflectionElement AsReflectionElement<T>(this T source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

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

        /// <summary>
        /// Converts an object to an <see cref="IReflectionElement" />
        /// instance. This method is guaranteed to handle only proper
        /// Reflection objects.
        /// </summary>
        /// <typeparam name="T">The type of the object to convert.</typeparam>
        /// <param name="source">The object to convert.</param>
        /// <returns>
        /// An <see cref="IReflectionElement" /> instance that adapts
        /// <paramref name="source" />, if possibly; otherwise, an
        /// <see cref="ArgumentException"/> is thrown.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method is guaranteed to only convert proper Reflection
        /// objects, such as <see cref="System.Reflection.ParameterInfo" />,
        /// <see cref="System.Reflection.PropertyInfo" />, <see cref="Type" />,
        /// <see cref="System.Reflection.Assembly" />, etc. If
        /// <paramref name="source" /> isn't such a proper Reflection object,
        /// an <see cref="ArgumentException" /> is thrown.
        /// </para>
        /// <para>
        /// For a weaker, but more robust converion, you can use
        /// <see cref="AsReflectionElement{T}(T)" />.
        /// </para>
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="source" /> isn't a proper Reflection object.
        /// </exception>
        /// <seealso cref="AsReflectionElement{T}(T)" />
        public static IReflectionElement ToReflectionElement<T>(this T source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var re = new CompositeReflectionElementRefraction<T>(
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
                .SingleOrDefault();

            if (re == null)
                throw new ArgumentException("The supplied conversion candidate is not a Reflection object. Only Reflection objects, like Type, PropertyInfo, ParameterInfo, etc. are supported by the ToReflectionElement method. If you need a weaker, but more robust conversion, please use the AsReflectionElement method.", "source");

            return re;
        }
    }
}
