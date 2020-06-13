using System.Windows;
using System.Windows.Input;

namespace Wox.Plugin.Program
{
    /// <summary>
    /// ProgramSuffixes.xaml 的交互逻辑
    /// </summary>
    public partial class ProgramSuffixes
    {
        private PluginInitContext context;
        private Settings _settings;

        public ProgramSuffixes(PluginInitContext context, Settings settings)
        {
            this.context = context;
            InitializeComponent();
            _settings = settings;
            tbSuffixes.Text = string.Join(Settings.SuffixSeperator.ToString(), _settings.ProgramSuffixes);

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbSuffixes.Focus();
            tbSuffixes.SelectionStart = tbSuffixes.Text.Length;
            tbSuffixes.SelectionLength = 0;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbSuffixes.Text))
            {
                string warning = context.API.GetTranslation("wox_plugin_program_suffixes_cannot_empty");
                MessageBox.Show(warning);
                return;
            }

            _settings.ProgramSuffixes = tbSuffixes.Text.Split(Settings.SuffixSeperator);
            string msg = context.API.GetTranslation("wox_plugin_program_update_file_suffixes");
            MessageBox.Show(msg);

            DialogResult = true;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                DialogResult = false; 
            }
        }
    }
}
