using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Albedo
{
    /// <summary>
    /// Provides strongly-typed, refactoring-safe access to
    /// <see cref="PropertyInfo" /> instances.
    /// </summary>
    /// <typeparam name="T">
    /// The type containing the desired property.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// While you can use the standard Reflection API (such as
    /// <see cref="Type.GetProperty(string)" />) to obtain a
    /// <see cref="PropertyInfo" /> instance, the problem is that it relies on
    /// identifying the property by name, using a string. This isn't
    /// refactoring-safe, so if you change the property name, your Reflection
    /// code may break. <strong>Properties&lt;T&gt;</strong> provides a
    /// strongly-typed alternative, utilizing LINQ expressions.
    /// </para>
    /// </remarks>
    /// <seealso cref="Select{TProperty}(Expression{Func{T, TProperty}})" />
    /// <seealso cref="Fields{T}" />
    /// <seealso cref="Constructors" />
    /// <seealso cref="Methods{T}" />
    public class Properties<T>
    {
        /// <summary>
        /// Selects a <see cref="PropertyInfo" /> instance based on a
        /// strongly-typed, refactoring-safe LINQ expression.
        /// </summary>
        /// <typeparam name="TProperty">
        /// The type of the desired property.
        /// </typeparam>
        /// <param name="propertySelector">
        /// A LINQ expression that identifies the desired field.
        /// </param>
        /// <returns>
        /// A <see cref="PropertyInfo" /> instance representing the property
        /// identified by <paramref name="propertySelector" />.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The Select method provides a strongly-typed, refactoring-safe way
        /// to get a <see cref="PropertyInfo" /> instance. It supports both
        /// LINQ method syntax, as well as LINQ query syntax.
        /// </para>
        /// </remarks>
        /// <example>
        /// This example demonstrates how to use <see cref="Properties{T}" />
        /// with LINQ method syntax. The <strong>Select</strong>
        /// method returns a <see cref="PropertyInfo" /> instance representing
        /// the <see cref="Version.Major" /> property of
        /// <see cref="Version" />.
        /// <code>
        /// PropertyInfo pi = new Properties&lt;Version&gt;().Select(v => v.Major);
        /// </code>
        /// This example demonstrates how to use <see cref="Properties{T}" />
        /// with LINQ query syntax. The <strong>Select</strong>
        /// method returns a <strong>PropertyInfo</strong> instance
        /// representing the <strong>Major</strong> property of
        /// <strong>Version</strong>.
        /// <code>
        /// PropertyInfo pi = from v in new Properties&lt;Version&gt;()
        ///                   select v.Major;
        /// </code>
        /// </example>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="propertySelector" /> is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The expression's body isn't a MemberExpression or it doesn't
        /// identify a property. The code block supplied should identify a
        /// property.
        /// Example: x => x.Bar.
        /// </exception>
        /// <seealso cref="Properties{T}" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "The expression is strongly typed in order to prevent the caller from passing any sort of expression. It doesn't fully capture everything the caller might throw at it, but it does constrain the caller as well as possible. This enables the developer to get a compile-time exception instead of a run-time exception in most cases where an invalid expression is being supplied.")]
        public PropertyInfo Select<TProperty>(Expression<Func<T, TProperty>> propertySelector)
        {
           if (propertySelector == null)
               throw new ArgumentNullException("propertySelector");

           var memberExp = propertySelector.Body as MemberExpression;
           if (memberExp == null)
                throw new ArgumentException("The expression's body must be a MemberExpression. The code block supplied should identify a property.\nExample: x => x.Bar.", "propertySelector");
 
           var pi = memberExp.Member as PropertyInfo;
           if (pi == null)
                throw new ArgumentException("The expression's body must identify a property, not a field or other member.", "propertySelector");

           return pi.ReflectedType == typeof(T) ? pi : typeof(T).GetProperty(pi.Name);
        }
    }

    /// <summary>
    /// Provides strongly-typed, refactoring-safe access to
    /// <see cref="PropertyInfo" /> instances.
    /// </summary>
    /// <remarks>
    /// <para>
    /// While you can use the standard Reflection API (such as
    /// <see cref="Type.GetProperty(string)" />) to obtain a
    /// <see cref="PropertyInfo" /> instance, the problem is that it relies on
    /// identifying the property by name, using a string. This isn't
    /// refactoring-safe, so if you change the property name, your Reflection
    /// code may break. <strong>Properties</strong> provides a
    /// strongly-typed alternative, utilizing LINQ expressions.
    /// </para>
    /// </remarks>
    /// <seealso cref="Fields" />
    /// <seealso cref="Constructors" />
    /// <seealso cref="Methods" />
    public static class Properties
    {
        /// <summary>
        /// Selects a <see cref="PropertyInfo" /> instance based on a
        /// strongly-typed, refactoring-safe LINQ expression.
        /// </summary>
        /// <param name="propertySelector">
        /// A LINQ expression that identifies the desired field.
        /// </param>
        /// <returns>
        /// A <see cref="PropertyInfo" /> instance representing the property
        /// identified by <paramref name="propertySelector" />.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The Select method provides a strongly-typed, refactoring-safe way
        /// to get a <see cref="PropertyInfo" /> instance. It supports
        /// LINQ method syntax.
        /// </para>
        /// </remarks>
        /// <example>
        /// This example demonstrates how to use <see cref="Properties" />
        /// with LINQ method syntax. The <strong>Select</strong>
        /// method returns a <see cref="PropertyInfo" /> instance representing
        /// the <see cref="Console.BackgroundColor" /> property of
        /// <see cref="Console" />.
        /// <code>
        /// PropertyInfo pi = new Properties.Select(() => Console.BackgroundColor);
        /// </code>
        /// </example>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="propertySelector" /> is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The expression's body isn't a MemberExpression or it doesn't
        /// identify a property. The code block supplied should identify a
        /// property.
        /// Example: () => Foo.Bar.
        /// </exception>
        /// <seealso cref="Properties{T}" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "The expression is strongly typed in order to prevent the caller from passing any sort of expression. It doesn't fully capture everything the caller might throw at it, but it does constrain the caller as well as possible. This enables the developer to get a compile-time exception instead of a run-time exception in most cases where an invalid expression is being supplied.")]
        public static PropertyInfo Select<TProperty>(Expression<Func<TProperty>> propertySelector)
        {
             if (propertySelector == null)
                throw new ArgumentNullException("propertySelector");

            var memberExp = propertySelector.Body as MemberExpression;
            if (memberExp == null)
                throw new ArgumentException("The expression's body must be a MemberExpression. The code block supplied should identify a property.\nExample: () => Foo.Bar.", "propertySelector");

            var pi = memberExp.Member as PropertyInfo;
            if (pi == null)
                throw new ArgumentException("The expression's body must identify a property, not a field or other member.", "propertySelector");

            return pi;
        }
    }
}
