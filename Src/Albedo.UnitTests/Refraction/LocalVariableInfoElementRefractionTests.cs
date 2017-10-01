using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Albedo.Refraction;
using Xunit;

namespace Albedo.UnitTests.Refraction
{
    public class LocalVariableInfoElementRefractionTests
    {
        [Fact]
        public void SutIsReflectionElementRefraction()
        {
            var sut = new LocalVariableInfoElementRefraction<object>();
            Assert.IsAssignableFrom<IReflectionElementRefraction<object>>(sut);
        }

        [Theory]
        [ClassData(typeof(SourceObjects))]
        public void RefractReturnsCorrectResult(object[] source)
        {
            var sut = new LocalVariableInfoElementRefraction<object>();

            var actual = sut.Refract(source);

            var expected = source
                .OfType<LocalVariableInfo>()
                .Select(m => new LocalVariableInfoElement(m))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RefractNullSourceThrows()
        {
            var sut = new LocalVariableInfoElementRefraction<object>();
            Assert.Throws<ArgumentNullException>(() => sut.Refract(null));
        }

        private class SourceObjects : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {new object[] { null }};
                yield return new object[] {new object[] {TypeWithLocalVariable.LocalVariable} };
                yield return new object[]
                {
                    new object[]
                    {
                        TypeWithLocalVariable.LocalVariable,
                        TypeWithLocalVariable.OtherLocalVariable
                    }
                };
                yield return new object[]
                {
                    new object[]
                    {
                        TypeWithLocalVariable.LocalVariable,
                        null,
                        TypeWithLocalVariable.OtherLocalVariable
                    }
                };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        class TypeWithLocalVariable
        {
            public static LocalVariableInfo LocalVariable
            {
                get
                {
                    return typeof(TypeWithLocalVariable)
                        .GetMethod("TheMethod")
                        .GetMethodBody()
                        .LocalVariables[0];
                }
            }

            public static LocalVariableInfo OtherLocalVariable
            {
                get
                {
                    return typeof(TypeWithLocalVariable)
                        .GetMethod("TheOtherMethod")
                        .GetMethodBody()
                        .LocalVariables[0];
                }
            }

            public void TheMethod()
            {
                // This is required to prevent the compiler from
                // warning and optimising away the local variable.
                var local = 1;
                local = local + 1;
                local = local + 2;
            }

            public void TheOtherMethod()
            {
                // This is required to prevent the compiler from
                // warning and optimising away the local variable.
                var local = 1;
                local = local + 1;
                local = local + 2;
            }
        }
    }
}