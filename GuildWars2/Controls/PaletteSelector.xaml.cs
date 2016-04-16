using GuildWars2.Classes.Palette;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace GuildWars2.Controls
{
    /// <summary>
    /// Interaction logic for PaletteSelector.xaml
    /// </summary>
    public partial class PaletteSelector : UserControl
    {
        public IEnumerable<Swatch> Swatches => Classes.Palette.SwatchesProvider.GetSwatches();

        public PaletteSelector() {
            InitializeComponent();
        }

        private static void ApplyBase(bool isDark) {
            new PaletteHelper().SetLightDark(isDark);
        }

        public ICommand ApplyPrimaryCommand { get; } = new CommandImplementation(o => ApplyPrimary((Swatch)o));
        private static void ApplyPrimary(Swatch swatch) {
            new PaletteHelper().ReplacePrimaryColor(swatch);
        }

        public ICommand ApplyAccentCommand { get; } = new CommandImplementation(o => ApplyAccent((Swatch)o));
        private static void ApplyAccent(Swatch swatch) {
            new PaletteHelper().ReplaceAccentColor(swatch);
        }
    }
}
