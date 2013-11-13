using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo.UnitTests.Samples.ValueReading
{
    public class ValueCollectingVisitor : ReflectionVisitor<IEnumerable>
    {
        private readonly object target;
        private readonly object[] values;

        public ValueCollectingVisitor(object target, params object[] values)
        {
            this.target = target;
            this.values = values;
        }

        public override IEnumerable Value
        {
            get { return this.values; }
        }

        public override IReflectionVisitor<IEnumerable> Visit(
            FieldInfoElement fieldInfoElement)
        {
            var value = fieldInfoElement.FieldInfo.GetValue(this.target);
            return new ValueCollectingVisitor(
                this.target,
                this.values.Concat(new[] { value }).ToArray());
        }

        public override IReflectionVisitor<IEnumerable> Visit(
            PropertyInfoElement propertyInfoElement)
        {
            var value = 
                propertyInfoElement.PropertyInfo.GetValue(this.target, null);
            return new ValueCollectingVisitor(
                this.target,
                this.values.Concat(new[] { value }).ToArray());
        }
    }
}
