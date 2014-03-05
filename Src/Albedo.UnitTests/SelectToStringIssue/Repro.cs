using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests.SelectToStringIssue
{
    public class Repro
    {
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
