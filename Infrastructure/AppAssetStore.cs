namespace Infrastructure {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class AppAssetStore {
        private static readonly AppAssetStore Instance = new AppAssetStore();
        private readonly List<Asset> _assets = new List<Asset>();
        private readonly List<Asset> _layoutAssets = new List<Asset>();

        static AppAssetStore() {}

        private AppAssetStore() {}

        public static AppAssetStore GetInstance {
            get { return Instance; }
        }

        public IEnumerable<Asset> GetRenderableAssets(params AssetType[] types) {
            foreach (Asset asset in _layoutAssets.Concat(_assets).Where(x => types.Contains(x.Type))) {
                asset.Rendered = true;
                yield return asset;
            }
        }

        public void Require(Asset asset, bool forLayout) {
            if (asset == null) {
                throw new ArgumentNullException("asset");
            }

            // no dups
            if (_assets.Any(x => x.Type == asset.Type && x.Text == asset.Text)) {
                return;
            }

            if (forLayout) {
                _layoutAssets.Add(asset);
            } else {
                _assets.Add(asset);
            }
        }
    }
}
