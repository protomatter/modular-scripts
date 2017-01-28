using ModuleHost;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(AssetRegistration), "Start")]

namespace ModuleHost {
    using Infrastructure;

    public static class AssetRegistration {
        public static void Start() {
            var assetStore = AppAssetStore.GetInstance;
            assetStore.Require(new Asset("~/assets/js/jquery.min.js", AssetType.ScriptFile), true);
            assetStore.Require(new Asset("~/assets/js/hostscript.js", AssetType.ScriptFile), true);

            assetStore.Require(new Asset("~/assets/css/style.css", AssetType.StyleFile), true);
        }
    }
}
