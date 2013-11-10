using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo.Refraction;
using Xunit.Extensions;
using Moq;

namespace Ploeh.Albedo.Refraction.UnitTests
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
