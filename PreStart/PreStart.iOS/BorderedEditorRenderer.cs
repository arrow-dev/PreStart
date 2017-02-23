using Prestart.CustomControls;
using PreStart.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(BorderedEditor), typeof(BorderedEditorRenderer))]
namespace PreStart.iOS
{
    public class BorderedEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Layer.BorderColor =
                UIColor.FromRGB(204, 204, 204).CGColor;
                Control.Layer.BorderWidth = 0.5f;
                Control.Layer.CornerRadius = 3f;
            }
        }
    }
}
