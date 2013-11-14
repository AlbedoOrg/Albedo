using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo.UnitTests.Samples.ValueWriting
{
    public class ValueWritingVisitor : ReflectionVisitor<Action<object>>
    {
        private readonly object target;
        private readonly Action<object>[] actions;

        public ValueWritingVisitor(
            object target,
            params Action<object>[] actions)
        {
            this.target = target;
            this.actions = actions;
        }

        public override Action<object> Value
        {
            get
            {
                return x =>
                {
                    foreach (var a in this.actions)
                        a(x);
                };
            }
        }

        public override IReflectionVisitor<Action<object>> Visit(
            FieldInfoElement fieldInfoElement)
        {
            Action<object> a =
                v => fieldInfoElement.FieldInfo.SetValue(this.target, v);
            return new ValueWritingVisitor(
                this.target,
                this.actions.Concat(new[] { a }).ToArray());
        }

        public override IReflectionVisitor<Action<object>> Visit(
            PropertyInfoElement propertyInfoElement)
        {
            Action<object> a =
                v => propertyInfoElement.PropertyInfo.SetValue(
                    this.target,
                    v,
                    null);
            return new ValueWritingVisitor(
                this.target,
                this.actions.Concat(new[] { a }).ToArray());
        }
    }
}
