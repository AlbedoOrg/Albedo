using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Ploeh.Albedo
{
    /// <summary>
    /// An <see cref="IReflectionElement"/> representing a <see cref="System.Type"/> which
    /// can be visited by an <see cref="IReflectionVisitor{T}"/> implementation.
    /// </summary>
    public class TypeElement : IReflectionElement
    {
        private const BindingFlags bindingFlags = 
            BindingFlags.Instance | BindingFlags.Static |
            BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// Gets the <see cref="System.Type"/> instance this element represents.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods",
            Justification = "The name follows a consistent pattern with the other reflection elements, and most users will understand the distiction between 'GetType()' and 'Type'.")]
        public Type Type { get; private set; }

        /// <summary>
        /// Constructs a new instance of the <see cref="TypeElement"/> which represents
        /// the specified <see cref="System.Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="System.Type"/> this element represents.</param>
        public TypeElement(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            this.Type = type;
        }

        /// <summary>
        /// Accepts the provided <see cref="IReflectionVisitor{T}"/>, by calling the
        /// appropriate strongly-typed <see cref="IReflectionVisitor{T}.Visit(TypeElement)"/>
        /// method on the visitor.
        /// </summary>
        /// <typeparam name="T">The type of observation or result which the
        /// <see cref="IReflectionVisitor{T}"/> instance produces when visiting nodes.</typeparam>
        /// <param name="visitor">The <see cref="IReflectionVisitor{T}"/> instance.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        public IReflectionVisitor<T> Accept<T>(IReflectionVisitor<T> visitor)
        {
            if (visitor == null) throw new ArgumentNullException("visitor");
            return visitor.Visit(this);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object" />, is equal to
        /// this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this
        /// instance.</param>
        /// <returns>
        /// <see langword="true" /> if the specified <see cref="Object" /> is
        /// equal to this instance; otherwise, <see langword="false" />.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Two instances of <see cref="TypeElement" /> are considered to
        /// be equal if their <see cref="Type" /> values are equal.
        /// </para>
        /// </remarks>
        public override bool Equals(object obj)
        {
            var other = obj as TypeElement;
            if (other == null)
                return false;

            return object.Equals(this.Type, other.Type);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing
        /// algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Type.GetHashCode();
        }

        /// <summary>
        /// Gets the string representation of the <see cref="Type"/>
        /// suitable for development / debugging display purposes.
        /// </summary>
        /// <returns>The string representation of the contained
        /// <see cref="Type"/></returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.CurrentCulture, "[[{0}]] ({1})", this.Type, "type");
        }

        internal FieldInfoElement[] GetFieldInfoElements()
        {
            return this.Type.GetFields(TypeElement.bindingFlags).Select(f => f.ToElement()).ToArray();
        }

        internal ConstructorInfoElement[] GetConstructorInfoElements()
        {
            return this.Type.GetConstructors(TypeElement.bindingFlags).Select(c => c.ToElement()).ToArray();
        }

        internal PropertyInfoElement[] GetPropertyInfoElements()
        {
            return this.Type.GetProperties(TypeElement.bindingFlags).Select(c => c.ToElement()).ToArray();
        }

        internal MethodInfoElement[] GetMethodInfoElements()
        {
            return this.Type.GetMethods(TypeElement.bindingFlags)
                .Except(this.Type.GetProperties(TypeElement.bindingFlags).SelectMany(p => p.GetAccessors(true)))
                .Select(m => m.ToElement())
                .ToArray();
        }

        internal EventInfoElement[] GetEventInfoElements()
        {
            return this.Type.GetEvents(TypeElement.bindingFlags).Select(e => e.ToElement()).ToArray();
        }
    }
}
