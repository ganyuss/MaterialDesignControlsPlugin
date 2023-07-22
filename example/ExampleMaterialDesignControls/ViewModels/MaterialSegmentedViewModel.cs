using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Plugin.MaterialDesignControls.Material3;
using Xamarin.Forms;

namespace ExampleMaterialDesignControls.ViewModels
{
    public class MaterialSegmentedViewModel : BaseViewModel
    { 
        public string SelectedIconSvgUri => "resource://ExampleMaterialDesignControls.Resources.Svg.starSelected.svg";
        
        private string selectedItem;
        public string SelectedItem
        { 
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
	    }

        private string selectedSize;
        public string SelectedSize
        { 
            get => selectedSize;
            set => SetProperty(ref selectedSize, value);
	    }

        private ObservableCollection<string> items;

        public ObservableCollection<string> Items
        {
            get { return items; }
            set { SetProperty(ref items, value); }
        }

        private ObservableCollection<string> items2;

        public ObservableCollection<string> Items2
        {
            get { return items2; }
            set { SetProperty(ref items2, value); }
        }

        private ObservableCollection<string> sizes;

        public ObservableCollection<string> Sizes
        {
            get { return sizes; }
            set { SetProperty(ref sizes, value); }
        }

        private ObservableCollection<string> onOff;
        public ObservableCollection<string> OnOff
        {
            get { return onOff; }
            set { SetProperty(ref onOff, value); }
        }

        private ObservableCollection<string> backlight;
        public ObservableCollection<string> Backlight
        {
            get { return backlight; }
            set { SetProperty(ref backlight, value); }
        }

        private ObservableCollection<string> frecuently;
        public ObservableCollection<string> Frecuently
        {
            get { return frecuently; }
            set { SetProperty(ref frecuently, value); }
        }

        private ObservableCollection<SegmentedType> segmentTypes;
        public ObservableCollection<SegmentedType> SegmentTypes
        {
            get { return segmentTypes; }
            set { SetProperty(ref segmentTypes, value); }
        }

        private SegmentedType selectedSegmentType; 
        public SegmentedType SelectedSegmentType
        {
            get { return selectedSegmentType; }
            set { SetProperty(ref selectedSegmentType, value); }
        }

        private ObservableCollection<SelectableColor> availableColors;
        public ObservableCollection<SelectableColor> AvailableColors
        {
            get { return availableColors; }
            set { SetProperty(ref availableColors, value); }
        }
        
        private SelectableColor selectedColor; 
        public SelectableColor SelectedColor
        {
            get { return selectedColor; }
            set { SetProperty(ref selectedColor, value); }
        }

        public MaterialSegmentedViewModel()
        {
            Items = new ObservableCollection<string> { "Complete","Incomplete","Pending"};
            SelectedItem = Items[0];
            Items2 = new ObservableCollection<string> { "Music", "Photos", "Movies", "Apps" };
            Sizes = new ObservableCollection<string> { "XS","S","M","L","XL"};
            OnOff = new ObservableCollection<string> { "Off","On"};
            Backlight = new ObservableCollection<string> { "Backlight On","Backlight Off"};
            Frecuently = new ObservableCollection<string> { "Day","Week","Month"};

            SegmentTypes = new ObservableCollection<SegmentedType> { SegmentedType.Filled, SegmentedType.Outlined };

            AvailableColors = new ObservableCollection<SelectableColor>
            {
                new() { Color = Color.Pink, ColorName = nameof(Color.Pink) }, 
                new() { Color = Color.Aquamarine, ColorName = nameof(Color.Aquamarine) },
                new() { Color = Color.Lavender, ColorName = nameof(Color.Lavender) }
            };
        }

        public ICommand SaveCommand => new Command( async()=>
        {
            await this.DisplayAlert.Invoke("Size", $"Selected item: {SelectedItem}", "Ok");
        });

        public ICommand SelectCommand => new Command( async ()=>
        {
            await this.DisplayAlert.Invoke("Size", $"Selected command: {SelectedSize}", "Ok");
        });

        public class SelectableColor : ObservableObject
        {
            private Color _color;
            public Color Color { 
                get => _color;
                set => SetProperty(ref _color, value); 
            }
            
            private string _colorName;
            public string ColorName { 
                get => _colorName;
                set => SetProperty(ref _colorName, value); 
            }


            public ImageSource ColorIcon => new FontImageSource() { Glyph = "■", Color = Color };
        }
    }
}
