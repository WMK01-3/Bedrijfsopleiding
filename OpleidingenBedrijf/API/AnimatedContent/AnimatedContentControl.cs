using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BedrijfsOpleiding.API.AnimatedContent
{
    /// <inheritdoc />
    /// <summary>
    /// A ContentControl that animates the transition between content
    /// </summary>
    [TemplatePart(Name = "PART_PaintArea", Type = typeof(Shape)),
     TemplatePart(Name = "PART_MainContent", Type = typeof(ContentPresenter))]
    public class AnimatedContentControl : ContentControl
    {
        public bool ShouldAnimate { get; set; }
        public delegate void Animate();

        private bool _goLeft;

        #region Generated static constructor
        static AnimatedContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedContentControl), new FrameworkPropertyMetadata(typeof(AnimatedContentControl)));
        }
        #endregion

        private Shape _mPaintArea;
        private ContentPresenter _mMainContent;

        /// <inheritdoc />
        /// <summary>
        /// This gets called when the template has been applied and we have our visual tree
        /// </summary>
        public override void OnApplyTemplate()
        {
            _mPaintArea = Template.FindName("PART_PaintArea", this) as Shape;
            _mMainContent = Template.FindName("PART_MainContent", this) as ContentPresenter;

            base.OnApplyTemplate();
        }

        /// <inheritdoc />
        /// <summary>
        /// This gets called when the content we're displaying has changed
        /// </summary>
        /// <param name="oldContent">The content that was previously displayed</param>
        /// <param name="newContent">The new content that is displayed</param>
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            if (ShouldAnimate)
            {
                if (_mPaintArea != null && _mMainContent != null)
                {
                    _mPaintArea.Fill = CreateBrushFromVisual(_mMainContent);

                    if (_goLeft)
                        BeginAnimateContentReplacement(TranslateTransform.XProperty, -1);
                    else
                        BeginAnimateContentReplacement(TranslateTransform.XProperty, 1);

                    _goLeft = !_goLeft;
                }
            }
            base.OnContentChanged(oldContent, newContent);
        }

        /// <summary>
        /// Starts the animation for the new content
        /// </summary>
        private void BeginAnimateContentReplacement(DependencyProperty transform, int direction)
        {
            TranslateTransform newContentTransform = new TranslateTransform();
            TranslateTransform oldContentTransform = new TranslateTransform();
            _mPaintArea.RenderTransform = oldContentTransform;
            _mMainContent.RenderTransform = newContentTransform;
            _mPaintArea.Visibility = Visibility.Visible;

            double directionAmount = transform == TranslateTransform.XProperty ? ActualWidth : ActualHeight;

            newContentTransform.BeginAnimation(transform, CreateAnimation(-direction * directionAmount, 0));
            oldContentTransform.BeginAnimation(transform, CreateAnimation(0, direction * directionAmount, (s, e) => _mPaintArea.Visibility = Visibility.Hidden));
        }

        /// <summary>
        /// Creates the animation that moves content in or out of view.
        /// </summary>
        /// <param name="from">The starting value of the animation.</param>
        /// <param name="to">The end value of the animation.</param>
        /// <param name="whenDone">(optional) A callback that will be called when the animation has completed.</param>
        private static AnimationTimeline CreateAnimation(double from, double to, EventHandler whenDone = null)
        {
            IEasingFunction ease = new BackEase { Amplitude = 0, EasingMode = EasingMode.EaseInOut };
            Duration duration = new Duration(TimeSpan.FromSeconds(0.5));
            DoubleAnimation anim = new DoubleAnimation(from, to, duration) { EasingFunction = ease };
            if (whenDone != null)
                anim.Completed += whenDone;
            anim.Freeze();
            return anim;
        }

        /// <summary>
        /// Creates a brush based on the current appearnace of a visual element. The brush is an ImageBrush and once created, won't update its look
        /// </summary>
        /// <param name="v">The visual element to take a snapshot of</param>
        private Brush CreateBrushFromVisual(Visual v)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));
            RenderTargetBitmap target = new RenderTargetBitmap((int)ActualWidth, (int)ActualHeight, 96, 96, PixelFormats.Pbgra32);
            target.Render(v);
            ImageBrush brush = new ImageBrush(target);
            brush.Freeze();
            return brush;
        }
    }
}
