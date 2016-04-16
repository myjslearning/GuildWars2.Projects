using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GuildWars2.Controls
{
    /// <summary>
    /// Interaction logic for MenuItem.xaml
    /// </summary>
    public partial class MenuItem : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MenuItem), new PropertyMetadata(""));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(VisualBrush), typeof(MenuItem), new PropertyMetadata(null));
        public VisualBrush Icon
        {
            get { return (VisualBrush)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        
        public MenuItem() {
            InitializeComponent();
        }
    }
}
