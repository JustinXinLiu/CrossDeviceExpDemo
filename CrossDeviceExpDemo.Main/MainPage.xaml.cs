using CrossDeviceExpDemo.Shared.Models;
using System;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace CrossDeviceExpDemo.Main
{
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Product> MyProducts { get; }
            = new ObservableCollection<Product>
            {
                //new Product("Ankylosaurus.glb", "Ankylosaurus"),
                //new Product("Apatosaurus.glb", "Apatosaurus"),
                //new Product("Pterodactyl.glb", "Pterodactyl"),
                //new Product("Triceratops.glb", "Triceratops"),
                new Product("TyrannosaurusRex.glb", "Tyrannosaurus Rex"),
                //new Product("Velociraptor.glb", "Velociraptor")
            };

        public MainPage()
        {
            InitializeComponent();

            Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1000);




        }

        private void OnVisualPlaceholderLoaded(object sender, RoutedEventArgs e)
        {
            Compositor compositor = Window.Current.Compositor;
            Rectangle visualPlaceholder = (Rectangle)sender;

            if (visualPlaceholder.DataContext is Product product)
            {
                //var spatialVisual = compositor.CreateSpriteVisual();
                //spatialVisual.Brush = compositor.CreateColorBrush(Colors.LightBlue);
                var spatialVisual = compositor.CreateSpatialVisual();
                spatialVisual.LoadModel(product.ModelPathLocal);
                spatialVisual.RelativeSizeAdjustment = Vector2.One;
                spatialVisual.CenterPoint = new Vector3(visualPlaceholder.RenderSize.ToVector2() * 0.5f, 0.5f);
                spatialVisual.RotationAxis = new Vector3(0.0f, 1.0f, 0.0f);
                //spatialVisual.RotationAngleInDegrees = 90.0f;

                ScalarKeyFrameAnimation rotationAnimation = compositor.CreateScalarKeyFrameAnimation();
                rotationAnimation.InsertKeyFrame(1.0f, 360.0f, compositor.CreateLinearEasingFunction());
                rotationAnimation.Duration = TimeSpan.FromMilliseconds(4000);
                rotationAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
                spatialVisual.StartAnimation(nameof(Visual.RotationAngleInDegrees), rotationAnimation);

                ElementCompositionPreview.SetElementChildVisual(visualPlaceholder, spatialVisual);
            }
            else
            {
                product = new Product("house.glb", "Tyrannosaurus Rex");

                var spatialVisual = compositor.CreateSpatialVisual();
                spatialVisual.LoadModel(product.ModelPathLocal);
                spatialVisual.RelativeSizeAdjustment = Vector2.One;
                spatialVisual.CenterPoint = new Vector3(visualPlaceholder.RenderSize.ToVector2() * 0.5f, 75.0f);
                spatialVisual.RotationAxis = new Vector3(1.0f, 1.0f, 0.0f);
                //spatialVisual.RotationAngleInDegrees = 45.0f;

                float radianPerDegree = (float)Math.PI / 180.0f;
                var xRotation = Quaternion.CreateFromAxisAngle(new Vector3(1.0f, 0.0f, 0.0f), radianPerDegree * -15.0f);
                var yRotation = Quaternion.CreateFromAxisAngle(new Vector3(0.0f, 1.0f, 0.0f), radianPerDegree * 45.0f);
                spatialVisual.Orientation = Quaternion.Multiply(xRotation, yRotation);

                //ScalarKeyFrameAnimation rotationAnimation = compositor.CreateScalarKeyFrameAnimation();
                //rotationAnimation.InsertKeyFrame(1.0f, 360.0f, compositor.CreateLinearEasingFunction());
                //rotationAnimation.Duration = TimeSpan.FromMilliseconds(4000);
                //rotationAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
                //spatialVisual.StartAnimation(nameof(Visual.RotationAngleInDegrees), rotationAnimation);

                ElementCompositionPreview.SetElementChildVisual(visualPlaceholder, spatialVisual);
            }
        }

        private void VisualPlaceholder_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            TranslateTransform transform = new TranslateTransform();
            VisualPlaceholder.RenderTransform = transform;
            transform.Y = e.Cumulative.Translation.Y;
        }
    }
}