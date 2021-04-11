using Haka.Core;
using Haka.Hybrid.Customs;
using Kasay.BindableProperty;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

namespace Haka.Hybrid
{
    public class SvgHybrid : ContentView
    {
        readonly static Dictionary<string, HtmlWebViewSource> htmlWebViewStore = htmlWebViewStore ?? new Dictionary<string, HtmlWebViewSource>();

        [Bind] public string Source { get; set; }

        public SvgHybrid()
        {
            this.OnChanged(_ => _.Source, OnSourceChanged);

            HeightRequest = 50;
            WidthRequest = 50;
        }

        void OnSourceChanged()
        {
            if (string.IsNullOrWhiteSpace(Source)) return;

            if (!htmlWebViewStore.TryGetValue(Source, out HtmlWebViewSource htmlWebViewSource))
            {
                htmlWebViewSource = new HtmlWebViewSource();
                string svgContent;

                if (Source.Contains("https"))
                {
                    using (var webClient = new WebClient())
                    {
                        svgContent = webClient.DownloadString(Source);
                    }
                }
                else
                {
                    svgContent = LocalResource.GetContent(Source);
                }

                htmlWebViewSource.Html = LocalResource.GetContent("svg.html", this)
                    .Replace("{svg}", svgContent); ;

                htmlWebViewStore.Add(Source, htmlWebViewSource);
            }

            var webViewCustom = new WebViewCustom
            {
                Source = htmlWebViewSource
            };

            Content = webViewCustom;
        }
    }
}
