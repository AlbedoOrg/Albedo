using System;
using System.Collections.Generic;
using System.Linq;

namespace Ploeh.Albedo
{
    public class ValueCollectingVisitor<T> : ReflectionVisitor<IEnumerable<T>>
    {
        private readonly object target;
        private readonly T[] values;

        public ValueCollectingVisitor(object target, params T[] values)
        {
            if (target == null) throw new ArgumentNullException("target");
            this.target = target;
            this.values = values;
        }

        public override IEnumerable<T> Value
        {
            get { return this.values; }
        }

        public override IReflectionVisitor<IEnumerable<T>> Visit(
            FieldInfoElement fieldInfoElement)
        {
            if (fieldInfoElement == null) throw new ArgumentNullException("fieldInfoElement");
            if (!typeof(T).IsAssignableFrom(fieldInfoElement.FieldInfo.FieldType))
                return this;

            var value = (T)fieldInfoElement.FieldInfo.GetValue(this.target);
            return new ValueCollectingVisitor<T>(
                this.target,
                this.values.Concat(new[] { value }).ToArray());
        }

        public override IReflectionVisitor<IEnumerable<T>> Visit(
            PropertyInfoElement propertyInfoElement)
        {
            if (propertyInfoElement == null) throw new ArgumentNullException("propertyInfoElement");
            if (!typeof(T).IsAssignableFrom(propertyInfoElement.PropertyInfo.PropertyType))
                return this;

            var value = (T)propertyInfoElement.PropertyInfo.GetValue(this.target, null);
            return new ValueCollectingVisitor<T>(
                this.target,
                this.values.Concat(new[] { value }).ToArray());
        }
    }
}