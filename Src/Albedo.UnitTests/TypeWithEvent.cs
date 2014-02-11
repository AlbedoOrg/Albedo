using System;
using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    internal class TypeWithEvent
    {
        public static EventInfo LocalEvent
        {
            get
            {
                return typeof (TypeWithEvent).GetEvent("TheEvent");
            }
        }

        public static EventInfo OtherEvent
        {
            get
            {
                return typeof(TypeWithEvent).GetEvent("TheOtherEvent");
            }
        }

        public event EventHandler TheEvent
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        public event EventHandler TheOtherEvent
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }
    }
}