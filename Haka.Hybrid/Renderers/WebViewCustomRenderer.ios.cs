using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(
    typeof(Haka.Hybrid.Customs.WebViewCustom),
    typeof(Haka.Hybrid.Renderers.WebViewCustomRenderer))]
namespace Haka.Hybrid.Renderers
{
    class WebViewCustomRenderer : WkWebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Opaque = false;
                BackgroundColor = UIColor.Clear;
            }
        }
    }
}
