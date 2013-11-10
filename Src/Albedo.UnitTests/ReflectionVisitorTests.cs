using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        [Fact]
        public void VisitConstructorInfoElementReturnsCorrectResult()
        {
            var sut = new ReflectionVisitor();
            var constructorInfoElement =
                new ConstructorInfoElement(
                    this.GetType().GetConstructor(Type.EmptyTypes));

            var actual = sut.Visit(constructorInfoElement);

            var expected = sut;
            Assert.Same(expected, actual);
        }

        [Fact]
        public void VisitFieldInfoElementReturnsCorrectResult()
        {
            var sut = new ReflectionVisitor();
            var fieldInfoElement =
                new FieldInfoElement(Dummy.Field);

            var actual = sut.Visit(fieldInfoElement);

            var expected = sut;
            Assert.Same(expected, actual);
        }

        [Fact]
        public void VisitMethodInfoElementReturnsCorrectResult()
        {
            var sut = new ReflectionVisitor();
            var methodInfoElement =
                new MethodInfoElement(Dummy.Method);

            var actual = sut.Visit(methodInfoElement);

            var expected = sut;
            Assert.Same(expected, actual);
        }

        [Fact]
        public void VisitParameterInfoElementReturnsCorrectResult()
        {
            var sut = new ReflectionVisitor();
            var parameterInfoElement =
                new ParameterInfoElement(Dummy.Parameter);

            var actual = sut.Visit(parameterInfoElement);

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

        private class Dummy
        {
            internal static FieldInfo Field
            {
                get
                {
                    return typeof(Dummy).GetFields(
                        BindingFlags.NonPublic | BindingFlags.Instance)[0];
                }
            }

            internal static MethodInfo Method
            {
                get
                {
                    return typeof(Dummy).GetMethods(
                        BindingFlags.NonPublic | BindingFlags.Instance)[0];
                }
            }

            internal static ParameterInfo Parameter
            {
                get
                {
                    return typeof(Dummy)
                        .GetMethods(
                            BindingFlags.NonPublic | BindingFlags.Instance)
                        .SelectMany(x => x.GetParameters())
                        .Single();
                }
            }

            private int i = 123;

            private void M() 
            {
            }

            private void M(object o) 
            {
            }
        }
    }

    public class ReflectionVisitorTestsOfObject : ReflectionVisitorTests<object> { }
    public class ReflectionVisitorTestsOfDouble : ReflectionVisitorTests<double> { }
    public class ReflectionVisitorTestsOfString : ReflectionVisitorTests<string> { }
}
