Albedo
======

A .NET library targeted at making Reflection programming more consistent, using a common set of abstractions and utilities.

Which problem does it address?
------------------------------

Albedo addresses the problem that the .NET Reflection API (mainly in [System.Reflection](http://msdn.microsoft.com/en-us/library/system.reflection.aspx)) doesn't provide a set of good abstractions. As an example, both [PropertyInfo](http://msdn.microsoft.com/en-us/library/system.reflection.propertyinfo.aspx) and [FieldInfo](http://msdn.microsoft.com/en-us/library/system.reflection.fieldinfo.aspx) expose `GetValue` and `SetValue` functions, but despite their similarities, these functions are defined directly on each of those two classes, so there's no polymorphic API to read a value from a property *or* field, or assign a value to a property *or* field.

It's also difficult to extract the type of a property or field in a polymorphic manner, because the type of a property is defined by the [PropertyType](http://msdn.microsoft.com/en-us/library/system.reflection.propertyinfo.propertytype.aspx) property, while the type of a field is defined by the [FieldType](http://msdn.microsoft.com/en-us/library/system.reflection.fieldinfo.fieldtype.aspx) property.

At least PropertyInfo and FieldInfo both derive from the abstract [MemberInfo](http://msdn.microsoft.com/en-us/library/system.reflection.memberinfo.aspx) class, so they still have a *little* in common. However, if you want to compare any of these to, say, a [ParameterInfo](http://msdn.microsoft.com/en-us/library/system.reflection.parameterinfo.aspx) instance, the highest common ancestor is `object`!

While you can define your own interfaces or delegates to deal with this lack of polymorphism in the Reflection API, Albedo offers a **common set of abstractions** over the Reflection API. These abstractions are based on tried-and-true design patterns, and as the code examples below demonstrate, are very flexible.
