using System;
using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    internal class TypeWithEvents
    {
        public static EventInfo LocalEvent
        {
            get
            {
                return typeof (TypeWithEvents).GetEvent("TheEvent");
            }
        }

        public static EventInfo OtherEvent
        {
            get
            {
                return typeof(TypeWithEvents).GetEvent("TheOtherEvent");
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