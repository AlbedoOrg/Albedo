using Ploeh.Albedo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Extensions;

namespace Ploeh.Albedo.UnitTests
{
    public class MethodInfoElementMaterializerTests
    {
        [Fact]
        public void SutIsReflectionElementMaterializer()
        {
            var sut = new MethodInfoElementMaterializer<object>();
            Assert.IsAssignableFrom<IReflectionElementMaterializer<object>>(sut);
        }

        [Theory, ClassData(typeof(SourceObjects))]
        public void MaterializeObjectsReturnsCorrectResult(object[] objects)
        {
            var sut = new MethodInfoElementMaterializer<object>();

            var actual = sut.Materialize(objects);

            var expected = objects
                .OfType<MethodInfo>()
                .Select(mi => new MethodInfoElement(mi))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

         [Fact]
         public void MaterializeNullSourceThrows()
         {
             var sut = new MethodInfoElementMaterializer<object>();
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
                        typeof(AppDomain).GetMethods().First()
                    }
                };
                yield return new[]
                {
                    typeof(AppDomain).GetMethods().Take(2).ToArray()
                };
                yield return new[]
                {
                    new object[]
                    {
                        typeof(AppDomain).GetMethods().First(),
                        "",
                        typeof(AppDomain).GetMethods().Skip(1).First(),
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