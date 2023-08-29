using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace DEXISUtilitiesWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class IsServerWindow : Window
    {
        DEXIS9Logic cl = new DEXIS9Logic();
        int _version = 0;
        public IsServerWindow() { }
        public IsServerWindow(int version)
        {
            InitializeComponent();
            _version = version;
            this.Hide();
            if (!cl.IsServer())
            {
                new MainWindow(_version, "");
            }
            else if (cl.IsServer())
            {
                this.Show();
            }
        }

        private void IsServerBrowse_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                isServerDataPath.Text = dialog.SelectedPath;
            }

        }

        private void IsServerSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (isServerDataPath.Text == "")
            {
                MessageBox.Show("The datapath cannot be empty");
            }
            else
            {
                cl.SetDataPath(isServerDataPath.Text);
                if (cl.DataPath == "NOTADIR")
                {
                    MessageBox.Show("Not a valid DEXIS 9 Database. Please try again");
                    isServerDataPath.Clear();
                }
                else
                {
                    this.Hide();
                    new MainWindow(_version, cl.DataPath);
                }
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
