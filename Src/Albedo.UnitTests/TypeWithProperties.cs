using System;
using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    internal class TypeWithProperties
    {
        public static PropertyInfo Property
        {
            get
            {
                return typeof (TypeWithProperties).GetProperty("TheProperty");
            }
        }

        public static PropertyInfo OtherProperty
        {
            get
            {
                return typeof(TypeWithProperties).GetProperty("TheOtherProperty");
            }
        }

        public static PropertyInfo ReadOnlyProperty
        {
            get
            {
                return typeof(TypeWithProperties).GetProperty("TheReadOnlyProperty");
            }
        }

        public static PropertyInfo WriteOnlyProperty
        {
            get
            {
                return typeof(TypeWithProperties).GetProperty("TheWriteOnlyProperty");
            }
        }

        public int TheProperty { get; set; }
        public int TheOtherProperty { get; set; }
        public int TheReadOnlyProperty
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        public int TheWriteOnlyProperty
        {
            set
            {
                throw new NotSupportedException();
            }
        }
    }
}