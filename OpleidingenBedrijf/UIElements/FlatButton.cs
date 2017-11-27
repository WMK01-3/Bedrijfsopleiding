using System.Windows;
using System.Windows.Controls;

namespace BedrijfsOpleiding.UIElements
{
    public class FlatButton : Button
    {
        static FlatButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatButton), new FrameworkPropertyMetadata(typeof(FlatButton)));
        }
    }
}
