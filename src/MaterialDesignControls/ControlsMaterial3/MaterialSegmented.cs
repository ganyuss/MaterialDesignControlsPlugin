using System;
using Xamarin.Forms;

namespace Plugin.MaterialDesignControls.Material3;

public class MaterialSegmented : MaterialSegmentedBase
{
    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(MaterialSegmentedBase), defaultValue: null, BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

    public object SelectedItem
    {
        get { return GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged; 

    protected override void OnItemTapped(int itemIndex)
    {
        SelectedItem = ItemsSource[itemIndex];
        
        SelectedItemChanged?.Invoke(this, new SelectedItemChangedEventArgs(SelectedItem, itemIndex));
    }

    protected override bool IsItemSelected(int itemIndex)
    {
        return Equals(ItemsSource[itemIndex], SelectedItem);
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == nameof(ItemsSource))
        {
            CheckForWrongSelectedItem();
        }
    }

    private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (MaterialSegmented)bindable;
        control.CheckForWrongSelectedItem();
        control.SelectionChanged();
    }

    private void CheckForWrongSelectedItem()
    {
        if (ItemsSource == null || ItemsSource.Count == 0) return;

        if (Equals(SelectedItem, null) || ! ItemsSource.Contains(SelectedItem))
            SelectedItem = ItemsSource[0];
    }
}