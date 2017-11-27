using System.Windows;
using System.Windows.Controls;

namespace BedrijfsOpleiding.UIElements
{
    public class SimpleButton : Button
    {
        static SimpleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleButton), new FrameworkPropertyMetadata(typeof(SimpleButton)));
        }
    }
}
