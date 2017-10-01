using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Albedo
{
    /// <summary>
    /// Provides strongly-typed, refactoring-safe access to
    /// <see cref="FieldInfo" /> instances.
    /// </summary>
    /// <typeparam name="T">The type containing the desired field.</typeparam>
    /// <remarks>
    /// <para>
    /// While you can use the standard Reflection API (such as
    /// <see cref="Type.GetField(string)" />) to obtain a
    /// <see cref="FieldInfo" /> instance, the problem is that it relies on
    /// identifying the field by name, using a string. This isn't refactoring-
    /// safe, so if you change the field name, your Reflection code may break.
    /// <strong>Fields&lt;T&gt;</strong> provides a strongly-typed alternative,
    /// utilizing LINQ expressions.
    /// </para>
    /// </remarks>
    /// <seealso cref="Select{TField}(Expression{Func{T, TField}})" />
    /// <seealso cref="Constructors"/>
    /// <seealso cref="Properties{T}" />
    /// <seealso cref="Methods{T}" />
    /// <seealso cref="Fields" />
    public class Fields<T>
    {
        /// <summary>
        /// Selects a <see cref="FieldInfo" /> instance based on a
        /// strongly-typed, refactoring-safe LINQ expression.
        /// </summary>
        /// <typeparam name="TField">The type of the desired field.</typeparam>
        /// <param name="fieldSelector">
        /// A LINQ expression that identifies the desired field.
        /// </param>
        /// <returns>
        /// A <see cref="FieldInfo" /> instance representing the field
        /// identified by <paramref name="fieldSelector" />.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The Select method provides a strongly-typed, refactoring-safe way
        /// to get a <see cref="FieldInfo" /> instance. It supports both
        /// LINQ method syntax, as well as LINQ query syntax.
        /// </para>
        /// </remarks>
        /// <example>
        /// This example demonstrates how to use <see cref="Fields{T}" /> with
        /// LINQ method syntax. <strong>ClassWithFields</strong>
        /// is a custom class with a public field called <strong>Text</strong>.
        /// The <strong>Select</strong> method returns a
        /// <strong>FieldInfo</strong> instance representing the
        /// <strong>Text</strong> field.
        /// <code>
        /// FieldInfo fi = new Fields&lt;ClassWithFields&gt;().Select(v => v.Text);
        /// </code>
        /// This example demonstrates how to use <see cref="Fields{T}" /> with
        /// LINQ query syntax. <strong>ClassWithFields</strong> is a custom class
        /// with a public field called <strong>Text</strong>. The
        /// <strong>Select</strong> method returns a <strong>FieldInfo</strong>
        /// instance representing the <strong>Text</strong> field.
        /// <code>
        /// FieldInfo fi = from v in new Fields&lt;ClassWithFields&gt;()
        ///                select v.Text;
        /// </code>
        /// </example>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="fieldSelector" /> is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The expression's body isn't a MemberExpression or it doesn't
        /// identify a field. The code block supplied should identify a field.
        /// Example: x => x.Bar.
        /// </exception>
        /// <seealso cref="Fields{T}" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "The expression is strongly typed in order to prevent the caller from passing any sort of expression. It doesn't fully capture everything the caller might throw at it, but it does constrain the caller as well as possible. This enables the developer to get a compile-time exception instead of a run-time exception in most cases where an invalid expression is being supplied.")]
        public FieldInfo Select<TField>(Expression<Func<T, TField>> fieldSelector)
        {
            if (fieldSelector == null)
                throw new ArgumentNullException("fieldSelector");

            var memberExp = fieldSelector.Body as MemberExpression;
            if (memberExp == null)
                throw new ArgumentException("The expression's body must be a MemberExpression. The code block supplied should identify a field.\nExample: x => x.Bar.", "fieldSelector");

            var fi = memberExp.Member as FieldInfo;
            if (fi == null)
                throw new ArgumentException("The expression's body must identify a field, not a property or other member.", "fieldSelector");

            return fi.ReflectedType == typeof(T) ? fi : typeof(T).GetField(fi.Name);
        }
    }

    /// <summary>
    /// Provides strongly-typed, refactoring-safe access to
    /// <see cref="FieldInfo" /> instances.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class provides similar services to <see cref="Fields{T}" />, but
    /// for static fields.
    /// </para>
    /// </remarks>
    public static class Fields
    {
        /// <summary>
        /// Selects a <see cref="FieldInfo" /> instance based on a
        /// strongly-typed, refactoring-safe LINQ expression.
        /// </summary>
        /// <typeparam name="TField">The type of the desired field.</typeparam>
        /// <param name="fieldSelector">
        /// A LINQ expression that identifies the desired field.
        /// </param>
        /// <returns>
        /// A <see cref="FieldInfo" /> instance representing the field
        /// identified by <paramref name="fieldSelector" />.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="fieldSelector" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The expression's body must be a MemberExpression. The code block supplied should identify a field.\nExample: () => Foo.Bar.;fieldSelector
        /// or
        /// The expression's body must identify a field, not a property or other member.;fieldSelector
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "The expression is strongly typed in order to prevent the caller from passing any sort of expression. It doesn't fully capture everything the caller might throw at it, but it does constrain the caller as well as possible. This enables the developer to get a compile-time exception instead of a run-time exception in most cases where an invalid expression is being supplied.")]
        public static FieldInfo Select<TField>(Expression<Func<TField>> fieldSelector)
        {
            if (fieldSelector == null)
                throw new ArgumentNullException("fieldSelector");

            var memberExp = fieldSelector.Body as MemberExpression;
            if (memberExp == null)
                throw new ArgumentException("The expression's body must be a MemberExpression. The code block supplied should identify a field.\nExample: () => Foo.Bar.", "fieldSelector");

            var fi = memberExp.Member as FieldInfo;
            if (fi == null)
                throw new ArgumentException("The expression's body must identify a field, not a property or other member.", "fieldSelector");

            return fi;
        }
    }
}