using System;

namespace Albedo.UnitTests.Samples.SemanticComparison
{
    public class SemanticComparisonValue
    {
        private readonly string name;
        private readonly Type type;

        public SemanticComparisonValue(string name, Type type)
        {
            this.name = name;
            this.type = type;
        }

        public override bool Equals(object obj)
        {
            var other = obj as SemanticComparisonValue;
            if (other == null)
                return base.Equals(obj);

            return object.Equals(this.type, other.type)
                && string.Equals(this.name, other.name,
                    StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return
                this.name.ToUpperInvariant().GetHashCode() ^
                this.type.GetHashCode();
        }
    }
}
