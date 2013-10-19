using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Ploeh.Albedo
{
    public class Fields<T>
    {
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

            return fi;
        }
    }
}