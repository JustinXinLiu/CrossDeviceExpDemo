using System;
using System.Collections.Generic;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace CrossDeviceExpDemo.Shared.Controls
{
    public sealed partial class AnimatedBubbleBackground : UserControl
    {
        public AnimatedBubbleBackground()
        {
            InitializeComponent();

            TopRectangles.Add(Rectangle3);
            TopRectangles.Add(Rectangle4);
            BottomRectangles.Add(Rectangle1);
            BottomRectangles.Add(Rectangle2);

            Loaded += OnLoaded;
            SizeChanged += OnSizeChanged;
        }

        private List<Rectangle> TopRectangles { get; } = new List<Rectangle>();
        private List<Rectangle> BottomRectangles { get; } = new List<Rectangle>();

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var compositor = Window.Current.Compositor;

            var rotationAnimation = compositor.CreateScalarKeyFrameAnimation();
            var linear = compositor.CreateLinearEasingFunction();
            rotationAnimation.InsertKeyFrame(1.0f, 360, linear);
            rotationAnimation.Duration = TimeSpan.FromSeconds(9);
            rotationAnimation.Target = nameof(Rectangle1.Rotation);
            rotationAnimation.IterationBehavior = AnimationIterationBehavior.Forever;

            Rectangle1.StartAnimation(rotationAnimation);
            Rectangle3.StartAnimation(rotationAnimation);

            rotationAnimation.Duration = TimeSpan.FromSeconds(8);

            Rectangle2.StartAnimation(rotationAnimation);
            Rectangle4.StartAnimation(rotationAnimation);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (var rectangle in TopRectangles)
            {
                var min = Math.Min(ActualWidth, ActualHeight);

                rectangle.Width = rectangle.Height = min * 0.8;
                rectangle.RadiusX = rectangle.Width * 0.49;
                rectangle.RadiusY = rectangle.Height * 0.45;

                Canvas.SetLeft(rectangle, ActualWidth - rectangle.Width * 0.4);
                Canvas.SetTop(rectangle, -rectangle.Height + rectangle.Height * 0.4);

                rectangle.CenterPoint = new Vector3((float)rectangle.Width / 2, (float)rectangle.Height / 2, 0);
            }

            foreach (var rectangle in BottomRectangles)
            {
                var min = Math.Min(ActualWidth, ActualHeight);

                rectangle.Width = rectangle.Height = min * 1.2;
                rectangle.RadiusX = rectangle.Width * 0.49;
                rectangle.RadiusY = rectangle.Height * 0.45;

                Canvas.SetLeft(rectangle, -rectangle.Width * 0.4);
                Canvas.SetTop(rectangle, ActualHeight - rectangle.Height + rectangle.Height * 0.4);

                rectangle.CenterPoint = new Vector3((float)rectangle.Width / 2, (float)rectangle.Height / 2, 0);
            }
        }
    }
}