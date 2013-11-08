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
    public class ConstructorInfoElementRefractionTests
    {
        [Fact]
        public void SutIsReflectionElementRefraction()
        {
            var sut = new ConstructorInfoElementRefraction<object>();
            Assert.IsAssignableFrom<IReflectionElementRefraction<object>>(sut);
        }

        [Theory, ClassData(typeof(SourceObjects))]
        public void MaterializeObjectsReturnsCorrectResult(object[] objects)
        {
            var sut = new ConstructorInfoElementRefraction<object>();

            var actual = sut.Materialize(objects);

            var expected = objects
                .OfType<ConstructorInfo>()
                .Select(ci => new ConstructorInfoElement(ci))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MaterializeNullSourceThrows()
        {
            var sut = new ConstructorInfoElementRefraction<object>();
            Assert.Throws<ArgumentNullException>(() => sut.Materialize(null));
        }

        private class SourceObjects : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new[]
                {
                    new object[]
                    {
                        this.GetType().GetConstructors().First() 
                    } 
                };
                yield return new[]
                {
                    new object[]
                    {
                        this.GetType().GetConstructors().First(),
                        typeof(Version).GetConstructors().First()
                    } 
                };
                yield return new[]
                {
                    new object[]
                    {
                        this.GetType().GetConstructors().First(),
                        typeof(Version),
                        typeof(Version).GetConstructors().First()
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
