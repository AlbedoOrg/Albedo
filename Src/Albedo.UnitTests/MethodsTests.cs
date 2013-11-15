using System;
using System.Collections.Generic;
using System.Linq;
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
                () => sut.Select(_ => new object()));
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
    }
}
