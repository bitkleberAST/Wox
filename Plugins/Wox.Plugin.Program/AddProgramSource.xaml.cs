using System.Linq;
using System.Windows;
using System.Windows.Input;
using Ookii.Dialogs.Wpf; // may be removed later https://github.com/dotnet/wpf/issues/438

namespace Wox.Plugin.Program
{
    /// <summary>
    /// Interaction logic for AddProgramSource.xaml
    /// </summary>
    public partial class AddProgramSource
    {
        private PluginInitContext _context;
        private ProgramSource _editing;
        private Settings _settings;

        public AddProgramSource()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            Directory.Focus();
        }

        public AddProgramSource(PluginInitContext context, Settings settings)
            : this()
        {

            _context = context;
            _settings = settings;
        }

        public AddProgramSource(ProgramSource edit, Settings settings)
            : this()
        {
            _editing = edit;
            _settings = settings;

            Directory.Text = _editing.Location;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == true)
            {
                Directory.Text = dialog.SelectedPath;
            }
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            string s = Directory.Text;
            if (!System.IO.Directory.Exists(s))
            {
                System.Windows.MessageBox.Show(_context.API.GetTranslation("wox_plugin_program_invalid_path"));
                return;
            }
            if (_editing == null)
            {
                var source = new ProgramSource
                {
                    Location = Directory.Text,
                };

                _settings.ProgramSources.Insert(0, source);
            }
            else
            {
                _editing.Location = Directory.Text;
            }

            DialogResult = true;
            Close();
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
