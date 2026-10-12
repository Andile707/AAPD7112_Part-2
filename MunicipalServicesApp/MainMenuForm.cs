using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
            cmbServices.SelectedIndex = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to exit Municipal Services?",
                "Exit Application",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (cmbServices.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a service.",
                    "Service Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string selectedService = cmbServices.SelectedItem.ToString();

            if (selectedService == "Report an Issue")
            {
                ReportIssueForm reportForm = new ReportIssueForm();

                this.Hide();
                reportForm.ShowDialog();
                this.Show();
            }
            else if (selectedService == "Local Events & Announcements")
            {
                LocalEventsForm eventsForm = new LocalEventsForm();

                this.Hide();
                eventsForm.ShowDialog();
                this.Show();
            }
            else if (selectedService == "Service Request Status")
            {
                MessageBox.Show(
                    "This service is not available yet.",
                    "Coming Soon",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
