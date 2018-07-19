using CrossDeviceExpDemo.Shared.Models;
using System;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CrossDeviceExpDemo.Main
{
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Product> MyProducts { get; }
            = new ObservableCollection<Product>
            {
                new Product("Ankylosaurus.glb", "Ankylosaurus"),
                new Product("Apatosaurus.glb", "Apatosaurus"),
                new Product("Pterodactyl.glb", "Pterodactyl"),
                new Product("Triceratops.glb", "Triceratops"),
                new Product("TyrannosaurusRex.glb", "Tyrannosaurus Rex"),
                new Product("Velociraptor.glb", "Velociraptor")
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
    }
}
