using System;

namespace Plugin.MaterialDesignControls.Utils;

public class PropertyPathHelper
{
    public static string GetPropertyValue(object item, string propertyToSearch)
    {
        var properties = item.GetType().GetProperties();
        foreach (var property in properties)
        {
            if (property.Name.Equals(propertyToSearch, StringComparison.InvariantCultureIgnoreCase))
            {
                return property.GetValue(item, null).ToString();
            }
        }
        return item.ToString();
    }
}