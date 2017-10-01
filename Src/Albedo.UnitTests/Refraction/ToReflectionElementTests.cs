using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Albedo.Refraction;
using Moq;
using Xunit;

namespace Albedo.UnitTests.Refraction
{
    public class ToReflectionElementTests
    {
        [Theory, ClassData(typeof(ToReflectionElementTestCases))]
        public void ToReflectionElementReturnsCorrectResult(
            object source,
            IReflectionElement expected)
        {
            IReflectionElement actual = source.ToReflectionElement();
            Assert.Equal(expected, actual);
        }

        private class ToReflectionElementTestCases : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    this.GetType().Assembly,
                    new AssemblyElement(this.GetType().Assembly)
                };
                yield return new object[]
                {
                    this.GetType().GetConstructors().First(),
                    new ConstructorInfoElement(
                        this.GetType().GetConstructors().First())
                };
                yield return new object[]
                {
                    typeof(AppDomain).GetEvents().First(),
                    new EventInfoElement(typeof(AppDomain).GetEvents().First())
                };
                yield return new object[]
                {
                    typeof(int).GetFields(BindingFlags.Static | BindingFlags.Public).First(),
                    new FieldInfoElement(typeof(int).GetFields(BindingFlags.Static | BindingFlags.Public).First())
                };
                yield return new object[]
                {
                    TypeWithLocalVariable.LocalVariable,
                    new LocalVariableInfoElement(TypeWithLocalVariable.LocalVariable)
                };
                yield return new object[]
                {
                    this.GetType().GetMethods().First(),
                    new MethodInfoElement(this.GetType().GetMethods().First())
                };
                yield return new object[]
                {
                    new Methods<object>()
                        .Select(x => x.Equals(null))
                        .GetParameters()
                        .First(),
                    new ParameterInfoElement(
                        new Methods<object>()
                            .Select(x => x.Equals(null))
                            .GetParameters()
                            .First())
                };
                yield return new object[]
                {
                    typeof(Version).GetProperties().First(),
                    new PropertyInfoElement(
                        typeof(Version).GetProperties().First())
                };
                yield return new object[]
                {
                    this.GetType(),
                    new TypeElement(this.GetType())
                };

                var re = new Mock<IReflectionElement>().Object;
                yield return new object[]
                {
                    re,
                    re
                };
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            private class TypeWithLocalVariable
            {
                private readonly static LocalVariableInfo localVariable =
                    typeof(TypeWithLocalVariable)
                        .GetMethod("TheMethod")
                        .GetMethodBody()
                        .LocalVariables[0];

                public static LocalVariableInfo LocalVariable
                {
                    get { return localVariable; }
                }

                public void TheMethod()
                {
                    // This is required to prevent the compiler from
                    // warning and optimising away the local variable.
                    var local = 1;
                    local = local + 1;
                    local = local + 2;
                }
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData(1)]
        [InlineData(42L)]
        public void NonReflectionObjectToReflectionElementThrows(object source)
        {
            Assert.Throws<ArgumentException>(() => source.ToReflectionElement());
        }

        [Fact]
        public void ToReflectionElementWithNullSourceThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => ((object)null).ToReflectionElement());
        }
    }
}
