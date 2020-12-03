using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(
    typeof(Haka.Hybrid.Customs.WebViewCustom),
    typeof(Haka.Hybrid.Renderers.WebViewCustomRenderer))]
namespace Haka.Hybrid.Renderers
{
    class WebViewCustomRenderer : WebViewRenderer
    {
        public WebViewCustomRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}
