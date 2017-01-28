using Module1;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(AssetRegistration), "Start")]

namespace Module1 {
    using Infrastructure;

    public static class AssetRegistration {
        public static void Start() {
            var assetStore = AppAssetStore.GetInstance;
            assetStore.Require(new Asset("~/assets/js/module1script.js", AssetType.ScriptFile), false);
        }
    }
}
