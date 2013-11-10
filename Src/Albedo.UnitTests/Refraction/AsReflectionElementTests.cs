using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Moq;
using Xunit;
using Xunit.Extensions;

namespace Ploeh.Albedo.Refraction.UnitTests
{
    public class AsReflectionElementTests
    {
        [Theory, ClassData(typeof(AsReflectionElementTestCases))]
        public void AsReflectionElementReturnsCorrectResult(
            object source,
            IReflectionElement expected)
        {
            IReflectionElement actual = source.AsReflectionElement();
            Assert.Equal(expected, actual);
        }

        private class AsReflectionElementTestCases : IEnumerable<object[]>
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

                yield return new object[]
                {
                    new object(),
                    new NullReflectionElement()
                };
                yield return new object[]
                {
                    "",
                    new NullReflectionElement()
                };
                yield return new object[]
                {
                    2,
                    new NullReflectionElement()
                };
                yield return new object[]
                {
                    new Version(1, 1),
                    new NullReflectionElement()
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

        [Fact]
        public void AsReflectionElementWithNullSourceThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => ((object)null).AsReflectionElement());
        }
    }
}
