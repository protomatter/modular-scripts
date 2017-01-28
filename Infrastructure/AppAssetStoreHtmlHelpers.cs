namespace Infrastructure {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    public static class AppAssetStoreHtmlHelpers {
        private static string Stylesheets(this HtmlHelper html, string[] cssFiles) {
            var sb = new StringBuilder();
            foreach (var cssFile in cssFiles) {
                sb.Append(html.Stylesheet(cssFile));
            }

            return sb.ToString();
        }

        private static string Stylesheet(this HtmlHelper html, string cssFile) {
            return string.Format("<link type=\"text/css\" rel=\"stylesheet\" href=\"{0}\"/>", UrlHelper.GenerateContentUrl(cssFile, html.ViewContext.HttpContext));
        }

        private static string Scripts(this HtmlHelper html, string[] jsFiles) {
            var sb = new StringBuilder();
            foreach (var jsFile in jsFiles) {
                sb.Append(html.Script(jsFile));
            }

            return sb.ToString();
        }

        private static string Script(this HtmlHelper html, string jsFile) {
            return string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", UrlHelper.GenerateContentUrl(jsFile, html.ViewContext.HttpContext));
        }

        public static IHtmlString RenderAppScripts(this HtmlHelper html) {
            var am = AppAssetStore.GetInstance;
            var appAssets = am.GetRenderableAssets(AssetType.ScriptFile, AssetType.Script).ToArray();

            var sb = new StringBuilder();
            var bundles = new List<AppAsset>();

            foreach (var asset in appAssets) {
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
            var am = AppAssetStore.GetInstance;
            var appAssets = am.GetRenderableAssets(AssetType.StyleFile, AssetType.Style).ToArray();

            var sb = new StringBuilder();
            var bundles = new List<AppAsset>();

            foreach (var asset in appAssets) {
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