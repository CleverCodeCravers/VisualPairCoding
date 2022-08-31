using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualPairCoding.WinForms
{
    public partial class AboutForm : Form
    {
        string _appVersion;
        string _appUrl;
        string _appName;

        public AboutForm()
        {
            InitializeComponent();
        }

        public AboutForm(string appVersion, string appUrl, string appName)
        {
            InitializeComponent();
            _appVersion = appVersion;
            _appUrl = appUrl;
            _appName = appName;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void appGithubPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo psInfo = new ProcessStartInfo
            {
                FileName = _appUrl,
                UseShellExecute = true
            };
            Process.Start(psInfo);
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            appNameLabel.Text = _appName + " v." + _appVersion;
        }
    }
}
