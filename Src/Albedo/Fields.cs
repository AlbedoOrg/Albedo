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