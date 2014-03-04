using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Ploeh.Albedo
{
    public class Constructors
    {
        public ConstructorInfo Select<T>(Expression<Func<T>> constructorSelector)
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
                    "The code block supplied should construct an new instance.\nExample: () => new Foo().",
                    "constructorSelector");
            }

            return newExpression.Constructor;
        }
    }
}