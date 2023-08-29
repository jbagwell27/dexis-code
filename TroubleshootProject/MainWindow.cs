using System;
using System.IO;
using System.Windows.Forms;

namespace TroubleshootProject
{
    public partial class MainWindow : Form
    {
        private NoteTemplate Template;
        private LogWriter Logger;
        private readonly ContextMenuStrip ComputerRoundMenuStrip;
        private readonly ContextMenuStrip HardwareRoundMenuStrip;
        private string SelectedMenuItem;

        //private string FinalText = "";

        public MainWindow()
        {
            InitializeComponent();
            Template = new NoteTemplate();
            Logger = new LogWriter();
            CaseAssetsTab.Enabled = false;
            PdsVersionPanel.Enabled = false;
            RemoteSessionTab.Enabled = false;
            NotePreviewTab.Enabled = false;
            PdsVersionPanel.Visible = false;
            PdsVersionBox.Enabled = false;

            SetFont();
            FillDropdowns();

            FirstTimeIssueBox.Visible = false;
            IssueVerifiedBox.Visible = false;
            DeviceListBox.Visible = false;
            WindowsUpdatedBox.Visible = false;
            WindowsVersionBox.Visible = false;
            WindowsArchBox.Visible = false;
            WindowsTypeBox.Visible = false;




            var removeCompMenuItem = new ToolStripMenuItem { Text = "Remove Item" };
            removeCompMenuItem.Click += RemoveCompMenuItem_Click;
            ComputerRoundMenuStrip = new ContextMenuStrip();
            ComputerRoundMenuStrip.Items.Add(removeCompMenuItem);
            CompList.MouseDown += new MouseEventHandler(CompList_MouseDown);

            var removeHwMenuItem = new ToolStripMenuItem { Text = "Remove Item" };
            removeHwMenuItem.Click += RemoveHwMenuItem_Click;
            HardwareRoundMenuStrip = new ContextMenuStrip();
            HardwareRoundMenuStrip.Items.Add(removeHwMenuItem);
            CompList.MouseDown += new MouseEventHandler(HwList_MouseDown);

            WindowsVersionBox.Enabled = false;
            WindowsTypeBox.Enabled = false;
            WindowsArchBox.Enabled = false;
            DeviceListBox.Enabled = false;
            FirstTimeIssueBox.Enabled = false;
            WindowsUpdatedBox.Enabled = false;
            IssueVerifiedBox.Enabled = false;

            DEXISVersionBox.DropDownStyle = ComboBoxStyle.DropDownList;
            PmsVersionBox.DropDownStyle = ComboBoxStyle.DropDownList;
            WindowsVersionBox.DropDownStyle = ComboBoxStyle.DropDownList;
            WindowsTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            WindowsArchBox.DropDownStyle = ComboBoxStyle.DropDownList;
            DeviceListBox.DropDownStyle = ComboBoxStyle.DropDownList;
            PdsVersionBox.DropDownStyle = ComboBoxStyle.DropDownList;
            FirstTimeIssueBox.DropDownStyle = ComboBoxStyle.DropDownList;
            IssueVerifiedBox.DropDownStyle = ComboBoxStyle.DropDownList;
            WindowsUpdatedBox.DropDownStyle = ComboBoxStyle.DropDownList;

            new LogWriter();
        }



        public void FillDropdowns()
        {
            //Windows Versions
            foreach (var item in Template.WindowsVersions)
            {
                WindowsVersionBox.Items.Add(item);
            }
            //DEXISVersions
            foreach (var item in Template.DEXISVersions)
            {
                DEXISVersionBox.Items.Add(item);
            }
            //PMSVersions
            foreach (var item in Template.PmsVersions)
            {
                PmsVersionBox.Items.Add(item);
            }
            //Devices
            foreach (var item in Template.Devices)
            {
                DeviceListBox.Items.Add(item);
            }
            //TitaniumVersions
            foreach (var item in Template.PdsVersions)
            {
                PdsVersionBox.Items.Add(item);
            }
        }

        private void SetFont()
        {
            ContactNameBox.Font = new System.Drawing.Font(Template.FontFamily, Template.FontSize);
            PhoneNumberBox.Font = new System.Drawing.Font(Template.FontFamily, Template.FontSize);
            DatabasePathBox.Font = new System.Drawing.Font(Template.FontFamily, 10);
            ProblemBox.Font = new System.Drawing.Font(Template.FontFamily, Template.FontSize);
            StepBox.Font = new System.Drawing.Font(Template.FontFamily, Template.FontSize); ;
            ResolutionBox.Font = new System.Drawing.Font(Template.FontFamily, Template.FontSize);
            NotePreviewBox.Font = new System.Drawing.Font(Template.FontFamily, Template.FontSize);
        }

        private void SetDevice()
        {

            Template.Device = DeviceType();
            if (Template.Device == "Titanium")
            {
                if (PdsVersion() == "")
                {
                    MessageBox.Show("Polaris Driver Software Version cannot be empty");
                    return;
                }
                else
                {
                    Template.PdsVersion = PdsVersion();
                }
            }
            if (Template.Device == "KaVo IXS")
            {
                if (PdsVersion() == "")
                {
                    MessageBox.Show("Polaris Driver Software Version cannot be empty");
                    return;
                }
                else
                {
                    Template.PdsVersion = PdsVersion();
                }
            }
        }

        private void AddCompButton_Click(object sender, EventArgs e)
        {
            if (OsNameBox.Text == "" || WindowsVersion() == "" || WindowsArch() == "" || (WindowsType() == "" && WindowsTypePanel.Enabled))
            {
                MessageBox.Show("No entry can be empty");
            }
            else
            {
                string computer = Template.OS_StringBuilder(OsNameBox.Text, "Windows", WindowsVersion(), WindowsType(), WindowsArch());
                Template.AddComp(computer);
                CompList.Items.Add(computer);
                OsNameBox.Clear();

                ClearCompCheckBoxes();
                this.Update();
            }
            this.ActiveControl = OsNameBox;
        }

        //Remote Session Tab Radio Selections
        private string WindowsVersion()
        {
            foreach (var control in WindowsVersionPanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    return radio.Text;
                }
            }
            return "";
        }

        private string WindowsArch()
        {
            foreach (var control in WindowsArchPanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    return radio.Text;
                }
            }
            return "";
        }

        private string WindowsType()
        {
            foreach (var control in WindowsTypePanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    return radio.Text;
                }
            }
            return "";
        }

        //Case Assets Tab Radio Selections
        private string DeviceType()
        {
            foreach (var control in DeviceListPanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    return radio.Text;
                }
            }
            return "";
        }

        private string PdsVersion()
        {
            foreach (var control in PdsVersionPanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    return radio.Text;
                }
            }
            return "";
        }
        private string WindowsUpdated()
        {
            foreach (var control in WindowsUpdatePanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    return radio.Text;
                }
            }
            return "";
        }

        //Main Tab Radio Selections
        private string IssueVerified()
        {

            foreach (var control in VerifiedIssuePanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    return radio.Text;
                }
            }
            return "";
        }

        private string FirstTimeIssue()
        {

            foreach (var control in FirstIssuePanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    return radio.Text;
                }
            }
            return "";
        }


        private void ClipboardButton_Click(object sender, EventArgs e)
        {
            Template.ContactName = ContactNameBox.Text;
            Template.PhoneNumber = PhoneNumberBox.Text;
            Template.PmsVersion = PmsVersionBox.Text;
            Template.DEXISVersion = DEXISVersionBox.Text;
            Template.DatabasePath = DatabasePathBox.Text;
            Template.FirstTimeIssue = FirstTimeIssue();
            Template.WindowsUpdated = WindowsUpdated();
            Template.IssueVerified = IssueVerified();


            if (ContactNameBox.Text == "" || PhoneNumberBox.Text == "" || ProblemBox.Text == "" || StepBox.Text == "" || ResolutionBox.Text == "" ||
                DEXISVersionBox.Text == "" || PmsVersionBox.Text == "" || FirstTimeIssue() == "" || IssueVerified() == "")
            {
                MessageBox.Show("All Required fields must be used");
                return;
            }
            if (Template.IsHardware)
            {
                if (Template.CaseAssets.Count == 0)
                {
                    MessageBox.Show("At least one Device must be selected");
                    return;
                }
            }
            if (Template.IsRemote)
            {
                if (Template.Computers.Count == 0)
                {
                    MessageBox.Show("At least one Computer must be selected");
                    return;
                }
            }


            Template.Problem = ProblemBox.Text;
            Template.TroubleshootingSteps = StepBox.Text;
            Template.Resolution = ResolutionBox.Text;

            Clipboard.SetText(Template.ToString());
            NotePreviewTab.Enabled = true;
            NotePreviewBox.Text = Template.ToString();

            Logger.AppendLog(Template.ToString());


        }

        private void OsVersionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (WindowsVersionBox.Text.Contains("Server"))
            //{
            //    OsTypeCombo.Text = " ";
            //    OsTypeCombo.Enabled = false;
            //}
            //else if (!WindowsVersionBox.Text.Contains("Server"))
            //{
            //    OsTypeCombo.Enabled = true;
            //}
        }

        private void HwCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (IsHwCheckBox.Checked)
            {
                Template.IsHardware = true;
                CaseAssetsTab.Enabled = true;
                MainTabController.SelectedTab = CaseAssetsTab;
                //VersionBox.Enabled = true;
            }
            else if (!IsHwCheckBox.Checked)
            {
                Template.IsHardware = false;
                CaseAssetsTab.Enabled = false;

            }
        }



        private void ClearDataButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all fields? Your changes will not be saved.", "Question",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ResetUI();

                Template.IsHardware = false;
                Template.IsRemote = false;
                Template.Device = "";
                Template.PmsVersion = "";
                Template.DEXISVersion = "";
                Template.HwSerialNumber = "";
                Template.Computers.Clear();
                Template.CaseAssets.Clear();
                Template.PdsVersion = "";
                Template.Problem = "";
                Template.TroubleshootingSteps = "";
                Template.Resolution = "";
                Template.DatabasePath = "";
                MainTabController.SelectedTab = MainContentsTab;
                this.ActiveControl = ContactNameBox;

            }

            //Console.WriteLine(richTextBox1.Text);
        }

        public void ResetUI()
        {
            ProblemBox.Text = "";
            StepBox.Text = "";
            ResolutionBox.Text = "";
            ContactNameBox.Text = "";
            PhoneNumberBox.Text = "";
            IsRemoteCheckBox.Checked = false;
            IsHwCheckBox.Checked = false;

            NotePreviewBox.Text = "";
            NotePreviewTab.Enabled = false;

            DEXISVersionBox.SelectedIndex = -1;
            PmsVersionBox.SelectedIndex = -1;
            PdsVersionBox.SelectedIndex = -1;
            DatabasePathBox.Text = "";
            SerialNumber.Text = "";
            CompList.Items.Clear();
            HwList.Items.Clear();

            ClearHwCheckBoxes();
            ClearMainCheckBoxes();
            ClearCompCheckBoxes();


            //PdsVersionBox.SelectedIndex = -1;
            //DevicesComboBox.SelectedIndex = -1;
            //FirstTimeIssueBox.SelectedIndex = -1;
            //IssueVerifiedBox.SelectedIndex = -1;
            //WindowsUpdatedBox.SelectedIndex = -1;
        }

        private void IsRemoteCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (IsRemoteCheckBox.Checked)
            {
                RemoteSessionTab.Enabled = true;
                Template.IsRemote = true;
                MainTabController.SelectedTab = RemoteSessionTab;

            }
            else if (!IsRemoteCheckBox.Checked)
            {
                RemoteSessionTab.Enabled = false;
                Template.IsRemote = false;
            }
        }

        private void ClearCompButton_Click_1(object sender, EventArgs e)
        {
            OsNameBox.Text = "";
            ClearCompCheckBoxes();
            Template.Computers.Clear();
            CompList.Items.Clear();
        }

        private void IncreaseFontButton_Click(object sender, EventArgs e)
        {
            ContactNameBox.Font = new System.Drawing.Font(ContactNameBox.Font.FontFamily, ContactNameBox.Font.Size + 1);
            PhoneNumberBox.Font = new System.Drawing.Font(PhoneNumberBox.Font.FontFamily, PhoneNumberBox.Font.Size + 1);
            ProblemBox.Font = new System.Drawing.Font(ProblemBox.Font.FontFamily, ProblemBox.Font.Size + 1);
            StepBox.Font = new System.Drawing.Font(StepBox.Font.FontFamily, StepBox.Font.Size + 1);
            ResolutionBox.Font = new System.Drawing.Font(ResolutionBox.Font.FontFamily, ResolutionBox.Font.Size + 1);
            NotePreviewBox.Font = new System.Drawing.Font(NotePreviewBox.Font.FontFamily, NotePreviewBox.Font.Size + 1);
        }

        private void DecreaseFontButton_Click(object sender, EventArgs e)
        {
            ContactNameBox.Font = new System.Drawing.Font(ContactNameBox.Font.FontFamily, ContactNameBox.Font.Size - 1);
            PhoneNumberBox.Font = new System.Drawing.Font(PhoneNumberBox.Font.FontFamily, PhoneNumberBox.Font.Size - 1);
            ProblemBox.Font = new System.Drawing.Font(ProblemBox.Font.FontFamily, ProblemBox.Font.Size - 1);
            StepBox.Font = new System.Drawing.Font(StepBox.Font.FontFamily, StepBox.Font.Size - 1);
            ResolutionBox.Font = new System.Drawing.Font(ResolutionBox.Font.FontFamily, ResolutionBox.Font.Size - 1);
            NotePreviewBox.Font = new System.Drawing.Font(NotePreviewBox.Font.FontFamily, NotePreviewBox.Font.Size - 1);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            SavePreferences();
        }

        private void SavePreferences()
        {
            string[] lines = File.ReadAllLines("settings.ini");

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("Size"))
                {
                    lines[i] = "Size=" + ProblemBox.Font.Size.ToString();
                }
            }
            File.WriteAllLines("settings.ini", lines);
        }

        private void HwComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Console.WriteLine(HwComboBox.Text);
            if (DeviceListBox.Text == "Titanium")
            {
                PdsVersionBox.Enabled = true;
            }
            else if (DeviceListBox.Text == "KaVo IXS")
            {
                PdsVersionBox.Enabled = true;
            }
            else
            {
                //Console.WriteLine(HwComboBox.SelectedItem.ToString());
                //Console.WriteLine("Version box is something else");
                PdsVersionBox.Enabled = false;
            }
        }

        private void StepBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                e.Handled = e.SuppressKeyPress = true;
                StepBox.SelectAll();
            }
        }

        private void ResolutionBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                e.Handled = e.SuppressKeyPress = true;
                ResolutionBox.SelectAll();
            }
        }

        private void ProblemBox_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void ProblemBox_MouseClick(object sender, MouseEventArgs e)
        {
            //Console.WriteLine(e.Clicks);
        }

        private void AboutLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new AboutForm();
        }

        private void ClearHwCheckBoxes()
        {
            foreach (var control in DeviceListPanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    radio.Checked = false;
                }
            }

            foreach (var control in PdsVersionPanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    radio.Checked = false;
                }
            }
        }

        private void ClearMainCheckBoxes()
        {

            foreach (var control in FirstIssuePanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    radio.Checked = false;
                }
            }
            foreach (var control in VerifiedIssuePanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    radio.Checked = false;
                }
            }

            foreach (var control in WindowsUpdatePanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    radio.Checked = false;
                }
            }
        }

        private void ClearCompCheckBoxes()
        {

            foreach (var control in WindowsVersionPanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    radio.Checked = false;
                }
            }
            foreach (var control in WindowsTypePanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    radio.Checked = false;
                }
            }

            foreach (var control in WindowsArchPanel.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    radio.Checked = false;
                }
            }

        }

        private void AddHwButton_Click(object sender, EventArgs e)
        {
            if (DeviceType() == "")
            {
                MessageBox.Show("Device Cannot be Empty");
            }
            else if (((DeviceType() == "Titanium" || DeviceType() == "KaVo IXS") && PdsVersionBox.Text == ""))
            {
                MessageBox.Show("Polaris Version Must be selected");
            }
            else
            {
                string hw = Template.CaseAsset_StringBuilder(DeviceType(), PdsVersionBox.Text, SerialNumber.Text);
                Template.AddHw(hw);
                HwList.Items.Add(hw);

                ClearHwCheckBoxes();
                PdsVersionBox.SelectedIndex = -1;
                SerialNumber.Text = "";

            }
        }

        private void ClearHwButton_Click(object sender, EventArgs e)
        {
            Template.CaseAssets.Clear();
            HwList.Items.Clear();
            DeviceListBox.SelectedIndex = -1;
            PdsVersionBox.SelectedIndex = -1;
            SerialNumber.Text = "";
            ClearHwCheckBoxes();
        }

        private void TitaniumRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (TitaniumRadio.Checked)
            {
                PdsVersionBox.Enabled = true;
            }
            if (!TitaniumRadio.Checked)
            {
                PdsVersionBox.SelectedIndex = -1;
                PdsVersionBox.Enabled = false;
            }
        }

        private void IXSRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (IXSRadio.Checked)
            {
                PdsVersionBox.Enabled = true;
            }
            if (!IXSRadio.Checked)
            {
                PdsVersionBox.SelectedIndex = -1;
                PdsVersionBox.Enabled = false;
            }
        }
        private void OtherHWRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (OtherHWRadio.Checked)
            {
                SerialNumber.Clear();
                SerialNumber.Enabled = false;
            }
            if (!OtherHWRadio.Checked)
            {
                SerialNumber.Enabled = true;
            }
        }

        private void ThirdPartyRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (ThirdPartyRadio.Checked)
            {
                SerialNumber.Clear();
                SerialNumber.Enabled = false;
            }
            if (!ThirdPartyRadio.Checked)
            {
                SerialNumber.Enabled = true;
            }
        }

        private void Server08Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (Server08Radio.Checked)
            {
                foreach (var control in WindowsTypePanel.Controls)
                {
                    RadioButton radio = control as RadioButton;

                    if (radio != null && radio.Checked)
                    {
                        radio.Checked = false;
                    }
                }

                WindowsTypePanel.Enabled = false;
            }
            else if (!Server08Radio.Checked)
            {
                WindowsTypePanel.Enabled = true;
            }
        }

        private void Server12Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (Server12Radio.Checked)
            {
                foreach (var control in WindowsTypePanel.Controls)
                {
                    RadioButton radio = control as RadioButton;

                    if (radio != null && radio.Checked)
                    {
                        radio.Checked = false;
                    }
                }

                WindowsTypePanel.Enabled = false;
            }
            else if (!Server12Radio.Checked)
            {
                WindowsTypePanel.Enabled = true;
            }
        }

        private void Server16Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (Server16Radio.Checked)
            {
                foreach (var control in WindowsTypePanel.Controls)
                {
                    RadioButton radio = control as RadioButton;

                    if (radio != null && radio.Checked)
                    {
                        radio.Checked = false;
                    }
                }

                WindowsTypePanel.Enabled = false;
            }
            else if (!Server16Radio.Checked)
            {
                WindowsTypePanel.Enabled = true;
            }
        }

        private void Server19Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (Server19Radio.Checked)
            {
                foreach (var control in WindowsTypePanel.Controls)
                {
                    RadioButton radio = control as RadioButton;

                    if (radio != null && radio.Checked)
                    {
                        radio.Checked = false;
                    }
                }

                WindowsTypePanel.Enabled = false;
            }
            else if (!Server19Radio.Checked)
            {
                WindowsTypePanel.Enabled = true;
            }
        }

        private void CompList_MouseDown(object sender, MouseEventArgs e)
        {
            int index = this.CompList.IndexFromPoint(e.Location);
            if (!e.Button.Equals((object)MouseButtons.Right))
            {
                return;
            }
            if (index != -1)
            {
                this.CompList.SelectedIndex = index;
                this.SelectedMenuItem = this.CompList.Items[index].ToString();
                this.ComputerRoundMenuStrip.Show(Cursor.Position);
                this.ComputerRoundMenuStrip.Visible = true;
            }
            else
                this.ComputerRoundMenuStrip.Visible = false;
        }

        private void RemoveCompMenuItem_Click(object sender, EventArgs e)
        {
            Template.Computers.RemoveAt(CompList.SelectedIndex);
            CompList.Items.Clear();
            foreach (var item in Template.Computers)
            {
                CompList.Items.Add(item);
            }
        }

        private void HwList_MouseDown(object sender, MouseEventArgs e)
        {
            int index = this.HwList.IndexFromPoint(e.Location);
            if (!e.Button.Equals((object)MouseButtons.Right))
            {
                return;
            }
            if (index != -1)
            {
                this.HwList.SelectedIndex = index;
                this.SelectedMenuItem = this.HwList.Items[index].ToString();
                this.HardwareRoundMenuStrip.Show(Cursor.Position);
                this.HardwareRoundMenuStrip.Visible = true;
            }
            else
                this.HardwareRoundMenuStrip.Visible = false;
        }
        private void RemoveHwMenuItem_Click(object sender, EventArgs e)
        {
            Template.CaseAssets.RemoveAt(HwList.SelectedIndex);
            HwList.Items.Clear();
            foreach (var item in Template.CaseAssets)
            {
                HwList.Items.Add(item);
            }
        }

        private void MainTabController_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                e.Cancel = true;
            }
            else if (e.TabPage == NotePreviewTab)
            {
                Template.ContactName = ContactNameBox.Text;
                Template.PhoneNumber = PhoneNumberBox.Text;
                Template.PmsVersion = PmsVersionBox.Text;
                Template.DEXISVersion = DEXISVersionBox.Text;
                Template.DatabasePath = DatabasePathBox.Text;
                Template.FirstTimeIssue = FirstTimeIssue();
                Template.WindowsUpdated = WindowsUpdated();
                Template.IssueVerified = IssueVerified();


                if (ContactNameBox.Text == "" || PhoneNumberBox.Text == "" || ProblemBox.Text == "" || StepBox.Text == "" || ResolutionBox.Text == "" ||
                    DEXISVersionBox.Text == "" || PmsVersionBox.Text == "" || FirstTimeIssue() == "" || IssueVerified() == "")
                {
                    MessageBox.Show("All Required fields must be used");
                    return;
                }
                if (Template.IsHardware)
                {
                    if (Template.CaseAssets.Count == 0)
                    {
                        MessageBox.Show("At least one Device must be selected");
                        return;
                    }
                }
                if (Template.IsRemote)
                {
                    if (Template.Computers.Count == 0)
                    {
                        MessageBox.Show("At least one Computer must be selected");
                        return;
                    }
                }

                Template.Problem = ProblemBox.Text;
                Template.TroubleshootingSteps = StepBox.Text;
                Template.Resolution = ResolutionBox.Text;

                NotePreviewBox.Text = Template.ToString();
            }
        }

        private void MultipleMachineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MultipleMachineCheckBox.Checked)
            {
                Template.IsMultipleMachines = true;
            }
            if (!MultipleMachineCheckBox.Checked)
            {
                Template.IsMultipleMachines = false;
            }
        }

    }
}
