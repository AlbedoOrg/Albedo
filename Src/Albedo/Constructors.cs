using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Ploeh.Albedo
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
    /// strongly-typed alternative, utilizing LINQ expressions.
    /// </para>
    /// </remarks>
    /// <seealso cref="Select{T}(Expression{Func{T}})" />
    /// <seealso cref="Select{T}(Expression{Func{object,T}})" />
    /// <seealso cref="Fields{T}" />
    /// <seealso cref="Properties{T}" />
    /// <seealso cref="Methods{T}" />
    public class Constructors
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
        /// to get a <see cref="ConstructorInfo" /> instance. It supports both
        /// LINQ method syntax, as well as LINQ query syntax.
        /// </para>
        /// </remarks>
        /// <example>
        /// This example demonstrates how to use <see cref="Constructors" />
        /// with LINQ method syntax. The <strong>Select</strong>
        /// method returns a <see cref="ConstructorInfo" /> instance
        /// representing the <see cref="String(Char[])" /> constructor of
        /// <see cref="String" />.
        /// <code>
        /// ConstructorInfo ci = new Constructors().Select(() => new string(new char[0]));
        /// </code>
        /// This example demonstrates how to use <see cref="Constructors" />
        /// with LINQ query syntax. The <strong>Select</strong>
        /// method returns a <see cref="ConstructorInfo" /> instance
        /// representing the <see cref="String(Char[])" /> constructor of
        /// <see cref="String" />.
        /// <code>
        /// ConstructorInfo ci = from v in new Constructors()
        ///                      select new string(new char[0]);
        /// </code>
        /// </example>
        /// <seealso cref="Constructors" />
        /// <seealso cref="Select{T}(Expression{Func{object,T}})" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This non-static member is needed to keep consistency with the others.")]
        public static ConstructorInfo Select<T>(Expression<Func<T>> constructorSelector)
        {
            if (constructorSelector == null)
            {
                throw new ArgumentNullException("constructorSelector");
            }

            var newExpression = constructorSelector.Body as NewExpression;
            if (newExpression == null)
            {
                throw new ArgumentException(
                    "The expression's body must be a NewExpression. " +
                        "The code block supplied should construct an new instance.\n" +
                        "Example: () => new Foo().",
                    "constructorSelector");
            }

            return newExpression.Constructor;
        }
    }
}