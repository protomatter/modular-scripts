namespace Infrastructure {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class AppAssetStore {
        private static readonly AppAssetStore Instance = new AppAssetStore();
        private readonly List<AppAsset> _assets = new List<AppAsset>();
        private readonly List<AppAsset> _layoutAssets = new List<AppAsset>();

        static AppAssetStore() {}

        private AppAssetStore() {}

        public static AppAssetStore GetInstance {
            get { return Instance; }
        }

        public IEnumerable<AppAsset> GetRenderableAssets(params AssetType[] types) {
            foreach (AppAsset asset in _layoutAssets.Concat(_assets).Where(x => !x.Rendered && types.Contains(x.Type))) {
                asset.Rendered = true;
                yield return asset;
            }
        }

        public void Require(AppAsset appAsset, bool forLayout) {
            if (appAsset == null) {
                throw new ArgumentNullException("appAsset");
            }

            // no duplicates
            if (_assets.Any(x => x.Type == appAsset.Type && x.Text == appAsset.Text)) {
                return;
            }

            if (forLayout) {
                _layoutAssets.Add(appAsset);
            } else {
                _assets.Add(appAsset);
            }
        }
    }
}
