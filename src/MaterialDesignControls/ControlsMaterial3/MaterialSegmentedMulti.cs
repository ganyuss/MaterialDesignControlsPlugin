using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Plugin.MaterialDesignControls.Material3;

public class MaterialSegmentedMulti : MaterialSegmentedBase
{
    public static readonly BindableProperty SelectedItemsProperty =
        BindableProperty.Create(nameof(SelectedItems), typeof(IList), typeof(MaterialSegmentedBase), defaultValue: new List<object>(), BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

    public IList SelectedItems
    {
        get { return (IList)GetValue(SelectedItemsProperty); }
        set { SetValue(SelectedItemsProperty, value); }
    }

    public event EventHandler<SelectionChangedEventArgs> SelectedItemsChanged; 
    
    protected override void OnItemTapped(int itemIndex)
    {
        var itemTapped = ItemsSource[itemIndex];

        IList<object> previousSelection = SelectedItems.Cast<object>().ToList();
        IList<object> currentSelection;

        if (SelectedItems.Contains(itemTapped))
            currentSelection = previousSelection
                .Where(o => o != itemTapped)
                .ToList();
        else
            currentSelection = previousSelection
                .Append(itemTapped)
                .ToList();

        SelectedItems = (IList)currentSelection;
        SelectedItemsChanged?.Invoke(this, new SelectionChangedEventArgs(previousSelection, currentSelection));
    }

    protected override bool IsItemSelected(int itemIndex)
    {
        return SelectedItems.Contains(ItemsSource[itemIndex]);
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == nameof(ItemsSource))
        {
            PatchWrongSelectedItem();
        }
    }

    private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (MaterialSegmentedMulti)bindable;
        control.SelectionChanged();
    }

    private void PatchWrongSelectedItem()
    {
        if (ItemsSource.Count == 0) return;

        foreach (var item in SelectedItems)
        {
            if (!ItemsSource.Contains(item))
                SelectedItems.Remove(item);
        }
    }
}

public class SelectionChangedEventArgs : EventArgs
{
    public IReadOnlyList<object> PreviousSelection { get; }
    public IReadOnlyList<object> CurrentSelection { get; }

    static readonly IReadOnlyList<object> s_empty = new List<object>(0);

    internal SelectionChangedEventArgs(object previousSelection, object currentSelection)
    {
        PreviousSelection = previousSelection != null ? new List<object>(1) { previousSelection } : s_empty;
        CurrentSelection = currentSelection != null ? new List<object>(1) { currentSelection } : s_empty;
    }

    internal SelectionChangedEventArgs(IList<object> previousSelection, IList<object> currentSelection)
    {
        PreviousSelection = new List<object>(previousSelection ?? throw new ArgumentNullException(nameof(previousSelection)));
        CurrentSelection = new List<object>(currentSelection ?? throw new ArgumentNullException(nameof(currentSelection)));
    }
}