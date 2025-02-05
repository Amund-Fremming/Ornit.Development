# Notes

## Configure 
```C#
services.AddTypeScriptSupport(options =>
{
    options.ClientLogging = false;
});
```

## Typescript Interface Generation
For the interface generation to work you need to tag your classes, records or structs with `ITypeScriptModel` interface.
There is also build in support for generating the props in primary constructors.
The generator will automatically generate enum types for your enums without having to do anything.

## TypeScript Api-Client Generation
This works out of the box
