using System;
using System.Collections.Generic;
using System.Linq;
using Albedo.Refraction;
using Moq;
using Xunit;

namespace Albedo.UnitTests.Refraction
{
    public class ReflectionElementRefractionTests
    {
        [Fact]
        public void SutIsReflectionElementRefraction()
        {
            var sut = new ReflectionElementRefraction<object>();
            Assert.IsAssignableFrom<IReflectionElementRefraction<object>>(sut);
        }

        [Theory, ClassData(typeof(SourceObjects))]
        public void RefractObjectsReturnsCorrectResult(object[] objects)
        {
            var sut = new ReflectionElementRefraction<object>();

            var actual = sut.Refract(objects);

            var expected = objects.OfType<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RefractNullSourceThrows()
        {
            var sut = new ReflectionElementRefraction<object>();
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
                        new Mock<IReflectionElement>().Object
                    }
                };
                yield return new object[]
                {
                    new object[]
                    {
                        new Mock<IReflectionElement>().Object,
                        new Mock<IReflectionElement>().Object
                    }
                };
                yield return new object[]
                {
                    new object[]
                    {
                        new Mock<IReflectionElement>().Object,
                        "",
                        new Mock<IReflectionElement>().Object
                    }
                };
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
