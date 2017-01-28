namespace Infrastructure {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    public static class AssetManagerHtmlHelpers {
        public static IHtmlString RenderAppScripts(this HtmlHelper html) {
            AppAssetStore am = AppAssetStore.GetInstance;
            Asset[] assets = am.GetRenderableAssets(AssetType.ScriptFile, AssetType.Script).ToArray();

            var sb = new StringBuilder();
            var bundles = new List<Asset>();

            foreach (Asset asset in assets) {
                switch (asset.Type) {
                    case AssetType.Script:
                        if (bundles.Any()) {
                            sb.AppendLine(html.Scripts(bundles.Select(x => x.Text).ToArray()));
                            bundles.Clear();
                        }
                        sb.AppendLine(asset.Text);
                        break;

                    case AssetType.ScriptFile:
                        bundles.Add(asset);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            sb.AppendLine(html.Scripts(bundles.Select(x => x.Text).ToArray()));
            return new MvcHtmlString(sb.ToString());
        }

        public static IHtmlString RenderAppStyles(this HtmlHelper html) {
            AppAssetStore am = AppAssetStore.GetInstance;
            Asset[] assets = am.GetRenderableAssets(AssetType.StyleFile, AssetType.Style).ToArray();

            var sb = new StringBuilder();
            var bundles = new List<Asset>();

            foreach (Asset asset in assets) {
                switch (asset.Type) {
                    case AssetType.Style:
                        if (bundles.Any()) {
                            sb.AppendLine(html.Stylesheets(bundles.Select(x => x.Text).ToArray()));
                            bundles.Clear();
                        }
                        sb.AppendLine(asset.Text);
                        break;

                    case AssetType.StyleFile:
                        bundles.Add(asset);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            sb.AppendLine(html.Stylesheets(bundles.Select(x => x.Text).ToArray()));
            return new MvcHtmlString(sb.ToString());
        }
    }
}