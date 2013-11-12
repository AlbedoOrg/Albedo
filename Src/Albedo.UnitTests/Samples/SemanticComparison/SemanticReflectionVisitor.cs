using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo.UnitTests.Samples.SemanticComparison
{
    public class SemanticReflectionVisitor :
        ReflectionVisitor<IEnumerable<SemanticComparisonValue>>
    {
        private readonly SemanticComparisonValue[] values;

        public SemanticReflectionVisitor(
            params SemanticComparisonValue[] values)
        {
            this.values = values;
        }

        public override IEnumerable<SemanticComparisonValue> Value
        {
            get { return this.values; }
        }

        public override IReflectionVisitor<IEnumerable<SemanticComparisonValue>> Visit(
            FieldInfoElement fieldInfoElement)
        {
            var v = new SemanticComparisonValue(
                fieldInfoElement.FieldInfo.Name,
                fieldInfoElement.FieldInfo.FieldType);
            return new SemanticReflectionVisitor(
                this.values.Concat(new[] { v }).ToArray());
        }

        public override IReflectionVisitor<IEnumerable<SemanticComparisonValue>> Visit(
            ParameterInfoElement parameterInfoElement)
        {
            var v = new SemanticComparisonValue(
                parameterInfoElement.ParameterInfo.Name,
                parameterInfoElement.ParameterInfo.ParameterType);
            return new SemanticReflectionVisitor(
                this.values.Concat(new[] { v }).ToArray());
        }

        public override IReflectionVisitor<IEnumerable<SemanticComparisonValue>> Visit(
            PropertyInfoElement propertyInfoElement)
        {
            var v = new SemanticComparisonValue(
                propertyInfoElement.PropertyInfo.Name,
                propertyInfoElement.PropertyInfo.PropertyType);
            return new SemanticReflectionVisitor(
                this.values.Concat(new[] { v }).ToArray());
        }
    }
}
