using System.Windows;
using System.Windows.Controls;

namespace market.Utilities
{
    /// <summary>
    /// Represents a button settings.
    /// </summary>
    public class Btn : RadioButton
    {
        static Btn()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Btn), new FrameworkPropertyMetadata(typeof(Btn)));
        }
    }
}
