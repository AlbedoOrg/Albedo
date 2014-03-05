using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Ploeh.Albedo
{
    /// <summary>
    /// Provides strongly-typed, refactoring-safe access to
    /// <see cref="MethodInfo" /> instances.
    /// </summary>
    /// <typeparam name="T">
    /// The type containing the desired method.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// While you can use the standard Reflection API (such as
    /// <see cref="Type.GetMethod(string)" />) to obtain a
    /// <see cref="MethodInfo" /> instance, the problem is that it relies on
    /// identifying the method by name, using a string. This isn't
    /// refactoring-safe, so if you change the method name, your Reflection
    /// code may break. <strong>Methods&lt;T&gt;</strong> provides a
    /// strongly-typed alternative, utilizing LINQ expressions.
    /// </para>
    /// </remarks>
    /// <seealso cref="Select(Expression{Action{T}})" />
    /// <seealso cref="Fields{T}" />
    /// <seealso cref="Constructors" />
    /// <seealso cref="Properties{T}" />
    public class Methods<T>
    {
        /// <summary>
        /// Selects a <see cref="MethodInfo" /> instance based on a
        /// strongly-typed, refactoring-safe LINQ expression.
        /// </summary>
        /// <param name="methodSelector">
        /// A LINQ expression that identifies the desired method.
        /// </param>
        /// <returns>
        /// A <see cref="MethodInfo" /> instance representing the method
        /// identified by <paramref name="methodSelector" />.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The Select method provides a strongly-typed, refactoring-safe way
        /// to get a <see cref="MethodInfo" /> instance. It supports both
        /// LINQ method syntax, as well as LINQ query syntax.
        /// </para>
        /// </remarks>
        /// <example>
        /// This example demonstrates how to use <see cref="Methods{T}" />
        /// with LINQ method syntax. The <strong>Select</strong>
        /// method returns a <see cref="MethodInfo" /> instance representing
        /// the <see cref="Version.ToString()" /> method of
        /// <see cref="Version" />.
        /// <code>
        /// MethodInfo mi = new Methods&lt;Version&gt;().Select(v => v.ToString());
        /// </code>
        /// This example demonstrates how to use <see cref="Methods{T}" />
        /// with LINQ query syntax. The <strong>Select</strong>
        /// method returns a <strong>MethodInfo</strong> instance
        /// representing the <strong>ToString()</strong> method of
        /// <strong>Version</strong>.
        /// <code>
        /// MethodInfo mi = from v in new Methods&lt;Version&gt;()
        ///                 select v.ToString();
        /// </code>
        /// </example>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="methodSelector" /> is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The expression's body isn't a <see cref="MethodCallExpression"/> or
        /// it doesn't identify a method. The code block supplied should
        /// identify a method.
        /// Example: x => x.Foo().
        /// </exception>
        /// <seealso cref="Methods{T}" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "The expression is strongly typed in order to prevent the caller from passing any sort of expression. It doesn't fully capture everything the caller might throw at it, but it does constrain the caller as well as possible. This enables the developer to get a compile-time exception instead of a run-time exception in most cases where an invalid expression is being supplied.")]
        public MethodInfo Select(
            Expression<Action<T>> methodSelector)
        {
            if (methodSelector == null)
                throw new ArgumentNullException("methodSelector");

            var methodCallExp = methodSelector.Body as MethodCallExpression;
            if (methodCallExp == null)
                throw new ArgumentException("The expression's body must be a MethodCallExpression. The code block supplied should invoke a method.\nExample: x => x.Foo().", "methodSelector");

            return methodCallExp.Method;
        }

        /// <summary>
        /// Selects a <see cref="MethodInfo" /> instance based on a
        /// strongly-typed, refactoring-safe LINQ expression.
        /// </summary>
        /// <param name="methodSelector">
        /// A LINQ expression that identifies the desired method.
        /// </param>
        /// <returns>
        /// A <see cref="MethodInfo" /> instance representing the method
        /// identified by <paramref name="methodSelector" />.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The Select method provides a strongly-typed, refactoring-safe way
        /// to get a <see cref="MethodInfo" /> instance. It supports both
        /// LINQ method syntax, as well as LINQ query syntax.
        /// </para>
        /// </remarks>
        /// <example>
        /// This example demonstrates how to use <see cref="Methods{T}" />
        /// with LINQ method syntax. The <strong>Select</strong>
        /// method returns a <see cref="MethodInfo" /> instance representing
        /// the <see cref="Version.ToString()" /> method of
        /// <see cref="Version" />.
        /// <code>
        /// MethodInfo mi = new Methods&lt;Version&gt;().Select(v => v.ToString());
        /// </code>
        /// This example demonstrates how to use <see cref="Methods{T}" />
        /// with LINQ query syntax. The <strong>Select</strong>
        /// method returns a <strong>MethodInfo</strong> instance
        /// representing the <strong>ToString()</strong> method of
        /// <strong>Version</strong>.
        /// <code>
        /// MethodInfo mi = from v in new Methods&lt;Version&gt;()
        ///                 select v.ToString();
        /// </code>
        /// </example>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="methodSelector" /> is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The expression's body isn't a <see cref="MethodCallExpression"/> or
        /// it doesn't identify a method. The code block supplied should
        /// identify a method.
        /// Example: x => x.Foo().
        /// </exception>
        /// <seealso cref="Methods{T}" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "The expression is strongly typed in order to prevent the caller from passing any sort of expression. It doesn't fully capture everything the caller might throw at it, but it does constrain the caller as well as possible. This enables the developer to get a compile-time exception instead of a run-time exception in most cases where an invalid expression is being supplied.")]
        public MethodInfo Select<TResult>(
            Expression<Func<T, TResult>> methodSelector)
        {
            if (methodSelector == null)
                throw new ArgumentNullException("methodSelector");

            var methodCallExp = methodSelector.Body as MethodCallExpression;
            if (methodCallExp == null)
                throw new ArgumentException("The expression's body must be a MethodCallExpression. The code block supplied should invoke a method.\nExample: x => x.Foo().", "methodSelector");

            return methodCallExp.Method;
        }
    }
}
