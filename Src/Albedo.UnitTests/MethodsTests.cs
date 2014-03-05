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
    public class MethodsTests
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
            var sut = new Methods<ClassWithMethodsOverridingEquals>();
            var expected =
                typeof(ClassWithMethodsOverridingEquals).GetMethod("ToString");

            var actual = sut.Select(x => x.ToString());

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
        }

        private class ClassWithMethodsOverridingEquals
        {
            public override string ToString()
            {
                return "foo";
            }
        }
    }
}
