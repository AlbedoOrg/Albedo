using System;
using System.Collections.Generic;
using System.Linq;

namespace Ploeh.Albedo
{
    public class ValueCollectingVisitor : ReflectionVisitor<IEnumerable<object>>
    {
        private readonly object target;
        private readonly object[] values;

        public ValueCollectingVisitor(object target, params object[] values)
        {
            if (target == null) throw new ArgumentNullException("target");
            if (values == null) throw new ArgumentNullException("values");
            this.target = target;
            this.values = values;
        }

        public override IEnumerable<object> Value
        {
            get { return this.values; }
        }

        public override IReflectionVisitor<IEnumerable<object>> Visit(
            FieldInfoElement fieldInfoElement)
        {
            if (fieldInfoElement == null) throw new ArgumentNullException("fieldInfoElement");
            var value = fieldInfoElement.FieldInfo.GetValue(this.target);
            return new ValueCollectingVisitor(
                this.target,
                this.values.Concat(new[] { value }).ToArray());
        }

        public override IReflectionVisitor<IEnumerable<object>> Visit(
            PropertyInfoElement propertyInfoElement)
        {
            if (propertyInfoElement == null) throw new ArgumentNullException("propertyInfoElement");
            var value = propertyInfoElement.PropertyInfo.GetValue(this.target, null);
            return new ValueCollectingVisitor(
                this.target,
                this.values.Concat(new[] { value }).ToArray());
        }
    }
}