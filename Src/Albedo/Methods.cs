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
            if (methodSelector == null)
                throw new ArgumentNullException("methodSelector");

            var methodCallExp = methodSelector.Body as MethodCallExpression;
            if (methodCallExp == null)
                throw new ArgumentException("The expression's body must be a MethodCallExpression. The code block supplied should invoke a method.\nExample: x => x.Foo().", "methodSelector");

            return methodCallExp.Method;
        }
    }
}
