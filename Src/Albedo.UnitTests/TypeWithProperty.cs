using System;
using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    internal class TypeWithProperty
    {
        public static PropertyInfo Property
        {
            get
            {
                return typeof (TypeWithProperty).GetProperty("TheProperty");
            }
        }

        public static PropertyInfo OtherProperty
        {
            get
            {
                return typeof(TypeWithProperty).GetProperty("TheOtherProperty");
            }
        }

        public static PropertyInfo ReadOnlyProperty
        {
            get
            {
                return typeof(TypeWithProperty).GetProperty("TheReadOnlyProperty");
            }
        }

        public static PropertyInfo WriteOnlyProperty
        {
            get
            {
                return typeof(TypeWithProperty).GetProperty("TheWriteOnlyProperty");
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