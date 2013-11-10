using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public abstract class ReflectionVisitorTests<T>
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new ReflectionVisitor();
            Assert.IsAssignableFrom<IReflectionVisitor<T>>(sut);
        }

        [Fact]
        public void VisitAssemblyElementReturnsCorrectResult()
        {
            var sut = new ReflectionVisitor();
            var assemblyElement =
                new AssemblyElement(this.GetType().Assembly);

            var actual = sut.Visit(assemblyElement);

            var expected = sut;
            Assert.Same(expected, actual);
        }

        private class ReflectionVisitor : ReflectionVisitor<T>
        {
            public override T Value
            {
                get { throw new NotImplementedException(); }
            }
        }
    }

    public class ReflectionVisitorTestsOfObject : ReflectionVisitorTests<object> { }
    public class ReflectionVisitorTestsOfDouble : ReflectionVisitorTests<double> { }
    public class ReflectionVisitorTestsOfString : ReflectionVisitorTests<string> { }
}
