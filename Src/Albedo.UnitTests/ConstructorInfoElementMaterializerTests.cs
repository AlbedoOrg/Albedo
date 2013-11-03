using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;
using Xunit.Extensions;
using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    public class ConstructorInfoElementMaterializerTests
    {
        [Fact]
        public void SutIsReflectionElementMaterializer()
        {
            var sut = new ConstructorInfoElementMaterializer<object>();
            Assert.IsAssignableFrom<IReflectionElementMaterializer<object>>(sut);
        }

        [Theory, ClassData(typeof(SourceObjects))]
        public void MaterializeObjectsReturnsCorrectResult(object[] objects)
        {
            var sut = new ConstructorInfoElementMaterializer<object>();

            var actual = sut.Materialize(objects);

            var expected = objects
                .OfType<ConstructorInfo>()
                .Select(ci => new ConstructorInfoElement(ci))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
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
