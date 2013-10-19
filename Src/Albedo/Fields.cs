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

            return (FieldInfo)((MemberExpression)fieldSelector.Body).Member;
        }
    }
}