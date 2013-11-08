using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;
using Xunit.Extensions;
using System.Reflection;

namespace Ploeh.Albedo.Refraction.UnitTests
{
    public class EventInfoElementRefractionTests
    {
        [Fact]
        public void SutIsReflectionElementRefraction()
        {
            var sut = new EventInfoElementRefraction<object>();
            Assert.IsAssignableFrom<IReflectionElementRefraction<object>>(sut);
        }

        [Theory, ClassData(typeof(SourceObjects))]
        public void rObjectsReturnsCorrectResult(object[] objects)
        {
            var sut = new EventInfoElementRefraction<object>();

            var actual = sut.Refract(objects);

            var expected = objects
                .OfType<EventInfo>()
                .Select(ei => new EventInfoElement(ei))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void rNullSourceThrows()
        {
            var sut = new EventInfoElementRefraction<object>();
            Assert.Throws<ArgumentNullException>(() => sut.Refract(null));
        }

        private class SourceObjects : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new[]
                {
                    new object[]
                    {
                        typeof(AppDomain).GetEvents().First()
                    }
                };
                yield return new[]
                {
                    typeof(AppDomain).GetEvents().Take(2).ToArray()
                };
                yield return new[]
                {
                    new object[]
                    {
                        typeof(AppDomain).GetEvents().First(),
                        "",
                        typeof(AppDomain).GetEvents().Skip(1).First(),
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
