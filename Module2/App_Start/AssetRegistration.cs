using Module2;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(AssetRegistration), "Start")]

namespace Module2 {
    using Infrastructure;

    public static class AssetRegistration {
        public static void Start() {
            var assetStore = AppAssetStore.GetInstance;
            assetStore.Require(new AppAsset("~/assets/js/module2script.js", AssetType.ScriptFile), false);
        }
    }
}
