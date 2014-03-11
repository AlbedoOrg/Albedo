﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
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
        public void SelectParameterLessGenericMethodReturnsCorrectMethod()
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
        public void SelectParameterLessGenericMethodWithReturnValueReturnsCorrectMethod()
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
        public void SelectNonParameterLessGenericMethodReturnsCorrectMethod()
        {
            var sut = new Methods<ClassWithMethods>();
            var dummy = default(T);

            MethodInfo actual = sut.Select(x => x.IncludeParameters<T>(dummy));

            var expected =
                typeof(ClassWithMethods)
                    .GetMethod("IncludeParameters")
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
        public void QueryNonParameterLessGenericMethodUsingLinqSyntax()
        {
            var sut = new Methods<ClassWithMethods>();
            var dummy = default(T);

            var actual = from x in sut select x.IncludeParameters<T>(dummy);

            var expected =
                typeof(ClassWithMethods)
                    .GetMethod("IncludeParameters")
                    .MakeGenericMethod(typeof(T));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectParameterLessGenericMethodInGenericClassReturnsCorrectMethod()
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
        public void SelectParameterLessGenericMethodInGenericClassWithReturnValueReturnsCorrectMethod()
        {
            var sut = new Methods<ClassWithMethods<T>>();

            MethodInfo actual = sut.Select(x =>
                x.OmitParametersGenericWithReturnValue<T>());

            var expected =
                typeof(ClassWithMethods<T>)
                    .GetMethod("OmitParametersGenericWithReturnValue")
                    .MakeGenericMethod(typeof(T));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectNonParameterLessGenericMethodWithParametersInGenericClassReturnsCorrectMethod()
        {
            var sut = new Methods<ClassWithMethods<T>>();
            var dummy = default(T);

            MethodInfo actual = sut.Select(x => x.IncludeParameters<T>(dummy));

            var expected =
                typeof(ClassWithMethods<T>)
                    .GetMethod("IncludeParameters")
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

        [Fact]
        public void QueryParameterLessMethodWithReturnValueInGenericClassUsingLinqSyntax()
        {
            var sut = new Methods<ClassWithMethods<T>>();

            var actual = from x in sut
                         select x.OmitParametersGenericWithReturnValue<T>();

            var expected =
                typeof(ClassWithMethods<T>)
                    .GetMethod("OmitParametersGenericWithReturnValue")
                    .MakeGenericMethod(typeof(T));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QueryNonParameterLessGenericMethodWithParametersInGenericClassUsingLinqSyntax()
        {
            var sut = new Methods<ClassWithMethods<T>>();
            var dummy = default(T);

            var actual = from x in sut select x.IncludeParameters<T>(dummy);

            var expected =
                typeof(ClassWithMethods<T>)
                    .GetMethod("IncludeParameters")
                    .MakeGenericMethod(typeof(T));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectMethodDeclaredOnBaseReturnsCorrectMethod()
        {
            var sut = new Methods<ClassWithMethods>();
            var expected = typeof(ClassWithMethods).GetMethod("ToString");

            var actual = sut.Select(x => x.ToString());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectMethodReturnsCorrectMethod()
        {
            var sut = new Methods<ClassOverridingToString>();
            var expected =
                typeof(ClassOverridingToString).GetMethod("ToString");

            var actual = sut.Select(x => x.ToString());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectParameterizedGenericMethodDeclaredOnBaseClassReturnsCorrectMethod()
        {
            var sut = new Methods<SubClassWithMethods<T>>();
            var expected = typeof(SubClassWithMethods<T>).GetMethods()
                .Single(m => m.Name == "IncludeMoreParameters"
                    && m.GetParameters().Length == 4)
                .MakeGenericMethod(typeof(string));
            Assert.False(expected.ContainsGenericParameters, "closed generic method form.");

            var actual = sut.Select(x => x.IncludeMoreParameters<string>(
                default(int),
                default(T),
                default(object),
                default(string)));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectParameterizedGenericMethodWithReturnValueDeclaredOnBaseClassReturnsCorrectMethod()
        {
            var sut = new Methods<SubClassWithMethods<T>>();
            var expected = typeof(SubClassWithMethods<T>).GetMethods()
                .Single(m => m.Name == "IncludeMoreParametersWithReturnValue"
                    && m.GetParameters().Length == 4)
                .MakeGenericMethod(typeof(string));
            Assert.False(expected.ContainsGenericParameters, "closed generic method form.");

            var actual = sut.Select(x => x.IncludeMoreParametersWithReturnValue<string>(
                default(object),
                default(string),
                default(int),
                default(T)));

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

            public void IncludeParameters<U>(U item)
            {
            }
        }

        private class ClassWithMethods<V>
        {
            public void OmitParametersGeneric<U>()
            {
            }

            public object OmitParametersGenericWithReturnValue<U>()
            {
                return default(U);
            }

            public void IncludeParameters<U>(U item)
            {
            }

            public void IncludeMoreParameters<U>(int item1, V item2, object item3, U item4)
            {
            }

            public void IncludeMoreParameters<U>(int item1, V item2, object item3)
            {
            }

            public object IncludeMoreParametersWithReturnValue<U>(object item1, U item2, int item3, V item4)
            {
                return default(object);
            }

            public object IncludeMoreParametersWithReturnValue<U>(object item1, U item2, int item3)
            {
                return default(object);
            }
        }

        private class SubClassWithMethods<V> : ClassWithMethods<V>
        {
        }

        private class ClassOverridingToString
        {
            public override string ToString()
            {
                return "foo";
            }
        }
    }

    public class MethodTestsOfInt : MethodsTests<int> { }
    public class MethodTestsOfByte : MethodsTests<byte> { }
    public class MethodTestsOfFloat : MethodsTests<float> { }
}
