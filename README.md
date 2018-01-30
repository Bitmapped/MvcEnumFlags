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
### Getting started
1. Add **MvcEnumFlags.dll** as a reference in your project.
2. You must register the model binder for each enum you are going to use in **global.asax.cs** under `ApplicationStart()`.  See below for examples.

### Registering the model binder

You must register the model binder for each enum type in **global.asax.cs** so that MVC knows to use `EnumFlagsModelBinder()` with it.

In the `Application_Start()` method, add the following line:
```
ModelBinders.Binders.Add(typeof(MyEnumType), new EnumFlagsModelBinder());
```

### Add the namespace
You must let Razor know to use the `MvcEnumFlags` namespace. You can do this in one of two ways.

#### Option 1: Add namespace to **Views\web.config**
Edit your **Views\web.config** file to include the namespace:
```
<configuration>
  <system.web.webPages.razor>
    <pages>
      <namespaces>
        <add namespace="MvcEnumFlags" />
      </namespaces>
    </pages>
  </system.web.webPages.razor>
</configuration>
```

#### Option 2: Add a using directive to your views
Instead of editing the **Views\web.config** file, you can add a using directive directly to the Razor views that use MvcEnumFlags:
```
@using MvcEnumFlags
````

### Using the flags

#### In the controller

There is nothing special to do in the controller.  With the model binder registered in **global.asax.cs**, MVC will automatically map the data back onto your model.

#### In the view

##### Using CheckBoxesForEnumFlagsFor()
To include enum checkboxes in your view, you can call the `Html.CheckBoxesForEnumFlagsFor()` method directly:
```
@Html.CheckBoxesForEnumFlagsFor(model => model.MyEnumTypeProperty)
```

##### Using EditorFor()
If you prefer using `Html.EditorFor()` in your views, annotate your property with the `UIHint` attribute:
```
[UIHint("EnumCheckboxes")]
public MyEnumType MyEnumTypeProperty { get; set; }
```
        
You can then create an EditorTemplate named **Views\Shared\EditorTemplates\EnumCheckboxes.cshtml** that MVC will use to display the editor for properties with a `UIHint` attribute of `EnumCheckboxes`:
```
@Html.CheckBoxesForEnumFlagsFor(m => m)
```

In your view, call `Html.EditorFor()` as you would for a textbox or other regular property:
```
@Html.EditorFor(model => model.MyEnumTypeProperty)
```
