using PreStart.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PreStart.Models
{
    public class SignaturePad : View
    {
        public bool IsBlank

        {

            get { return RequestIsBlank(); }

        }

        public static readonly BindableProperty CaptionTextProperty =
            BindableProperty.Create(nameof(CaptionText), typeof(string), typeof(SignaturePad), null);

        public string CaptionText
        {

            get { return (string)GetValue(CaptionTextProperty); }

            set { SetValue(CaptionTextProperty, value); }
        }

        public static readonly BindableProperty CaptionTextColorProperty =
            BindableProperty.Create(nameof(CaptionTextColor), typeof(Color), typeof(SignaturePad), Color.Default);

        public Color CaptionTextColor
        {
            get { return (Color)GetValue(CaptionTextColorProperty); }
            set { SetValue(CaptionTextColorProperty, value); }
        }

        public static readonly BindableProperty ClearTextProperty =
            BindableProperty.Create(nameof(ClearText), typeof(string), typeof(SignaturePad), null);

        public string ClearText
        {
            get { return (string)GetValue(ClearTextProperty); }
            set { SetValue(ClearTextProperty, value); }
        }

        public static readonly BindableProperty ClearTextColorProperty =
            BindableProperty.Create(nameof(ClearTextColor), typeof(Color), typeof(SignaturePad), Color.Default);

        public Color ClearTextColor
        {
            get { return (Color)GetValue(ClearTextColorProperty); }
            set { SetValue(ClearTextColorProperty, value); }
        }

        public static readonly BindableProperty PromptTextProperty =
            BindableProperty.Create(nameof(PromptText), typeof(string), typeof(SignaturePad), null);

        public string PromptText
        {
            get { return (string)GetValue(PromptTextProperty); }
            set { SetValue(PromptTextProperty, value); }
        }

        public static readonly BindableProperty PromptTextColorProperty =
            BindableProperty.Create(nameof(PromptTextColor), typeof(Color), typeof(SignaturePad), Color.Default);

        public Color PromptTextColor
        {
            get { return (Color)GetValue(PromptTextColorProperty); }
            set { SetValue(PromptTextColorProperty, value); }
        }

        public static readonly BindableProperty SignatureLineColorProperty =
            BindableProperty.Create(nameof(SignatureLineColor), typeof(Color), typeof(SignaturePad), Color.Default);

        public Color SignatureLineColor
        {
            get { return (Color)GetValue(SignatureLineColorProperty); }
            set { SetValue(SignatureLineColorProperty, value); }
        }

        public static readonly BindableProperty StrokeColorProperty =
            BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(SignaturePad), Color.Default);

        public Color StrokeColor
        {
            get { return (Color)GetValue(StrokeColorProperty); }
            set { SetValue(StrokeColorProperty, value); }
        }

        public static readonly BindableProperty StrokeWidthProperty =
            BindableProperty.Create(nameof(StrokeWidth), typeof(float), typeof(SignaturePad), (float)0);

        public float StrokeWidth
        {
            get { return (float)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }


        public IEnumerable<Point> Points

        {

            get { return GetSignaturePoints(); }

            set { SetSignaturePoints(value); }

        }

        public Task<Stream> GetImageStreamAsync(SignatureImageFormat imageFormat)

        {

            var args = new ImageStreamRequestedEventArgs(imageFormat);

            ImageStreamRequested?.Invoke(this, args);

            return args.ImageStreamTask;

        }



        private IEnumerable<Point> GetSignaturePoints()

        {

            var args = new PointsEventArgs();

            PointsRequested?.Invoke(this, args);

            return args.Points;

        }



        private void SetSignaturePoints(IEnumerable<Point> points)

        {

            PointsSpecified?.Invoke(this, new PointsEventArgs { Points = points });

        }



        private bool RequestIsBlank()

        {

            var args = new IsBlankRequestedEventArgs();

            IsBlankRequested?.Invoke(this, args);

            return args.IsBlank;

        }



        public event EventHandler<ImageStreamRequestedEventArgs> ImageStreamRequested;

        public event EventHandler<IsBlankRequestedEventArgs> IsBlankRequested;

        public event EventHandler<PointsEventArgs> PointsRequested;

        public event EventHandler<PointsEventArgs> PointsSpecified;



        public class ImageStreamRequestedEventArgs : EventArgs

        {

            public ImageStreamRequestedEventArgs(SignatureImageFormat imageFormat)

            {

                ImageFormat = imageFormat;

            }



            public SignatureImageFormat ImageFormat { get; private set; }



            public Task<Stream> ImageStreamTask { get; set; } = System.Threading.Tasks.Task.FromResult<Stream>(null);

        }



        public class IsBlankRequestedEventArgs : EventArgs

        {

            public bool IsBlank { get; set; } = true;

        }



        public class PointsEventArgs : EventArgs

        {

            public IEnumerable<Point> Points { get; set; } = new Point[0];

        }




    }
}
