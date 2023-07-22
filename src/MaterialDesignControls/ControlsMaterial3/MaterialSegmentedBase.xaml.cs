using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Plugin.MaterialDesignControls.Implementations;
using Plugin.MaterialDesignControls.Utils;
using Xamarin.Forms;

namespace Plugin.MaterialDesignControls.Material3
{
    public enum SegmentedType : int
    {
        Outlined,
        Filled
    }
    
    public abstract partial class MaterialSegmentedBase : ContentView
    {
        #region Fields

        private const int SELECTED_IMAGE_INDEX = 0;
        private const int LEADING_IMAGE_INDEX = 1;
        private const int TRAILING_IMAGE_INDEX = 3;
        private const int LABEL_INDEX = 2;

        private bool itemFramesSetup => itemFrames.Count > 0;

        protected readonly List<CustomFrame> itemFrames = new List<CustomFrame>();

        #endregion Fields

        #region Constructors

        protected MaterialSegmentedBase()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructors
        
        #region Properties

        public new static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialSegmentedBase), defaultValue: Color.White);

        public new Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public static readonly BindableProperty DisabledBackgroundColorProperty =
            BindableProperty.Create(nameof(DisabledBackgroundColor), typeof(Color), typeof(MaterialSegmentedBase), defaultValue: Color.White);

        public Color DisabledBackgroundColor
        {
            get { return (Color)GetValue(DisabledBackgroundColorProperty); }
            set { SetValue(DisabledBackgroundColorProperty, value); }
        }

        public static readonly BindableProperty SelectedColorProperty =
            BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(MaterialSegmentedBase), defaultValue: Color.FromHex("#2e85cc"));

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public static readonly BindableProperty DisabledSelectedColorProperty =
            BindableProperty.Create(nameof(DisabledSelectedColor), typeof(Color), typeof(MaterialSegmentedBase), defaultValue: Color.LightGray);

        public Color DisabledSelectedColor
        {
            get { return (Color)GetValue(DisabledSelectedColorProperty); }
            set { SetValue(DisabledSelectedColorProperty, value); }
        }

        public static readonly BindableProperty UnselectedColorProperty =
            BindableProperty.Create(nameof(UnselectedColor), typeof(Color), typeof(MaterialSegmentedBase), defaultValue: Color.White);

        public Color UnselectedColor
        {
            get { return (Color)GetValue(UnselectedColorProperty); }
            set { SetValue(UnselectedColorProperty, value); }
        }

        public static readonly BindableProperty DisabledUnselectedColorProperty =
            BindableProperty.Create(nameof(DisabledUnselectedColor), typeof(Color), typeof(MaterialSegmentedBase), defaultValue: Color.White);

        public Color DisabledUnselectedColor
        {
            get { return (Color)GetValue(DisabledUnselectedColorProperty); }
            set { SetValue(DisabledUnselectedColorProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(MaterialSegmentedBase), defaultValue: null, propertyChanged: OnItemsSourceChanged);

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        
        public static readonly BindableProperty TextPropertyPathProperty =
            BindableProperty.Create(nameof(TextPropertyPath), typeof(string), typeof(MaterialDoublePicker), defaultValue: null, propertyChanged: ElementRelatedPropertyPathChanged);

        public string TextPropertyPath
        {
            get { return (string)GetValue(TextPropertyPathProperty); }
            set { SetValue(TextPropertyPathProperty, value); }
        }
        
        public static readonly BindableProperty LeadingIconPropertyPathProperty =
            BindableProperty.Create(nameof(LeadingIconPropertyPath), typeof(string), typeof(MaterialDoublePicker), defaultValue: null, propertyChanged: ElementRelatedPropertyPathChanged);

        public string LeadingIconPropertyPath
        {
            get { return (string)GetValue(LeadingIconPropertyPathProperty); }
            set { SetValue(LeadingIconPropertyPathProperty, value); }
        }

        private bool IsLeadingIconPropertyPathSet => !string.IsNullOrEmpty(LeadingIconPropertyPath);
        
        public static readonly BindableProperty TrailingIconPropertyPathProperty =
            BindableProperty.Create(nameof(TrailingIconPropertyPath), typeof(string), typeof(MaterialDoublePicker), defaultValue: null, propertyChanged: ElementRelatedPropertyPathChanged);

        public string TrailingIconPropertyPath
        {
            get { return (string)GetValue(TrailingIconPropertyPathProperty); }
            set { SetValue(TrailingIconPropertyPathProperty, value); }
        }

        private bool IsTrailingIconPropertyPathSet => !string.IsNullOrEmpty(TrailingIconPropertyPath);

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(MaterialSegmentedBase), defaultValue: 20.0);

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty SegmentMarginProperty =
            BindableProperty.Create(nameof(SegmentMargin), typeof(int), typeof(MaterialSegmentedBase), defaultValue: 2);

        public int SegmentMargin
        {
            get { return (int)GetValue(SegmentMarginProperty); }
            set { SetValue(SegmentMarginProperty, value); }
        }

        public static readonly BindableProperty SelectedTextColorProperty =
            BindableProperty.Create(nameof(SelectedTextColor), typeof(Color), typeof(MaterialSegmentedBase), defaultValue: Color.Black);

        public Color SelectedTextColor
        {
            get { return (Color)GetValue(SelectedTextColorProperty); }
            set { SetValue(SelectedTextColorProperty, value); }
        }

        public static readonly BindableProperty DisabledSelectedTextColorProperty =
            BindableProperty.Create(nameof(DisabledSelectedTextColor), typeof(Color), typeof(MaterialSegmentedBase), defaultValue: Color.DimGray);

        public Color DisabledSelectedTextColor
        {
            get { return (Color)GetValue(DisabledSelectedTextColorProperty); }
            set { SetValue(DisabledSelectedTextColorProperty, value); }
        }

        public static readonly BindableProperty UnselectedTextColorProperty =
            BindableProperty.Create(nameof(UnselectedTextColor), typeof(Color), typeof(MaterialSegmentedBase), defaultValue: Color.Black);

        public Color UnselectedTextColor
        {
            get { return (Color)GetValue(UnselectedTextColorProperty); }
            set { SetValue(UnselectedTextColorProperty, value); }
        }

        public static readonly BindableProperty DisabledUnselectedTextColorProperty =
            BindableProperty.Create(nameof(DisabledUnselectedTextColor), typeof(Color), typeof(MaterialSegmentedBase), defaultValue: Color.Gray);

        public Color DisabledUnselectedTextColor
        {
            get { return (Color)GetValue(DisabledUnselectedTextColorProperty); }
            set { SetValue(DisabledUnselectedTextColorProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialSegmentedBase), defaultValue: 14.0);

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialSegmentedBase), defaultValue: null);

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }
        
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialSegmentedBase), defaultValue: Color.DimGray);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly BindableProperty DisabledBorderColorProperty =
            BindableProperty.Create(nameof(DisabledBorderColor), typeof(Color), typeof(MaterialSegmentedBase), defaultValue: Color.Gray);

        public Color DisabledBorderColor
        {
            get { return (Color)GetValue(DisabledBorderColorProperty); }
            set { SetValue(DisabledBorderColorProperty, value); }
        }

        public new static readonly BindableProperty SegmentTypeProperty =
            BindableProperty.Create(nameof(SegmentType), typeof(SegmentedType), typeof(MaterialSegmentedBase), defaultValue: SegmentedType.Outlined, propertyChanged: SegmentTypeChanged);

        public SegmentedType SegmentType
        {
            get { return (SegmentedType)GetValue(SegmentTypeProperty); }
            set { SetValue(SegmentTypeProperty, value); }
        }
        
        public new static readonly BindableProperty SelectedIconProperty =
            BindableProperty.Create(nameof(SelectedIcon), typeof(ImageSource), typeof(MaterialSegmentedBase), defaultValue: new FontImageSource() { Glyph = "✓", Color = Color.Black }, propertyChanged: SelectedIconChanged);

        [TypeConverter(typeof (ImageSourceConverter))]
        public ImageSource SelectedIcon
        {
            get { return (ImageSource)GetValue(SelectedIconProperty); }
            set { SetValue(SelectedIconProperty, value); }
        }
        
        public new static readonly BindableProperty ShowSelectedIconProperty =
            BindableProperty.Create(nameof(ShowSelectedIcon), typeof(bool), typeof(MaterialSegmentedBase), defaultValue: true);

        public bool ShowSelectedIcon
        {
            get { return (bool)GetValue(ShowSelectedIconProperty); }
            set { SetValue(ShowSelectedIconProperty, value); }
        }
        
        
        public static readonly BindableProperty CommandProperty = 
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialSegmentedBase), null);
        
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty = 
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialSegmentedBase), null);
        
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        #endregion Properties

        #region Methods

        private void Initialize()
        {
            borderFrame.SetBinding(Frame.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
            borderFrame.SetBinding(Frame.BorderColorProperty, new Binding(nameof(BorderColor), source: this));
            
            mainFrame.SetBinding(Frame.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
        }

        private void UpdateSegmentTypeBindings()
        {
            if (SegmentType == SegmentedType.Filled)
            {
                borderFrame.RemoveBinding(Frame.BorderColorProperty);
                borderFrame.BorderColor = Color.Transparent;
            }
            else
            {
                borderFrame.SetBinding(Frame.BorderColorProperty, new Binding(nameof(BorderColor), source: this));
            }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MaterialSegmentedBase)bindable;
            control.SetupItemFrames();
        }

        private static void SegmentTypeChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (MaterialSegmentedBase)bindable;

            control.UpdateSegmentTypeBindings();
            control.SetupItemFrames();
        }

        private static void SelectedIconChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var oldIcon = (ImageSource)oldvalue; 
            var newIcon = (ImageSource)newvalue;
            
            if (oldIcon.IsEmpty != newIcon.IsEmpty)
            {
                var control = (MaterialSegmentedBase)bindable;

                control.SetupItemFrames();
            }
        }
        
        private static void ElementRelatedPropertyPathChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (MaterialSegmentedBase)bindable;
            control.UpdateElementRelatedBounds();
        }

        private void SetupItemFrames()
        {
            grid.ColumnDefinitions = new ColumnDefinitionCollection();
            grid.Children.Clear();
            
            itemFrames.Clear();

            int column = 0;
            if (ItemsSource != null)
            {
                for (var i = 0; i < ItemsSource.Count; i++)
                {
                    var item = ItemsSource[i];
                    
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                    var frame = new CustomFrame
                    {
                        HasShadow = false,
                        Padding = new Thickness(10, 0),
                        HorizontalOptions = LayoutOptions.Fill,
                        Margin = new Thickness(SegmentMargin)
                    };
                    
                    if (SegmentType == SegmentedType.Filled)
                    {
                        frame.SetBinding(Frame.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
                    }
                    else
                    {
                        frame.Margin = new Thickness(
                            i == 0 ? -1 : -0.5, 
                            -2,  
                            i == ItemsSource.Count-1 ? -1 : -0.5, 
                            -1);
                        
                        frame.CornerRadius = 0;
                        frame.SetBinding(Frame.BorderColorProperty, new Binding(nameof(BorderColor), source: this));
                    }

                    var stackLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal, 
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                    };

                    var label = new MaterialLabel
                    {
                    };
                    label.SetBinding(Label.FontSizeProperty, new Binding(nameof(FontSize), source: this));
                    label.SetBinding(Label.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
                    
                    var selectedIcon = new Image
                    {
                    };
                    
                    selectedIcon.SetBinding(Image.HeightRequestProperty, new Binding(nameof(FontSize), source: this));
                    selectedIcon.SetBinding(Image.WidthRequestProperty, new Binding(nameof(FontSize), source: this));
                    selectedIcon.SetBinding(Image.SourceProperty, new Binding(nameof(SelectedIcon), source: this));
                    
                    
                    var leadingIcon = new Image { };
                    leadingIcon.SetBinding(Image.HeightRequestProperty, new Binding(nameof(FontSize), source: this));
                    leadingIcon.SetBinding(Image.WidthRequestProperty, new Binding(nameof(FontSize), source: this));
                    
                    var trailingIcon = new Image { };
                    trailingIcon.SetBinding(Image.HeightRequestProperty, new Binding(nameof(FontSize), source: this));
                    trailingIcon.SetBinding(Image.WidthRequestProperty, new Binding(nameof(FontSize), source: this));

                    var tapped = new TapGestureRecognizer();
                    
                    var i1 = i;
                    tapped.Tapped += (_, _) =>
                    {
                        if (!IsEnabled)
                            return;
                        
                        OnItemTapped(i1);
                        
                        if (Command != null && Command.CanExecute(CommandParameter))
                            Command.Execute(CommandParameter);
                    };

                    stackLayout.Children.Add(selectedIcon);
                    stackLayout.Children.Add(leadingIcon);
                    stackLayout.Children.Add(label);
                    stackLayout.Children.Add(trailingIcon);
                    frame.Content = stackLayout;
                    
                    frame.GestureRecognizers.Add(tapped);
                    
                    grid.Children.Add(frame, column, 0);

                    itemFrames.Add(frame);
                    column++;
                }

                UpdateFrameSelection();
                UpdateElementRelatedBounds();
            }
            
            grid.SetBinding(Grid.BackgroundColorProperty, new Binding(nameof(UnselectedColor), source: this));
        }
        

        private void UpdateElementRelatedBounds()
        {
            for (var i = 0; i < itemFrames.Count; i++)
            {
                var frame = itemFrames[i];
                var item = ItemsSource[i];
                
                var leadingImage = (Image)((Layout<View>)frame.Children[0]).Children[LEADING_IMAGE_INDEX];
                var trailingImage = (Image)((Layout<View>)frame.Children[0]).Children[TRAILING_IMAGE_INDEX];
                var label = (MaterialLabel) ((Layout<View>) frame.Children[0]).Children[LABEL_INDEX];

                if (IsLeadingIconPropertyPathSet)
                {
                    leadingImage.IsVisible = true;
                    leadingImage.SetBinding(Image.SourceProperty, new Binding(LeadingIconPropertyPath, source: item));
                }
                else
                {
                    leadingImage.IsVisible = false;
                    leadingImage.Source = null;
                }
                
                if (IsTrailingIconPropertyPathSet)
                {
                    trailingImage.IsVisible = true;
                    trailingImage.SetBinding(Image.SourceProperty, new Binding(TrailingIconPropertyPath, source: item));
                }
                else
                {
                    trailingImage.IsVisible = false;
                    trailingImage.Source = null;
                }
                
                if (string.IsNullOrEmpty(TextPropertyPath))
                {
                    label.IsVisible = !IsLeadingIconPropertyPathSet && !IsTrailingIconPropertyPathSet;
                    label.Text = item?.ToString() ?? string.Empty;
                }
                else
                {
                    label.SetBinding(Label.TextProperty, new Binding(TextPropertyPath, source: item));
                }
            }
        }
        
        protected abstract void OnItemTapped(int itemIndex);

        protected abstract bool IsItemSelected(int itemIndex);

        protected void SelectionChanged()
        {
            UpdateFrameSelection();
        }
        
        private void UpdateFrameSelection()
        {
            if (!itemFramesSetup)
                return;
            
            for (int i = 0; i < ItemsSource.Count; i++)
            {
                SetFrameSelected(itemFrames[i], IsItemSelected(i), ItemsSource[i]);
            }
        }

        protected void SetFrameSelected(CustomFrame frame, bool selected, object frameItem)
        {
            var selectImage = (Image) ((Layout<View>) frame.Children[0]).Children[SELECTED_IMAGE_INDEX];
            var leadingImage = (Image) ((Layout<View>) frame.Children[0]).Children[LEADING_IMAGE_INDEX];
            var label = (MaterialLabel) ((Layout<View>) frame.Children[0]).Children[LABEL_INDEX];

            string frameColorPath = string.Empty;
            string TextColorPath;

            if (selected)
            {
                frameColorPath = IsEnabled ? nameof(SelectedColor) : nameof(DisabledSelectedColor);
                TextColorPath = IsEnabled ? nameof(SelectedTextColor) : nameof(DisabledSelectedTextColor);
            }
            else
            {
                TextColorPath = IsEnabled ? nameof(UnselectedTextColor) : nameof(DisabledUnselectedTextColor);
            }
            
            if (selected)
            {
                frame.SetBinding(Frame.BackgroundColorProperty, new Binding(frameColorPath, source: this));

                leadingImage.IsVisible = IsLeadingIconPropertyPathSet && (! ShowSelectedIcon || ! string.IsNullOrWhiteSpace(label.Text));
            }
            else
            {
                leadingImage.IsVisible = IsLeadingIconPropertyPathSet;
                frame.BackgroundColor = Color.Transparent;
            }
            label.SetBinding(Label.TextColorProperty, new Binding(TextColorPath, source: this));

            
            if (selected)
                selectImage.SetBinding(Image.IsVisibleProperty, new Binding(nameof(ShowSelectedIcon), source: this));
            else
                selectImage.IsVisible = false;
        }

        #endregion Methods
    }
}