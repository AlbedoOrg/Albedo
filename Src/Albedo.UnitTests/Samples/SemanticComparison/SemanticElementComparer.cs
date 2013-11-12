using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo.UnitTests.Samples.SemanticComparison
{
    public class SemanticElementComparer : IEqualityComparer<IReflectionElement>
    {
        private readonly IReflectionVisitor<IEnumerable<SemanticComparisonValue>> visitor;

        public SemanticElementComparer(
            IReflectionVisitor<IEnumerable<SemanticComparisonValue>> visitor)
        {
            this.visitor = visitor;
        }

        public bool Equals(IReflectionElement x, IReflectionElement y)
        {
            var values = new CompositeReflectionElement(x, y)
                .Accept(this.visitor)
                .Value
                .ToArray();
            return values.Length == 2
                && values.Distinct().Count() == 1;
        }

        public int GetHashCode(IReflectionElement obj)
        {
            return obj
                .Accept(this.visitor)
                .Value
                .Single()
                .GetHashCode();
        }
    }
}
