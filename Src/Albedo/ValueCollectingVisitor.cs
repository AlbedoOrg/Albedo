using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        /// <summary>Visits the specified assembly element.</summary>
        /// <param name="assemblyElement">The assembly element.</param>
        /// <returns>Always throws.</returns>
        /// <exception cref="System.NotSupportedException">
        /// Collecting values from an entire assembly is not supported.
        /// ValueCollectingVisitor collect values from a single object (the
        /// 'target'), and the values collected must correspond to fields or 
        /// properties on that object. Attempting to collect values from an
        /// entire Assembly is meaningless, because an Assembly normally 
        /// defines more than a single type. In the rare case where you have an
        /// assembly with only a single type, use the Visit(TypeElement)
        /// overload instead.
        /// </exception>
        public override IReflectionVisitor<IEnumerable<object>> Visit(
            AssemblyElement assemblyElement)
        {
            throw new NotSupportedException("Collecting values from an entire assembly is not supported. ValueCollectingVisitor collect values from a single object (the 'target'), and the values collected must correspond to fields or properties on that object. Attempting to collect values from an entire Assembly is meaningless, because an Assembly normally defines more than a single type. In the rare case where you have an assembly with only a single type, use the Visit(TypeElement) overload instead.");
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
            if (fieldInfoElement == null)
                throw new ArgumentNullException("fieldInfoElement");

            var value = fieldInfoElement.FieldInfo.GetValue(this.target);
            return new ValueCollectingVisitor(
                this.target,
                this.values.Concat(new[] { value }).ToArray());
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
            if (propertyInfoElement == null)
                throw new ArgumentNullException("propertyInfoElement");
            if (this.IsIncompatibleWith(propertyInfoElement))
                throw new ArgumentException(
                    string.Format(
                        "Property '{0}' defined on type '{1}' is not a property on the target object, which is of type '{2}'.",
                        propertyInfoElement.PropertyInfo.Name,
                        propertyInfoElement.PropertyInfo.DeclaringType,
                        this.target.GetType()),
                    "propertyInfoElement");

            var value =
                propertyInfoElement.PropertyInfo.GetValue(this.target, null);
            return new ValueCollectingVisitor(
                this.target,
                this.values.Concat(new[] { value }).ToArray());
        }

        private bool IsIncompatibleWith(PropertyInfoElement propertyInfoElement)
        {
            return !propertyInfoElement
                .PropertyInfo
                .DeclaringType
                .IsInstanceOfType(this.target);
        }
    }
}