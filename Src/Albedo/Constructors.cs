using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Albedo
{
    /// <summary>
    /// Provides strongly-typed, refactoring-safe access to
    /// <see cref="ConstructorInfo" /> instances.
    /// </summary>
    /// <remarks>
    /// <para>
    /// While you can use the standard Reflection API (such as
    /// <see cref="Type.GetConstructor(Type[])" />) to obtain a
    /// <see cref="ConstructorInfo" /> instance, the problem is that it relies
    /// on identifying the constructor by parameter types. This isn't
    /// refactoring-safe, so if you change a constructor, your Reflection
    /// code may break. <strong>Constructors</strong> provides a
    /// strongly-typed, refactoring-safe alternative, utilizing LINQ expressions.
    /// </para>
    /// </remarks>
    /// <seealso cref="Select{T}(Expression{Func{T}})" />
    /// <seealso cref="Fields{T}" />
    /// <seealso cref="Properties{T}" />
    /// <seealso cref="Methods{T}" />
    public static class Constructors
    {
        /// <summary>
        /// Selects a <see cref="ConstructorInfo" /> instance based on a
        /// strongly-typed, refactoring-safe LINQ expression.
        /// </summary>
        /// <typeparam name="T">
        /// The type containing the desired constructor.
        /// </typeparam>
        /// <param name="constructorSelector">
        /// A LINQ expression that identifies the desired constructor.
        /// </param>
        /// <returns>
        /// A <see cref="ConstructorInfo" /> instance representing the
        /// constructor identified by <paramref name="constructorSelector" />.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The Select method provides a strongly-typed, refactoring-safe way
        /// to get a <see cref="ConstructorInfo" /> instance.
        /// </para>
        /// </remarks>
        /// <example>
        /// This example demonstrates how to use <see cref="Constructors" />
        /// with LINQ method syntax. The <strong>Select</strong>
        /// method returns a <see cref="ConstructorInfo" /> instance
        /// representing the <see cref="String(Char[])" /> constructor of
        /// <see cref="String" />.
        /// <code>
        /// ConstructorInfo ci = Constructors.Select(() => new string(new char[0]));
        /// </code>
        /// </example>
        /// <seealso cref="Constructors" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "The expression is strongly typed in order to prevent the caller from passing any sort of expression. It doesn't fully capture everything the caller might throw at it, but it does constrain the caller as well as possible. This enables the developer to get a compile-time exception instead of a run-time exception in most cases where an invalid expression is being supplied.")]
        public static ConstructorInfo Select<T>(Expression<Func<T>> constructorSelector)
        {
            if (constructorSelector == null)
            {
                throw new ArgumentNullException("constructorSelector");
            }

            var newExpression = constructorSelector.Body as NewExpression;
            if (newExpression == null)
            {
                throw new ArgumentException("The expression's body must be a NewExpression. The code block supplied should construct an new instance.\nExample: () => new Foo().", "constructorSelector");
            }

            return newExpression.Constructor;
        }
    }
}