using Haka.Hybrid.Customs;
using Kasay.BindableProperty;
using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;
using Haka.Core;

namespace Haka.Hybrid
{
    public class SvgHybrid : ContentView
    {
        readonly static Dictionary<String, WebViewCustom> webViewCustomStore = webViewCustomStore ?? new Dictionary<String, WebViewCustom>();

        [Bind] public String Source { get; set; }

        [Bind] public Double Size { get; set; }

        public SvgHybrid()
        {
            this
                .OnChanged(_ => _.Source, OnSourceChanged)
                .OnChanged(_ => _.Size, OnSizeChanged);
        }

        void OnSourceChanged()
        {
            if (String.IsNullOrWhiteSpace(Source)) return;

            if (!webViewCustomStore.TryGetValue(Source, out WebViewCustom webViewCustom))
            {
                webViewCustom = new WebViewCustom();
                HtmlWebViewSource htmlWebViewSource = new HtmlWebViewSource();
                String svgContent;

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

                var svgHtmlContent = LocalResource.GetContent("svg.html", this)
                    .Replace("{svg}", svgContent);

                htmlWebViewSource.Html = svgHtmlContent;
                webViewCustom.Source = htmlWebViewSource;
            }

            Content = webViewCustom;
        }

        void OnSizeChanged()
        {
            HeightRequest = Size;
            WidthRequest = Size;
        }
    }
}
