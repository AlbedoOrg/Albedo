﻿using System;
using System.Reflection;

namespace Ploeh.Albedo
{
    /// <summary>
    /// An <see cref="IReflectionElement"/> representing a <see cref="ParameterInfo"/> which
    /// can be visited by an <see cref="IReflectionVisitor{T}"/> implementation.
    /// </summary>
    public class ParameterInfoElement : IReflectionElement
    {
        /// <summary>
        /// Gets the <see cref="System.Reflection.ParameterInfo"/> instance this element represents.
        /// </summary>
        public ParameterInfo ParameterInfo { get; private set; }

        /// <summary>
        /// Constructs a new instance of the <see cref="ParameterInfoElement"/> which represents
        /// the specified <see cref="System.Reflection.ParameterInfo"/>.
        /// </summary>
        /// <param name="parameterInfo">The <see cref="System.Reflection.ParameterInfo"/> this 
        /// element represents.</param>
        public ParameterInfoElement(ParameterInfo parameterInfo)
        {
            if (parameterInfo == null) throw new ArgumentNullException("parameterInfo");
            this.ParameterInfo = parameterInfo;
        }

        /// <summary>
        /// Accepts the provided <see cref="IReflectionVisitor{T}"/>, by calling the
        /// appropriate strongly-typed <see cref="IReflectionVisitor{T}.Visit(ParameterInfoElement)"/>
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
