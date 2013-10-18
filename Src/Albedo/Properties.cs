using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Ploeh.Albedo
{
    public class Properties<T>
    {
        public PropertyInfo Select<TProperty>(Expression<Func<T, TProperty>> propertySelector)
        {
            return (PropertyInfo)((MemberExpression)propertySelector.Body).Member;
        }
    }
}
