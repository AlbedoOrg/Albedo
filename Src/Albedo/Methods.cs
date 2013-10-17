using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Ploeh.Albedo
{
    public class Methods<T>
    {
        public MethodInfo Select(
            Expression<Action<T>> methodSelector)
        {
            return ((MethodCallExpression)methodSelector.Body).Method;
        }
    }
}
