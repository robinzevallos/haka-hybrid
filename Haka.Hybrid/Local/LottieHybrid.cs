using Haka.Hybrid.Customs;
using Haka.Core;
using Kasay.BindableProperty;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Haka.Hybrid
{
    public class LottieHybrid : ContentView
    {
        readonly static Dictionary<String, WebViewCustom> webViewCustomStore = webViewCustomStore ?? new Dictionary<String, WebViewCustom>();

        [Bind] public String Source { get; set; }

        [Bind] public Double Size { get; set; }

        public LottieHybrid()
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
                var htmlWebViewSource = new HtmlWebViewSource();

                var lottieHtmlContent = LocalResource.GetContent("lottie.html", this);
                var lottieJsContent = LocalResource.GetContent("lottie.min.5.6.10.js", this);

                var svgHtmlContent = lottieHtmlContent
                    .Replace("{path}", Source)
                    .Replace("{script}", lottieJsContent);

                htmlWebViewSource.Html = svgHtmlContent;
                webViewCustom.Source = htmlWebViewSource;

                if (!webViewCustomStore.ContainsKey(Source))
                {
                    webViewCustomStore.Add(Source, webViewCustom);
                }
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
