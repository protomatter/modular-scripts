namespace Infrastructure {
    using System.Text;
    using System.Web.Mvc;

    public static class AppAssetStoreHelpers {
        public static string Stylesheets(this HtmlHelper html, string[] cssFiles) {
            var sb = new StringBuilder();
            foreach (var cssFile in cssFiles) {
                sb.Append(html.Stylesheet(cssFile));
            }

            return sb.ToString();
        }

        public static string Stylesheet(this HtmlHelper html, string cssFile) {
            return string.Format("<link type=\"text/css\" rel=\"stylesheet\" href=\"{0}\"/>", UrlHelper.GenerateContentUrl(cssFile, html.ViewContext.HttpContext));
        }

        public static string Scripts(this HtmlHelper html, string[] jsFiles) {
            var sb = new StringBuilder();
            foreach (var jsFile in jsFiles) {
                sb.Append(html.Script(jsFile));
            }

            return sb.ToString();
        }

        public static string Script(this HtmlHelper html, string jsFile) {
            return string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", UrlHelper.GenerateContentUrl(jsFile, html.ViewContext.HttpContext));
        }
    }
}