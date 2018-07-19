namespace CrossDeviceExpDemo.Shared.Models
{
    public class Product
    {
        public string ModelName { get; set; }
        public string ModelPath => $"ms-appx:///CrossDeviceExpDemo.Shared/Assets/3D/{ModelName}";

        public string DisplayName { get; set; }

        public Product(string modelName, string displayName)
        {
            ModelName = modelName;
            DisplayName = displayName;
        }
    }
}
