Albedo
======

A .NET library targeted at making Reflection programming more consistent, using a common set of abstractions and utilities.

This examples uses a [PropertyInfo](http://msdn.microsoft.com/en-us/library/system.reflection.propertyinfo.aspx) to read a value off a [System.Version](http://msdn.microsoft.com/en-us/library/system.version.aspx) instance:

```C#
PropertyInfo pi = from v in new Properties<Version>()
                  select v.Minor;
var version = new Version(2, 7);
var visitor = new ValueCollectingVisitor(version);

var actual = new PropertyInfoElement(pi).Accept(visitor);

Assert.Equal(version.Minor, actual.Value.OfType<int>().First());
```

More examples further down.

Which problem does it address?
------------------------------

Albedo addresses the problem that the .NET Reflection API (mainly in [System.Reflection](http://msdn.microsoft.com/en-us/library/system.reflection.aspx)) doesn't provide a set of good abstractions. As an example, both [PropertyInfo](http://msdn.microsoft.com/en-us/library/system.reflection.propertyinfo.aspx) and [FieldInfo](http://msdn.microsoft.com/en-us/library/system.reflection.fieldinfo.aspx) expose `GetValue` and `SetValue` functions, but despite their similarities, these functions are defined directly on each of those two classes, so there's no polymorphic API to read a value from a property *or* field, or assign a value to a property *or* field.

It's also difficult to extract the type of a property or field in a polymorphic manner, because the type of a property is defined by the [PropertyType](http://msdn.microsoft.com/en-us/library/system.reflection.propertyinfo.propertytype.aspx) property, while the type of a field is defined by the [FieldType](http://msdn.microsoft.com/en-us/library/system.reflection.fieldinfo.fieldtype.aspx) property.

At least PropertyInfo and FieldInfo both derive from the abstract [MemberInfo](http://msdn.microsoft.com/en-us/library/system.reflection.memberinfo.aspx) class, so they still have a *little* in common. However, if you want to compare any of these to, say, a [ParameterInfo](http://msdn.microsoft.com/en-us/library/system.reflection.parameterinfo.aspx) instance, the highest common ancestor is `object`!

While you can define your own interfaces or delegates to deal with this lack of polymorphism in the Reflection API, Albedo offers a **common set of abstractions** over the Reflection API. These abstractions are based on tried-and-true design patterns, and as the code examples below demonstrate, are very flexible.

Who cares?
----------

People who write lots of Reflection code might benefit from Albedo: Programmers of
- ORMs
- Auto-Mappers
- dynamic mock libraries
- DI Containers
- unit testing frameworks
- etc.

Instead of defining a constrained and specific set of interfaces for each such tool, Albedo can provide a common, reusable abstraction over Reflection, enabling a more open architecture.

Albedo was born out of [a need for such abstractions in AutoFixture](https://github.com/AutoFixture/AutoFixture/pull/171#discussion_r6505484). While it's easy to extract a specific interface to address a specific need, you always run the risk of defining an interface that can only be used in one very specific situation; such an [interface may not be a good abstraction](http://blog.ploeh.dk/2010/12/02/Interfacesarenotabstractions).

If you don't write a lot of Reflection code, you probably don't need Albedo.

How does it work?
-----------------

In OOD, whenever you find yourself in a situation where you need to provide a consistent API over a *final*, known set of *concrete* classes, the much-derided [Visitor pattern](http://en.wikipedia.org/wiki/Visitor_pattern) is very useful. Albedo is based on an `IReflectionVisitor<T>` interface that visits [Adapters](http://en.wikipedia.org/wiki/Adapter_pattern) over the known Reflection types, such as PropertyInfo, ParameterInfo, etc.

While Albedo defines the `IReflectionVisitor<T>` interface, it also provides a `ReflectionVisitor<T>` abstract base class you can use to only visit those `IReflectionElement` Adapters you care about.

The initial example uses a sample `ValueCollectingVisitor`, which is implemented like this:

```C#
public class ValueCollectingVisitor : ReflectionVisitor<IEnumerable>
{
    private readonly object target;
    private readonly object[] values;

    public ValueCollectingVisitor(object target, params object[] values)
    {
        this.target = target;
        this.values = values;
    }

    public override IEnumerable Value
    {
        get { return this.values; }
    }

    public override IReflectionVisitor<IEnumerable> Visit(
        FieldInfoElement fieldInfoElement)
    {
        var value = fieldInfoElement.FieldInfo.GetValue(this.target);
        return new ValueCollectingVisitor(
            this.target,
            this.values.Concat(new[] { value }).ToArray());
    }

    public override IReflectionVisitor<IEnumerable> Visit(
        PropertyInfoElement propertyInfoElement)
    {
        var value = 
            propertyInfoElement.PropertyInfo.GetValue(this.target, null);
        return new ValueCollectingVisitor(
            this.target,
            this.values.Concat(new[] { value }).ToArray());
    }
}
```

Notice that this Visitor only collects information about the values of properties and fields, and not (say) the return value of method calls. (Since this is sample code, it implicitly assumes that `PropertyInfo.GetValue` will succeed, which will not be the case if the property is a write-only property. However, it's trivial to add a check to see if the property can be read.)

Here's a more complicated example that uses the sample `ValueCollectingVisitor` to read all public properties and fields from a `TimeSpan` instance:

```C#
var ts = new TimeSpan(2, 4, 3, 8, 9);
var flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
var elements = ts.GetType()
    .GetProperties(flags)
    .Select(pi => new PropertyInfoElement(pi))
    .Cast<IReflectionElement>()
    .Concat(ts.GetType()
        .GetFields(flags)
        .Select(fi => new FieldInfoElement(fi))
        .Cast<IReflectionElement>())
    .ToArray();
var visitor = new ValueCollectingVisitor(ts);

var actual =
    new CompositeReflectionElement(elements).Accept(visitor);

var actualValues = actual.Value.Cast<object>().ToArray();
Assert.Equal(elements.Length, actualValues.Length);
Assert.Equal(1, actualValues.Count(ts.Days.Equals));
Assert.Equal(1, actualValues.Count(ts.Hours.Equals));
Assert.Equal(1, actualValues.Count(ts.Milliseconds.Equals));
Assert.Equal(1, actualValues.Count(ts.Minutes.Equals));
Assert.Equal(1, actualValues.Count(ts.Seconds.Equals));
Assert.Equal(1, actualValues.Count(ts.Ticks.Equals));
Assert.Equal(1, actualValues.Count(ts.TotalDays.Equals));
Assert.Equal(1, actualValues.Count(ts.TotalHours.Equals));
Assert.Equal(1, actualValues.Count(ts.TotalMilliseconds.Equals));
Assert.Equal(1, actualValues.Count(ts.TotalMinutes.Equals));
Assert.Equal(1, actualValues.Count(ts.TotalSeconds.Equals));
Assert.Equal(1, actualValues.Count(TimeSpan.MaxValue.Equals));
Assert.Equal(1, actualValues.Count(TimeSpan.MinValue.Equals));
Assert.Equal(1, actualValues.Count(TimeSpan.TicksPerDay.Equals));
Assert.Equal(1, actualValues.Count(TimeSpan.TicksPerHour.Equals));
Assert.Equal(1, actualValues.Count(TimeSpan.TicksPerMillisecond.Equals));
Assert.Equal(1, actualValues.Count(TimeSpan.TicksPerMinute.Equals));
Assert.Equal(1, actualValues.Count(TimeSpan.TicksPerSecond.Equals));
Assert.Equal(1, actualValues.Count(TimeSpan.Zero.Equals));
```

The `ValueCollectingVisitor` class is the only custom code written to implement this behaviour. The rest of the types in the above code example is either defined in the BCL (`TimeSpan`, etc.) or provided by Albedo (`PropertyInfoElement`, `FieldInfoElement`, `CompositeReflectionElement`).
