# MvcEnumFlags
ASP.NET MVC model binder and HtmlHelper extensions for working with flags-attribute enum values.

## What's inside
This project includes two C# classes:
- EnumFlagsModelBinder: Model binder for mapping values from submitting forms onto enum object.
- CheckBoxesForEnumFlagsFor: HtmlHelper extension that creates checkboxes for flags-attribute enum values.

## System requirements
1. NET Framework 4

## NuGet availability
This project is available on [NuGet](https://www.nuget.org/packages/MvcEnumFlags/).

## Usage instructions
### Getting Started
1. Add **MvcEnumFlags.dll** as a reference in your project.
2. You must register the model binder for each enum you are going to use in **global.asax.cs** under `ApplicationStart()`.  See below for examples.

### Registering the Model Binder

You must register the model binder for each enum type in global.asax.cs so that MVC knows to use `EnumFlagsModelBinder()` with it.

In the `Application_Start()` method, add the following line:
```
ModelBinders.Binders.Add(typeof(MyEnumType), new EnumFlagsModelBinder());
```

### Using the Flags

#### In the controller

There is nothing special to do in the controller.  With the model binder registered in **global.asax.cs**, MVC will automatically map the data back onto your model.

#### In the view

MVC will automatically call MvcEnumFlags to display enum types you've registered in **global.asax.cs** when you use `Html.EditorFor()`.

The following is an example of displaying the flags checkbox with a registered enum.
```
@Html.EditorFor(model => model.MyEnumTypeProperty)
```
