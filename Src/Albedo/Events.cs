using System;
using System.Linq;
using System.Reflection;
using Mono.Reflection;

namespace Ploeh.Albedo
{
    public class Events<T>
    {
        public EventInfo Select(Action<T> eventSelector)
        {
            var instructions = eventSelector.Method.GetInstructions();
            var method = instructions.Select(i => i.Operand).OfType<MethodInfo>().Single();
            return typeof(T).GetEvent(method.Name.Replace("add_", string.Empty));
        }
    }
}