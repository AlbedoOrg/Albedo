using System;
using System.Collections.Generic;
using System.Linq;

namespace Ploeh.Albedo
{
    /// <summary>
    /// Represents a Visitor which can visit <see cref="IReflectionElement" /> instances,
    /// collecting values from <see cref="PropertyInfoElement"/> or
    /// <see cref="FieldInfoElement"/> instances.
    /// </summary>
    public class ValueCollectingVisitor : ReflectionVisitor<IEnumerable<object>>
    {
        private readonly object target;
        private readonly object[] values;

        /// <summary>
        /// Constructs a new instance of the <see cref="EventInfoElement"/> which represents
        /// the specified <see cref="System.Reflection.EventInfo"/>.
        /// </summary>
        /// <param name="target">The target object instance, from which
        /// values will be collected</param>
        /// <param name="values">The existing/initial values from any previous collections
        /// </param>
        public ValueCollectingVisitor(object target, params object[] values)
        {
            if (target == null) throw new ArgumentNullException("target");
            if (values == null) throw new ArgumentNullException("values");
            this.target = target;
            this.values = values;
        }

        /// <summary>
        /// Gets the observation or value produced by this instance; the actual values
        /// from the visited properties and fields.
        /// </summary>
        public override IEnumerable<object> Value
        {
            get { return this.values; }
        }

        /// <summary>
        /// Gets the target object that values are collected from.
        /// </summary>
        public object Target
        {
            get { return this.target; }
        }

        public override IReflectionVisitor<IEnumerable<object>> Visit(AssemblyElement assemblyElement)
        {
            return this;
        }

        public override IReflectionVisitor<IEnumerable<object>> Visit(TypeElement typeElement)
        {
            return this;
        }

        /// <summary>
        /// Visits the <see cref="FieldInfoElement"/> and collects the value for this
        /// field from the <see cref="Target"/> instance.
        /// </summary>
        /// <param name="fieldInfoElement">
        /// The <see cref="FieldInfoElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<object>> Visit(
            FieldInfoElement fieldInfoElement)
        {
            if (fieldInfoElement == null) throw new ArgumentNullException("fieldInfoElement");
            var value = fieldInfoElement.FieldInfo.GetValue(this.target);
            return new ValueCollectingVisitor(
                this.target,
                this.values.Concat(new[] {value}).ToArray());
        }

        /// <summary>
        /// Visits the <see cref="FieldInfoElement"/> and collects the value for this
        /// field from the <see cref="Target"/> instance.
        /// </summary>
        /// <param name="propertyInfoElement">
        /// The <see cref="PropertyInfoElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<object>> Visit(
            PropertyInfoElement propertyInfoElement)
        {
            if (propertyInfoElement == null) throw new ArgumentNullException("propertyInfoElement");
            var value = propertyInfoElement.PropertyInfo.GetValue(this.target, null);
            return new ValueCollectingVisitor(
                this.target,
                this.values.Concat(new[] {value}).ToArray());
        }
    }
}