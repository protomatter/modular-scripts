# Modular Javacript and Stylesheets in C# #

Inspired by: https://github.com/ronnieoverby/MvcAssetManager

**Blog Post:** http://www.protomatter.co.uk/blog/development/2017/01/modular-javacript-and-stylesheets-in-c-sharp/ - 
a little more information about this approach.

---

A simple approach to modular Javascript registration in C#. This Solution demonstrates a straight forward 
method for loading Javascript files and stylesheets from multiple C# projects at runtime.

This method involves embedding the required assets in dll files, using a singleton 'AppAssetStore' class 
to store a list of these assets at startup, a [VirtualPathProvider](https://msdn.microsoft.com/en-us/library/system.web.hosting.virtualpathprovider(v=vs.110).aspx) to give us access to the embedded assets,
and a couple of HtmlHelpers to simplify rendering of these assets to a MVC web page.

## Project Parts
### Infrastructure
This project contains the `AppAssetStore` class and associated helper classes. These classes allow the
registration and rendering of required assets. Registration of an asset would typically take place at startup 
via [WebActivatorEx](https://www.nuget.org/packages/WebActivatorEx/) - this allows independent modules to 
self-register their assets, for example:

```csharp
using MyModule;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(AssetRegistration), "Start")]

namespace MyModule {
    using Infrastructure;

    public static class AssetRegistration {
        public static void Start() {
            var assetStore = AppAssetStore.GetInstance;
            assetStore.Require(new AppAsset("~/assets/js/mymodulescript.js", AssetType.ScriptFile), false);
        }
    }
}
```

### Module 1 and Module 2
Each of these projects references the Infrastructure project, includes an embedded Javascript file 
and an `AssetRegistration` class as described above. These are super simple examples of what a module could be, 
my own usage loads Angular modules which are injected into a host Angular app. In these two example modules we
just use `console.log()` in the Javascripts to print to the browser console.


### Module Host
The module host project pulls all the parts together, it references the Infrastructure project and the two 
module projects. It uses the `AppAssetStore` to register JavaScript and a stylesheet of it's own and 
configures the [EmbeddedResourceVirtualPathProvider](https://www.nuget.org/packages/EmbeddedResourceVirtualPathProvider) 
by [Harry McIntyre](http://www.adverseconditionals.com/) which gives us access to the embedded assets in the
module assemblies.

In this project we have a default controller and a view. The view uses a couple of HtmlHelpers from the 
Infrastructure project to actually render the registered assets, namely `@Html.RenderAppStyles()` and 
`@Html.RenderAppScripts()`. In the basic example provided here our view looks like this: 

```html
@using Infrastructure
<!DOCTYPE html>
<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
    @Html.RenderAppStyles()
</head>
<body>
    <h1>Asset Injection Test</h1>
    @Html.RenderAppScripts()
</body>
</html>
```

## Asset Render Order

There is limited support for ordering of assets. Assets added to the asset store with the `forLayout` 
parameter of the `Require` method set to `true` will be rendered before those set to false, for example:

```csharp
var assetStore = AppAssetStore.GetInstance;
// Add the jquery.js file as a 'layout' asset - all layout assets are rendered first 
assetStore.Require(new AppAsset("~/assets/js/jquery.js", AssetType.ScriptFile), true);
// Add the mymodulescript.js file as a 'non-layout' asset, these are rendered after the layout assets
assetStore.Require(new AppAsset("~/assets/js/mymodulescript.js", AssetType.ScriptFile), false);
```

# Improvements
There are likely areas for improvement on this approach and code:

* **Assets are not rendered in any particular order** - the forLayout parameter discussed allows 
crude ordering, but an order parameter could perhaps be implemented.