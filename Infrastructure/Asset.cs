namespace Infrastructure {
    using System;

    public class Asset {
        public Asset(string text, AssetType type) {
            if (text == null) {
                throw new ArgumentNullException("text");
            }

            Text = text;
            Type = type;
        }

        public string Text { get; set; }
        public AssetType Type { get; set; }
        public bool Rendered { get; internal set; }
    }
}