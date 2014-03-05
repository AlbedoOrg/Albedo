using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xunit;
using Ploeh.Albedo;
using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    public abstract class MethodsTests<T>
    {
        [Fact]
        public void SelectParameterLessReturnsCorrectMethod()
        {
            var sut = new Methods<ClassWithMethods>();
            
            MethodInfo actual = sut.Select(x => x.OmitParameters());

            var expected = typeof(ClassWithMethods).GetMethod("OmitParameters");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectParameterLessWithReturnValueReturnsCorrectMethod()
        {
            var sut = new Methods<ClassWithMethods>();

            MethodInfo actual = sut.Select(x => x.OmitParametersWithReturnValue());

            var expected = typeof(ClassWithMethods).GetMethod("OmitParametersWithReturnValue");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QueryParameterLessMethodUsingLinqSyntax()
        {
            var sut = new Methods<ClassWithMethods>();

            var actual = from x in sut select x.OmitParameters();

            var expected = typeof(ClassWithMethods).GetMethod("OmitParameters");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QueryParameterLessMethodWithReturnValueUsingLinqSyntax()
        {
            var sut = new Methods<ClassWithMethods>();

            var actual = from x in sut select x.OmitParametersWithReturnValue();

            var expected = typeof(ClassWithMethods).GetMethod("OmitParametersWithReturnValue");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectNullThrows()
        {
            var sut = new Methods<ClassWithMethods>();
            Assert.Throws<ArgumentNullException>(() => sut.Select(null));
        }

        [Fact]
        public void SelectNonMethodCallExpressionThrows()
        {
            var sut = new Methods<ClassWithMethods>();
            Assert.Throws<ArgumentException>(
                () => sut.Select(_ => 1 + 1));
        }

        [Fact]
        public void SelectNullFuncThrows()
        {
            var sut = new Methods<ClassWithMethods>();
            Assert.Throws<ArgumentNullException>(() =>
                sut.Select((Expression<Func<ClassWithMethods, object>>)null));
        }

        [Fact]
        public void SelectNonMethodCallExpressionWithoutReturnThrows()
        {
            var sut = new Methods<ClassWithMethods>();
            Expression<Action<ClassWithMethods>> nonMethodCallExpression =
                _ => new object();
            Assert.Throws<ArgumentException>(
                () => sut.Select(nonMethodCallExpression));
        }

        [Fact]
        public void SelectParameterLessGenericReturnsCorrectMethod()
        {
            var sut = new Methods<ClassWithMethods>();

            MethodInfo actual = sut.Select(x => x.OmitParametersGeneric<T>());

            var expected = 
                typeof(ClassWithMethods)
                    .GetMethod("OmitParametersGeneric")
                    .MakeGenericMethod(typeof(T));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectParameterLessGenericWithReturnValueReturnsCorrectMethod()
        {
            var sut = new Methods<ClassWithMethods>();

            MethodInfo actual = sut.Select(x => 
                x.OmitParametersGenericWithReturnValue<T>());

            var expected = 
                typeof(ClassWithMethods)
                    .GetMethod("OmitParametersGenericWithReturnValue")
                    .MakeGenericMethod(typeof(T));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QueryParameterLessGenericMethodUsingLinqSyntax()
        {
            var sut = new Methods<ClassWithMethods>();

            var actual = from x in sut select x.OmitParametersGeneric<T>();

            var expected =
                typeof(ClassWithMethods)
                    .GetMethod("OmitParametersGeneric")
                    .MakeGenericMethod(typeof(T));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QueryParameterLessGenericMethodWithReturnValueUsingLinqSyntax()
        {
            var sut = new Methods<ClassWithMethods>();

            var actual = from x in sut
                         select x.OmitParametersGenericWithReturnValue<T>();

            var expected =
                typeof(ClassWithMethods)
                    .GetMethod("OmitParametersGenericWithReturnValue")
                    .MakeGenericMethod(typeof(T));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectParameterLessGenericInGenericClassReturnsCorrectMethod()
        {
            var sut = new Methods<ClassWithMethods<T>>();

            MethodInfo actual = sut.Select(x => x.OmitParametersGeneric<T>());

            var expected =
                typeof(ClassWithMethods<T>)
                    .GetMethod("OmitParametersGeneric")
                    .MakeGenericMethod(typeof(T));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QueryParameterLessGenericMethodInGenericClassUsingLinqSyntax()
        {
            var sut = new Methods<ClassWithMethods<T>>();

            var actual = from x in sut select x.OmitParametersGeneric<T>();

            var expected =
                typeof(ClassWithMethods<T>)
                    .GetMethod("OmitParametersGeneric")
                    .MakeGenericMethod(typeof(T));
            Assert.Equal(expected, actual);
        }

        private class ClassWithMethods
        {
            public void OmitParameters()
            {
            }

            public object OmitParametersWithReturnValue()
            {
                return new object();
            }

            public void OmitParametersGeneric<U>()
            {
            }

            public object OmitParametersGenericWithReturnValue<U>()
            {
                return default(U);
            }
        }

        private class ClassWithMethods<V>
        {
            public void OmitParametersGeneric<U>()
            {
            }
        }
    }

    public class MethodTestsOfInt : MethodsTests<int> { }
    public class MethodTestsOfByte : MethodsTests<byte> { }
    public class MethodTestsOfFloat : MethodsTests<float> { }
}
