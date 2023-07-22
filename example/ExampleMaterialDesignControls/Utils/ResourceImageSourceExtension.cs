using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExampleMaterialDesignControls.Utils
{
    [ContentProperty(nameof(Source))]
    public class ResourceImageSourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue (IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }

            var imageSource = ImageSource.FromResource(Source, typeof(ResourceImageSourceExtension).GetTypeInfo().Assembly);
            return imageSource;
        }
    }
}