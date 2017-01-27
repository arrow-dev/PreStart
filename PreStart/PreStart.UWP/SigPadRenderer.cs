using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using SignaturePad.Forms;
using PreStart.UWP;
using SignaturePad.Forms.Platform;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using NativeSignaturePad = Xamarin.Controls.SignaturePad;
using NativePoint = Windows.Foundation.Point;


[assembly: ExportRenderer(typeof(PreStart.Models.SignaturePad), typeof(SigPadRenderer))]
namespace PreStart.UWP
{
    public class SigPadRenderer : ViewRenderer<Models.SignaturePad, NativeSignaturePad>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Models.SignaturePad> e)
        {
            base.OnElementChanged(e);

            if (Control == null && e.NewElement != null)
            {
                // Instantiate the native control and assign it to the Control property

                var native = new NativeSignaturePad();

                SetNativeControl(native);
            }

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources
                e.OldElement.ImageStreamRequested -= OnImageStreamRequested;
                e.OldElement.IsBlankRequested -= OnIsBlankRequested;
                e.OldElement.PointsRequested -= OnPointsRequested;
                e.OldElement.PointsSpecified -= OnPointsSpecified;
            }

            if (e.NewElement != null)
            {
                // Configure the control and subscribe to event handlers
                e.NewElement.ImageStreamRequested += OnImageStreamRequested;
                e.NewElement.IsBlankRequested += OnIsBlankRequested;
                e.NewElement.PointsRequested += OnPointsRequested;
                e.NewElement.PointsSpecified += OnPointsSpecified;

                UpdateAll();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            Update(e.PropertyName);

        }


        private void OnImageStreamRequested(object sender, Models.SignaturePad.ImageStreamRequestedEventArgs e)
        {
            var ctrl = Control;
            if (ctrl != null)
            {
                var image = ctrl.GetImage();

                InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
                var encoder =
                    BitmapEncoder.CreateAsync(
                        (e.ImageFormat == Models.SignatureImageFormat.Png)
                            ? BitmapEncoder.PngEncoderId
                            : BitmapEncoder.JpegEncoderId, stream).AsTask().Result;
                // Get pixels of the WriteableBitmap object 
                byte[] pixels = image.PixelBuffer.ToArray();
                // Save the image file with jpg extension 
                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, (uint)image.PixelWidth,
                    (uint)image.PixelHeight, 96, 96, pixels);
                encoder.FlushAsync().AsTask().Wait();
                e.ImageStreamTask = Task.Run<Stream>(() =>
                {
                    return stream.AsStream();
                });

            }
        }


        private void OnIsBlankRequested(object sender, Models.SignaturePad.IsBlankRequestedEventArgs e)
        {
            var ctrl = Control;
            if (ctrl != null)
            {
                e.IsBlank = ctrl.IsBlank;
            }
        }

        private void OnPointsRequested(object sender, Models.SignaturePad.PointsEventArgs e)
        {
            var ctrl = Control;
            if (ctrl != null)
            {
                e.Points = ctrl.Points.Select(p => new Point(p.X, p.Y));
            }
        }

        private void OnPointsSpecified(object sender, Models.SignaturePad.PointsEventArgs e)
        {
            var ctrl = Control;
            if (ctrl != null)
            {
                ctrl.LoadPoints(e.Points.Select(p => new NativePoint((float)p.X, (float)p.Y)).ToArray());
            }
        }

        private void Update(string property)
        {
            if (Control == null || Element == null)
            {
                return;
            }

            if (property == Models.SignaturePad.BackgroundColorProperty.PropertyName)
            {
                Control.BackgroundColor = Element.BackgroundColor.ToNative();
            }
            else if (property == Models.SignaturePad.CaptionTextProperty.PropertyName)
            {
                Control.CaptionText = Element.CaptionText;
            }
            else if (property == Models.SignaturePad.CaptionTextColorProperty.PropertyName)
            {
                Control.Caption.SetTextColor(Element.CaptionTextColor);
            }
            else if (property == Models.SignaturePad.ClearTextProperty.PropertyName)
            {
                Control.ClearLabelText = Element.ClearText;
            }
            else if (property == Models.SignaturePad.ClearTextColorProperty.PropertyName)
            {
                Control.ClearLabel.SetTextColor(Element.ClearTextColor);
            }
            else if (property == Models.SignaturePad.PromptTextProperty.PropertyName)
            {
                Control.SignaturePromptText = Element.PromptText;
            }
            else if (property == Models.SignaturePad.PromptTextColorProperty.PropertyName)
            {
                Control.SignaturePrompt.SetTextColor(Element.PromptTextColor);
            }
            else if (property == Models.SignaturePad.SignatureLineColorProperty.PropertyName)
            {
                var color = Element.SignatureLineColor.ToNative();

                Control.SignatureLineBrush = new SolidColorBrush(color);

            }
            else if (property == Models.SignaturePad.StrokeColorProperty.PropertyName)
            {
                Control.StrokeColor = Element.StrokeColor.ToNative();
            }
            else if (property == Models.SignaturePad.StrokeWidthProperty.PropertyName)
            {
                Control.StrokeWidth = Element.StrokeWidth;
            }
        }

        private void UpdateAll()
        {
            if (Control == null || Element == null)
            {
                return;
            }

            if (Element.BackgroundColor != Color.Default)
            {
                Control.BackgroundColor = Element.BackgroundColor.ToNative();
            }
            if (!string.IsNullOrEmpty(Element.CaptionText))
            {
                Control.CaptionText = Element.CaptionText;
            }
            if (Element.CaptionTextColor != Color.Default)
            {
                Control.Caption.SetTextColor(Element.CaptionTextColor);
            }
            if (!string.IsNullOrEmpty(Element.ClearText))
            {
                Control.ClearLabelText = Element.ClearText;
            }
            if (Element.ClearTextColor != Color.Default)
            {
                Control.ClearLabel.SetTextColor(Element.ClearTextColor);
            }
            if (!string.IsNullOrEmpty(Element.PromptText))
            {
                Control.SignaturePromptText = Element.PromptText;
            }
            if (Element.PromptTextColor != Color.Default)
            {
                Control.SignaturePrompt.SetTextColor(Element.PromptTextColor);
            }
            if (Element.SignatureLineColor != Color.Default)
            {
                var color = Element.SignatureLineColor.ToNative();

                Control.SignatureLineBrush = new SolidColorBrush(color);

            }
            if (Element.StrokeColor != Color.Default)
            {
                Control.StrokeColor = Element.StrokeColor.ToNative();
            }
            if (Element.StrokeWidth > 0)
            {
                Control.StrokeWidth = Element.StrokeWidth;
            }
        }
    }
}
