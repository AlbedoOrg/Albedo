using System;
using System.Reflection;

namespace Ploeh.Albedo
{
    /// <summary>
    /// An <see cref="IReflectionElement"/> representing a <see cref="LocalVariableInfo"/> which
    /// can be visited by an <see cref="IReflectionVisitor{T}"/> implementation.
    /// </summary>
    public class LocalVariableInfoElement : IReflectionElement
    {
        /// <summary>
        /// Gets the <see cref="System.Reflection.LocalVariableInfo"/> instance this element represents.
        /// </summary>
        public LocalVariableInfo LocalVariableInfo { get; private set; }

        /// <summary>
        /// Constructs a new instance of the <see cref="LocalVariableInfoElement"/> which represents
        /// the specified <see cref="System.Reflection.LocalVariableInfo"/>.
        /// </summary>
        /// <param name="localVariableInfo">The <see cref="System.Reflection.LocalVariableInfo"/> this 
        /// element represents.</param>
        public LocalVariableInfoElement(LocalVariableInfo localVariableInfo)
        {
            if (localVariableInfo == null) throw new ArgumentNullException("localVariableInfo");
            this.LocalVariableInfo = localVariableInfo;
        }

        /// <summary>
        /// Accepts the provided <see cref="IReflectionVisitor{T}"/>, by calling the
        /// appropriate strongly-typed <see cref="IReflectionVisitor{T}.Visit(LocalVariableInfoElement)"/>
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
    }
}
