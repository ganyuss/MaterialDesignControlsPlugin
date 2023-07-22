using ExampleMaterialDesignControls.ViewModels;
using Xamarin.Forms;

namespace ExampleMaterialDesignControls.Pages
{
    public partial class MaterialSegmentedPage : ContentPage
    {
        public MaterialSegmentedPage()
        {
            InitializeComponent();

            BindingContext = new MaterialSegmentedViewModel() { DisplayAlert = DisplayAlert, Navigation = Navigation };
        }

        void MaterialSegmented_IsSelectedChanged(System.Object sender, System.EventArgs e)
        {
            DisplayAlert("Message","Segment Is Selected Changed","OK");
        }

        private void SizeSelectionChanged(object sender, SelectedItemChangedEventArgs e)
        {
            DisplayAlert("Size",$"Selected command: {e.SelectedItem}","OK");
        }
    }
}