# Modular Javacript and Stylesheets in C#

A simple approach to modular Javascript registration in C#. This Solution demonstrates a straight forward 
method for loading Javascript files and stylesheets from multiple C# projects at runtime.

This method involves embedding the required assets in dll files, using a singleton 'AppAssetStore' class 
to store a list of these assets at startup, a VirtualPathProvider to give us access to the embedded assets,
and a couple of HtmlHelpers to simplify rendering of these assets to a web page.

## Project Parts
### Infrastructure
This project contains the AppAssetStore class and associated classes and helpers. These classes allow the
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



### Module Host



### Module 1

### Module 2

For more information see 

