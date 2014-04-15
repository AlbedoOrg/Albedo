using System;
using System.Globalization;
using System.Reflection;

namespace Ploeh.Albedo
{
    /// <summary>
    /// An <see cref="IReflectionElement"/> representing a <see cref="EventInfo"/> which
    /// can be visited by an <see cref="IReflectionVisitor{T}"/> implementation.
    /// </summary>
    public class EventInfoElement : IReflectionElement
    {
        /// <summary>
        /// Gets the <see cref="System.Reflection.EventInfo"/> instance this element represents.
        /// </summary>
        public EventInfo EventInfo { get; private set; }

        /// <summary>
        /// Constructs a new instance of the <see cref="EventInfoElement"/> which represents
        /// the specified <see cref="System.Reflection.EventInfo"/>.
        /// </summary>
        /// <param name="eventInfo">The <see cref="System.Reflection.EventInfo"/> this 
        /// element represents.</param>
        public EventInfoElement(EventInfo eventInfo)
        {
            if (eventInfo == null) throw new ArgumentNullException("eventInfo");
            this.EventInfo = eventInfo;
        }

        /// <summary>
        /// Accepts the provided <see cref="IReflectionVisitor{T}"/>, by calling the
        /// appropriate strongly-typed <see cref="IReflectionVisitor{T}.Visit(EventInfoElement)"/>
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
        /// Two instances of <see cref="EventInfoElement" /> are 
        /// considered to be equal if their <see cref="EventInfo" />
        /// values are equal.
        /// </para>
        /// </remarks>
        public override bool Equals(object obj)
        {
            var other = obj as EventInfoElement;
            if (other == null)
                return false;

            return object.Equals(this.EventInfo, other.EventInfo);
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
            return this.EventInfo.GetHashCode();
        }

        /// <summary>
        /// Gets the string representation of the <see cref="EventInfo"/>
        /// suitable for development / debugging display purposes.
        /// </summary>
        /// <returns>The string representation of the contained
        /// <see cref="EventInfo"/></returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.CurrentCulture, "[[{0}]] ({1})", this.EventInfo, "event");
        }

        internal MethodInfoElement GetAddMethodInfoElement()
        {
            return this.EventInfo.GetAddMethod(true).ToElement();
        }

        internal MethodInfoElement GetRemoveMethodInfoElement()
        {
            return this.EventInfo.GetRemoveMethod(true).ToElement();
        }
    }
}
