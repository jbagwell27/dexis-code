using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;

namespace DEXISUtilitiesWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DEXIS9Logic D9;
        private DEXIS10Logic D10 = new DEXIS10Logic();
        private DEXIS10Frame D10frame = new DEXIS10Frame();
        public MainWindow(int version, string path)
        {
            InitializeComponent();

            D9 = new DEXIS9Logic();

            this.Hide();
            if (!D9.IsServer())
            {
                D9.GetDataPathFromIni();
            }
            else
            {
                D9.DataPath = path;
                isWSCheckBox.IsEnabled = false;
            }

            switch (version)
            {
                case 9:
                    dexis10Tab.IsEnabled = false;
                    dexis10Radio.IsEnabled = false;
                    dexis9Radio.IsChecked = true;
                    break;
                case 10:
                    dexis9Tab.IsEnabled = false;
                    dexis9Radio.IsEnabled = false;
                    dexis10Radio.IsChecked = true;
                    dexis10Tab.IsSelected = true;
                    break;
                default:
                    break;
            }

            DEXIS9Startup();
            DEXIS10Startup();
            ExtrasStartup();

            this.Show();
        }

        private void DEXIS9Startup()
        {
            patient9Name.Text = "---";
            openFolderButton.IsEnabled = false;
            deleteLckFileButton.IsEnabled = false;
            changeProvButton.IsEnabled = false;
            clearBirthButton.IsEnabled = false;
        }

        private void DEXIS10Startup()
        {
            patient10Name.Text = "---";

        }

        private void ExtrasStartup()
        {
            BackupSetup();
        }
        private void MainTabController_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dexis9Tab.IsSelected)
                mainTitle.Text = "DEXIS 9 Server Utilities";
            if (dexis10Tab.IsSelected)
                mainTitle.Text = "DEXIS 10 Server Utilities";
            if (extrasTab.IsSelected)
                mainTitle.Text = "Extra Tools";
            if (aboutTab.IsSelected)
                mainTitle.Text = "DEXmaintenance 2.0";
        }

        public MainWindow() { }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        //DEXIS 9 tab Actions
        private void Set9PatientButton_Click(object sender, RoutedEventArgs e)
        {
            D9.Id = patient9IDBox.Text;
            if (D9.GetPatientName(D9.Id) == "NO1234")
            {
                MessageBox.Show("ID not valid. Please try again.");
                patient9Name.Text = "---";
                openFolderButton.IsEnabled = false;
                deleteLckFileButton.IsEnabled = false;
                changeProvButton.IsEnabled = false;
                clearBirthButton.IsEnabled = false;
            }
            else
            {
                patient9Name.Text = D9.GetPatientName(D9.Id);
                D9.FillPatientList(D9.Id);
                DEXIS9ImageCollection.Items.Clear();
                foreach (var image in D9.ImageFiles)
                {
                    DEXIS9ImageCollection.Items.Add(image);
                } 
                openFolderButton.IsEnabled = true;
                deleteLckFileButton.IsEnabled = true;
                changeProvButton.IsEnabled = true;
                clearBirthButton.IsEnabled = true;
            }
        }

        private void OpenFolderButton_Click(object sender, RoutedEventArgs e)
        {


            if ((bool)(allPatientCheckBox.IsChecked))
            {
                if (D9.OpenPatientFolder(patient9IDBox.Text) == 0)
                {
                    MessageBox.Show("Please enter a valid ID");
                }
            }
            else
            {
                D9.OpenPatientFolder(D9.Id);
            }
        }

        private void AllPatientCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)(allPatientCheckBox.IsChecked))
            {
                D9.Id = "";
                patient9Name.Text = "---";
                patient9IDBox.Text = "";
                D9.ImageFiles.Clear();
                DEXIS9ImageCollection.Items.Clear();
                set9PatientButton.IsEnabled = false;
                openFolderButton.IsEnabled = true;
                deleteLckFileButton.IsEnabled = false;
                changeProvButton.IsEnabled = true;
                clearBirthButton.IsEnabled = true;
            }
            else
            {
                set9PatientButton.IsEnabled = true;
                openFolderButton.IsEnabled = false;
                deleteLckFileButton.IsEnabled = false;
                changeProvButton.IsEnabled = false;
                clearBirthButton.IsEnabled = false;
            }
        }

        private void DeleteLckFileButton_Click(object sender, RoutedEventArgs e)
        {
            D9.DeleteLckFile(D9.Id);
            MessageBox.Show("Lck File for " + D9.GetPatientName(D9.Id) + " has been removed.");
        }

        private void ChangeProvButton_Click(object sender, RoutedEventArgs e)
        {
            if (oldProvBox.Text == "" || newProvBox.Text == "")
            {
                MessageBox.Show("Please enter a provider ID in BOTH fields.");
            }
            else
            {
                if ((bool)(allPatientCheckBox.IsChecked))
                {
                    D9.ChangeProvider(oldProvBox.Text, newProvBox.Text);
                    MessageBox.Show("Provider changed for all patients");
                }
                else
                {
                    D9.ChangeProvider(oldProvBox.Text, newProvBox.Text, D9.Id);
                    MessageBox.Show("Provider changed for " + D9.GetPatientName(D9.Id) + ".");
                }
            }
        }

        private void ClearBirthButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)(allPatientCheckBox.IsChecked))
            {
                D9.ClearBirthDate();
                MessageBox.Show("Birthdate cleared for all patients.");
            }
            else
            {
                D9.ClearBirthDate(D9.Id);
                MessageBox.Show("Birthdate cleared for " + D9.GetPatientName(D9.Id) + ".");
            }
        }
        private void ForceRebuildButton_Click(object sender, RoutedEventArgs e)
        {
            D9.ForceRebuild();
            MessageBox.Show("Rebuild Complete. Please launch DEXIS to start Reindex.");
        }

        private void DeleteEmptyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Make sure you backup your database!!!!!! \n" +
                "Also make sure everyone is out of DEXIS");
            D9.DeleteEmptyPatients();

            MessageBox.Show("Reindex was performed. Please launch DEXIS to rebuild the index");
        }

        private void DEXIS9ImageCollection_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(DEXIS9ImageCollection.SelectedItem.ToString());
        }

        //DEXIS 10 Tab Actions
        private void Set10PatientButton_Click(object sender, RoutedEventArgs e)
        {
            D10._id = patient10IDBox.Text;
            patient10Name.Text = D10frame.PatientName(D10._id);
            imageCollection.Items.Clear();
            if (startDate.SelectedDate == null || endDate.SelectedDate == null)
            {
                D10._files = D10frame.ImageList(D10._id);
                //d10.ImagesByID(d10._id);
                foreach (string s in D10._files)
                {
                    imageCollection.Items.Add(s);
                }
            }
            else
            {
                D10.ImagesByDate(startDate.SelectedDate.ToString(), endDate.SelectedDate.ToString(), D10._id);
                foreach (string s in D10._files)
                {
                    imageCollection.Items.Add(s);
                }
            }
        }
        private void CheckCorruptButton_Click(object sender, RoutedEventArgs e)
        {
            imageCollection.Items.Clear();
            D10.GetSmallFiles();
            foreach (string s in D10._files)
            {
                imageCollection.Items.Add(s);
            }
        }

        public void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            string filePath = imageCollection.SelectedItem.ToString();
            D10.OpenFile(filePath);
        }

        public void OpenContainingFolder_Click(object sender, RoutedEventArgs e)
        {
            string filePath = imageCollection.SelectedItem.ToString();
            D10.OpenContainingFolder(filePath);
        }

        private void ImageCollection_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string filePath = imageCollection.SelectedItem.ToString();
            D10.OpenFile(filePath);
        }

        //Extra Tab Actions
        private void BackupSetup()
        {
            extraTitle.Text = "Backup";
            pathBoxHint.Text = "Please enter a destination path for the backup.";
            extraStartButton.Content = "Start Backup";
        }
        private void MigrateSetup()
        {
            extraTitle.Text = "Server Migration";
            pathBoxHint.Text = "Please enter the data path for the new server.";
            extraStartButton.Content = "Change Server Path";
        }

        private void IsWSCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)(isWSCheckBox.IsChecked))
            {
                MigrateSetup();
            }
            else
            {
                BackupSetup();
            }
        }

        public int version()
        {
            if ((bool)(dexis9Radio.IsChecked))
                return 9;
            else if ((bool)(dexis9Radio.IsChecked))
                return 10;
            else
                return 0;
        }

        public void DEXIS9Extra()
        {
            try
            {
                Path.GetFullPath(pathBox.Text);

                if ((bool)(isWSCheckBox.IsChecked))
                {
                    D9.Migration(9, pathBox.Text);
                    MessageBox.Show("dexis.ini file updated with " + pathBox.Text + " as new server path");
                }
                else
                {
                    Thread.Sleep(500);
                    pathBoxHint.Text = "Backup in progress....";
                    Thread.Sleep(500);
                    D9.Backup(D9.DataPath, pathBox.Text);
                    pathBoxHint.Text = "Backup Complete";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid path");
            }
        }
        public void DEXIS10Extra()
        {
            try
            {
                Path.GetFullPath(pathBox.Text);

                if ((bool)(isWSCheckBox.IsChecked))
                {
                    D9.Migration(10, pathBox.Text);
                    MessageBox.Show("dexis.ini file updated with " + pathBox.Text + " as new server path");
                }
                else
                {
                    Thread.Sleep(500);
                    pathBoxHint.Text = "Backup in progress....";
                    Thread.Sleep(500);
                    D10.Backup(pathBox.Text);
                    pathBoxHint.Text = "Backup Complete";
                }
            }
            catch (Exception)
            {
                //throw;
                MessageBox.Show("Please enter a valid Path");
            }
        }

        private void ExtraStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(bool)(dexis9Radio.IsChecked) && !(bool)(dexis10Radio.IsChecked))
            {
                MessageBox.Show("Please select a version.");
            }
            if (pathBox.Text == "")
            {
                MessageBox.Show("Path cannot be empty");
            }
            else
            {
                if ((bool)(dexis9Radio.IsChecked))
                    DEXIS9Extra();
                else if ((bool)(dexis10Radio.IsChecked))
                    DEXIS10Extra();
            }
        }
        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            //BackgroundWorker worker = new BackgroundWorker();
            //worker.WorkerReportsProgress = true;
            //worker.DoWork += Worker_DoWork;
            //worker.ProgressChanged += Worker_ProgressChanged;

            //worker.RunWorkerAsync();
        }
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                pathBox.Text = dialog.SelectedPath;
            }
        }

        
    }
}
