using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Ploeh.Albedo
{
    public class Constructors
    {
        public ConstructorInfo Select<T>(Expression<Func<T>> constructorSelector)
        {
            return SelectImpl(constructorSelector);
        }

        public ConstructorInfo Select<T>(Expression<Func<object, T>> constructorSelector)
        {
            return SelectImpl(constructorSelector);
        }

        private static ConstructorInfo SelectImpl<TFunc>(Expression<TFunc> constructorSelector)
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