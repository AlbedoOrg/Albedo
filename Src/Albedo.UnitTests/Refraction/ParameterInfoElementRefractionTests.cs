﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Albedo.Refraction;
using Xunit;

namespace Albedo.UnitTests.Refraction
{
    public class ParameterInfoElementRefractionTests
    {
        [Fact]
        public void SutIsReflectionElementRefraction()
        {
            var sut = new ParameterInfoElementRefraction<object>();
            Assert.IsAssignableFrom<IReflectionElementRefraction<object>>(sut);
        }

        [Theory, ClassData(typeof(SourceObjects))]
        public void RefractObjectsReturnsCorrectResult(object[] objects)
        {
            var sut = new ParameterInfoElementRefraction<object>();

            var actual = sut.Refract(objects);

            var expected = objects
                .OfType<ParameterInfo>()
                .Select(mi => new ParameterInfoElement(mi))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RefractNullSourceThrows()
        {
            var sut = new ParameterInfoElementRefraction<object>();
            Assert.Throws<ArgumentNullException>(() => sut.Refract(null));
        }

        private class SourceObjects : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new object[]
                    {
                        TypeWithParameter.Parameter
                    }
                };
                yield return new object[]
                {
                    new object[]
                    {
                        TypeWithParameter.Parameter,
                        TypeWithParameter.OtherParameter
                    }
                };
                yield return new object[]
                {
                    new object[]
                    {
                        TypeWithParameter.Parameter,
                        "",
                        TypeWithParameter.OtherParameter
                    }
                };
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        class TypeWithParameter
        {
            public static ParameterInfo Parameter
            {
                get
                {
                    return typeof(TypeWithParameter)
                        .GetMethod("TheMethod")
                        .GetParameters()
                        .First();
                }
            }

            public static ParameterInfo OtherParameter
            {
                get
                {
                    return typeof(TypeWithParameter)
                        .GetMethod("TheOtherMethod")
                        .GetParameters()
                        .First();
                }
            }

            public void TheMethod(int param1)
            {
            }

            public void TheOtherMethod(int param1)
            {
            }
        }
    }
}
