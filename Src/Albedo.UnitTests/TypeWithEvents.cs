using System;
using System.Reflection;

namespace Albedo.UnitTests
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

        public static EventInfo PrivateEvent
        {
            get
            {
                return typeof(TypeWithEvents).GetEvent(
                    "ThePrivateEvent", BindingFlags.Instance | BindingFlags.NonPublic);
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

        private event EventHandler ThePrivateEvent
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }
    }
}