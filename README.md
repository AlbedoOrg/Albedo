[![Build status](https://ci.appveyor.com/api/projects/status/v62om0j3hqiaoedx/branch/master?svg=true)](https://ci.appveyor.com/project/Albedo/albedo/branch/master) [![NuGet version](https://img.shields.io/nuget/v/Albedo.svg)](https://www.nuget.org/packages/Albedo)

# Announcement

Recently the ownership of this project has been passed from Mark Seemann to maintainers of the AlbedoOrg organization. To reflect that change the default namespace prefix and assembly name were changed from `Ploeh.Albedo` to `Albedo`. Please use the text replace feature of your IDE to quickly fix all the namespace imports.

# Albedo

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

Albedo follows [Semantic Versioning 2.0.0](http://semver.org/spec/v2.0.0.html).

## Where do you get it?

Obviously, the source code is available here on GitHub, but you can [download the compiled library with NuGet](http://www.nuget.org/packages/Albedo).

## What problem does it address?

Albedo addresses the problem that the .NET Reflection API (mainly in [System.Reflection](http://msdn.microsoft.com/en-us/library/system.reflection.aspx)) doesn't provide a set of good abstractions. As an example, both [PropertyInfo](http://msdn.microsoft.com/en-us/library/system.reflection.propertyinfo.aspx) and [FieldInfo](http://msdn.microsoft.com/en-us/library/system.reflection.fieldinfo.aspx) expose `GetValue` and `SetValue` functions, yet despite their similarities, these functions are defined directly on each of those two classes, so there's no polymorphic API to read a value from a property *or* field, or assign a value to a property *or* field.

It's also difficult to extract the type of a property or field in a polymorphic manner, because the type of a property is defined by the [PropertyType](http://msdn.microsoft.com/en-us/library/system.reflection.propertyinfo.propertytype.aspx) property, while the type of a field is defined by the [FieldType](http://msdn.microsoft.com/en-us/library/system.reflection.fieldinfo.fieldtype.aspx) property.

At least PropertyInfo and FieldInfo both derive from the abstract [MemberInfo](http://msdn.microsoft.com/en-us/library/system.reflection.memberinfo.aspx) class, so they still have a *little* in common. However, if you want to compare any of these to, say, a [ParameterInfo](http://msdn.microsoft.com/en-us/library/system.reflection.parameterinfo.aspx) instance, the highest common ancestor is `Object`!

While you can define your own interfaces or delegates to deal with this lack of polymorphism in the Reflection API, Albedo offers a **common set of abstractions** over the Reflection API. These abstractions are based on tried-and-true design patterns, and as the code examples below demonstrate, are very flexible.

## Who cares?

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

## How does it work?

In OOD, whenever you find yourself in a situation where you need to provide a consistent API over a *final*, known set of *concrete* classes, the much-derided [Visitor pattern](http://en.wikipedia.org/wiki/Visitor_pattern) is very useful. Albedo is based on an `IReflectionVisitor<T>` interface that visits [Adapters](http://en.wikipedia.org/wiki/Adapter_pattern) over the known Reflection types, such as PropertyInfo, ParameterInfo, etc.

While Albedo defines the `IReflectionVisitor<T>` interface, it also provides a `ReflectionVisitor<T>` abstract base class you can use to visit only those `IReflectionElement` Adapters you care about.

All examples shown here can be found in the `Scenario` class in the Albedo code base's unit tests.

### Collecting values

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

> **Note:** `ValueCollectingVisitor` has turned out to be such a generally useful implementation that it's now available by default in Albedo. Thus, you don't have to implement it yourself, but remains here as an example of how to use Albedo.

Here's a more complicated example that uses the `ValueCollectingVisitor` to read all public properties and fields from a `TimeSpan` instance:

```C#
var ts = new TimeSpan(2, 4, 3, 8, 9);
var elements = ts.GetType().GetPublicPropertiesAndFields().ToArray();

var actual = elements.Accept(new ValueCollectingVisitor(ts));

var actualValues = actual.Value.ToArray();
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

### Assigning values

Albedo wouldn't be a truly flexible library if the only thing it could do is to read values from properties and fields. It can also do other things; closely related to *reading* values is *assigning* values.

Assume that you have a class like this:

```C#
    public class ClassWithWritablePropertiesAndFields<T>
    {
        public T Field1;

        public T Field2;

        public T Property1 { get; set; }

        public T Property2 { get; set; }
    }
```

If you want to assign a value to all fields and properties, you can do it like this:

```C#
var t = new ClassWithWritablePropertiesAndFields<int>();
var elements = t.GetType().GetPublicPropertiesAndFields().ToArray();

var actual = elements.Accept(new ValueWritingVisitor(t));
actual.Value(42);
            
Assert.Equal(42, t.Field1);
Assert.Equal(42, t.Field2);
Assert.Equal(42, t.Property1);
Assert.Equal(42, t.Property2);
```

Apart from `ClassWithWritablePropertiesAndFields<T>`, the only custom type you'd have to provide to enable this feature is the `ValueWritingVisitor`:

```C#
public class ValueWritingVisitor : ReflectionVisitor<Action<object>>
{
    private readonly object target;
    private readonly Action<object>[] actions;

    public ValueWritingVisitor(
        object target,
        params Action<object>[] actions)
    {
        this.target = target;
        this.actions = actions;
    }

    public override Action<object> Value
    {
        get
        {
            return x =>
            {
                foreach (var a in this.actions)
                    a(x);
            };
        }
    }

    public override IReflectionVisitor<Action<object>> Visit(
        FieldInfoElement fieldInfoElement)
    {
        Action<object> a =
            v => fieldInfoElement.FieldInfo.SetValue(this.target, v);
        return new ValueWritingVisitor(
            this.target,
            this.actions.Concat(new[] { a }).ToArray());
    }

    public override IReflectionVisitor<Action<object>> Visit(
        PropertyInfoElement propertyInfoElement)
    {
        Action<object> a =
            v => propertyInfoElement.PropertyInfo.SetValue(
                this.target,
                v,
                null);
        return new ValueWritingVisitor(
            this.target,
            this.actions.Concat(new[] { a }).ToArray());
    }
}
```

As you can see in this example, the `ValueWritingVisitor` collects an array of `Action`s that can be executed once the Visitor has visited all the elements. (Once again, for demonstration purposes, the above sample code assumes that each assignment is possible, which may very well not be the case if the property or field is read-only, or if the types don't match.)

### Comparison

Another, more advanced scenario (and [the scenario that ultimately sparked the Albedo project](https://github.com/AutoFixture/AutoFixture/pull/171#discussion_r6505484) in the first place) is to compare disparate Reflection elements.

Imagine, for example, that you want to match a constructor's `ParameterInfo` to a `PropertyInfo` that exposes the value originally passed to the constructor ([Structural Inspection](http://blog.ploeh.dk/2013/04/04/structural-inspection)). For example, you might want to check whether the [Version](http://msdn.microsoft.com/en-us/library/system.version.aspx) `major` constructor argument matches the [Major](http://msdn.microsoft.com/en-us/library/system.version.major.aspx) property.

Since this is a comparison, it sounds like a job for [`IEqualityComparer<T>`](http://msdn.microsoft.com/en-us/library/ms132151.aspx), but what should `T` be?

The challenge is that `ParameterInfo` shares no API with `PropertyInfo`, and additionally, you'll want to make a case-insensitive comparison of their names.

Albedo can address this scenario with its abstractions on top of Reflection, enabling you to declare an `IEqualityComparer<IReflectionElement>` and use it like this:

```C#
[Theory]
[InlineData("Major", "major", true)]
[InlineData("Major", "minor", false)]
[InlineData("Minor", "major", false)]
[InlineData("Minor", "minor", true)]
public void MatchContructorArgumentAgainstReadOnlyProperty(
    string propertyName, string parameterName, bool expected)
{
    var prop = new PropertyInfoElement(
        typeof(Version).GetProperty(propertyName));
    var param = new ParameterInfoElement(
        typeof(Version)
            .GetConstructor(new[] { typeof(int), typeof(int) })
            .GetParameters()
            .Where(p => p.Name == parameterName)
            .Single());

    var actual = 
        new SemanticElementComparer(new SemanticReflectionVisitor())
            .Equals(prop, param);

    Assert.Equal(expected, actual);
}
```

The custom `SemanticReflectionVisitor` collects comparison values, based on properties, fields, and parameters:

```C#
public class SemanticReflectionVisitor : ReflectionVisitor<IEnumerable>
{
    private readonly object[] values;

    public SemanticReflectionVisitor(
        params object[] values)
    {
        this.values = values;
    }

    public override IEnumerable Value
    {
        get { return this.values; }
    }

    public override IReflectionVisitor<IEnumerable> Visit(
        FieldInfoElement fieldInfoElement)
    {
        var v = new SemanticComparisonValue(
            fieldInfoElement.FieldInfo.Name,
            fieldInfoElement.FieldInfo.FieldType);
        return new SemanticReflectionVisitor(
            this.values.Concat(new[] { v }).ToArray());
    }

    public override IReflectionVisitor<IEnumerable> Visit(
        ParameterInfoElement parameterInfoElement)
    {
        var v = new SemanticComparisonValue(
            parameterInfoElement.ParameterInfo.Name,
            parameterInfoElement.ParameterInfo.ParameterType);
        return new SemanticReflectionVisitor(
            this.values.Concat(new[] { v }).ToArray());
    }

    public override IReflectionVisitor<IEnumerable> Visit(
        PropertyInfoElement propertyInfoElement)
    {
        var v = new SemanticComparisonValue(
            propertyInfoElement.PropertyInfo.Name,
            propertyInfoElement.PropertyInfo.PropertyType);
        return new SemanticReflectionVisitor(
            this.values.Concat(new[] { v }).ToArray());
    }
}
```

The `SemanticComparisonValue` class is a custom [Value Object](http://martinfowler.com/bliki/ValueObject.html) that collects the names and types of the fields, properties, or parameters:

```C#
public class SemanticComparisonValue
{
    private readonly string name;
    private readonly Type type;

    public SemanticComparisonValue(string name, Type type)
    {
        this.name = name;
        this.type = type;
    }

    public override bool Equals(object obj)
    {
        var other = obj as SemanticComparisonValue;
        if (other == null)
            return base.Equals(obj);

        return object.Equals(this.type, other.type)
            && string.Equals(this.name, other.name,
                StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        return
            this.name.ToUpperInvariant().GetHashCode() ^
            this.type.GetHashCode();
    }
}
```

The important quality of the `SemanticComparisonValue` class is that it implements custom equality comparison, so that it compares the names in a case-insensitive manner.

With these two custom classes, it's fairly straight-forward to implement the EqualityComparer:

```C#
public class SemanticElementComparer : IEqualityComparer<IReflectionElement>
{
    private readonly IReflectionVisitor<IEnumerable> visitor;

    public SemanticElementComparer(
        IReflectionVisitor<IEnumerable> visitor)
    {
        this.visitor = visitor;
    }

    public bool Equals(IReflectionElement x, IReflectionElement y)
    {
        var values = new CompositeReflectionElement(x, y)
            .Accept(this.visitor)
            .Value
            .Cast<object>()
            .ToArray();
        return values.Length == 2
            && values.Distinct().Count() == 1;
    }

    public int GetHashCode(IReflectionElement obj)
    {
        return obj
            .Accept(this.visitor)
            .Value
            .Cast<object>()
            .Single()
            .GetHashCode();
    }
}
```

Since the `Equals` method compares exactly *two* elements, there should also be *exactly two* collected elements. This isn't guaranteed, because `x` or `y` could also be instances of `TypeElement` or `MethodInfoElement`, and `SemanticReflectionVisitor` doesn't collect those. On the other hand, while there should be exactly two instances, they must be equal to each other for the `Equals` method to return `true`; thus, if the count of *distinct* values is *one*, they are equal to each other, since the `Distinct` method uses object equality, as implemented by each element's `Equals` method (and recall that `SemanticComparisonValue` overrides `Equals`).

### Strongly-typed queries of type members

Sometimes, you'd like to get a `PropertyInfo` instance for a particular class' property, or similar for fields and methods. The problem with the BCL Reflection API is that you have to refer to the property by naming it with a string, which isn't refactoring-safe.

As a convenience, Albedo provides a couple of strongly typed queries over type members:

```C#
MethodInfo mi = new Methods<Version>().Select(v => v.ToString());
Assert.Equal("ToString", mi.Name);
```

Due to the signature of the `Select` method, you can alternatively write the above query as a LINQ query!

```C#
MethodInfo mi = from v in new Methods<Version>()
                select v.ToString();
Assert.Equal("ToString", mi.Name);
```

In addition to strongly-typed queries over methods, Albedo also provides strongly-typed queries over fields and properties.
