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
    public partial class ReportIssueForm : Form
    {
        public ReportIssueForm()
        {
            InitializeComponent();
            cmbCategory.SelectedIndex = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ReportIssueForm_Load(object sender, EventArgs e)
        {
            cmbCategory.Items.Add("Roads");
            cmbCategory.Items.Add("Water and Sanitation");
            cmbCategory.Items.Add("Electricity");
            cmbCategory.Items.Add("Waste Management");
            cmbCategory.Items.Add("Public Safety");
            cmbCategory.Items.Add("Other");

            cmbCategory.SelectedIndex = -1;
        }

        private void UpdateProgress()
        {
            int progress = 0;

            if (!string.IsNullOrWhiteSpace(txtLocation.Text))
                progress += 25;

            if (cmbCategory.SelectedIndex != -1)
                progress += 25;

            if (!string.IsNullOrWhiteSpace(txtDescription.Text))
                progress += 25;

            if (!string.IsNullOrWhiteSpace(txtAttachment.Text))
                progress += 25;

            progressReport.Value = progress;

            if (progress == 0)
            {
                lblProgress.Text = "Start completing the report.";
            }
            else if (progress <= 50)
            {
                lblProgress.Text = "Good start! Keep going.";
            }
            else if (progress < 100)
            {
                lblProgress.Text = "Almost there!";
            }
            else
            {
                lblProgress.Text = "Great! Your report is ready to submit.";
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select an image or document";

            openFileDialog.Filter =
                "Supported Files|*.jpg;*.jpeg;*.png;*.pdf;*.doc;*.docx|" +
                "Image Files|*.jpg;*.jpeg;*.png|" +
                "PDF Files|*.pdf|" +
                "Word Documents|*.doc;*.docx|" +
                "All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtAttachment.Text = openFileDialog.FileName;

                UpdateProgress();
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Please enter the location of the issue.");
                txtLocation.Focus();
                return;
            }

            if (cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category.");
                cmbCategory.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please provide a description of the issue.");
                txtDescription.Focus();
                return;
            }

            // Generate reference number
            string referenceNumber =
                "MS-" + (IssueRepository.Issues.Count + 1).ToString("D4");

            // Create the issue
            ReportedIssue issue = new ReportedIssue
            {
                ReferenceNumber = referenceNumber,
                Location = txtLocation.Text.Trim(),
                Category = cmbCategory.SelectedItem.ToString(),
                Description = txtDescription.Text.Trim(),
                AttachmentPath = txtAttachment.Text,
                DateReported = DateTime.Now,
                Status = "Submitted"
            };

            // Store the issue
            IssueRepository.Issues.Add(issue);

            // Show confirmation
            MessageBox.Show(
                "Your issue has been submitted successfully.\n\n" +
                "Reference Number: " + referenceNumber +
                "\nStatus: Submitted",
                "Report Submitted",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Clear the form AFTER successful submission
            txtLocation.Clear();
            cmbCategory.SelectedIndex = -1;
            txtDescription.Clear();
            txtAttachment.Clear();

            UpdateProgress();

            txtLocation.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLocation.Clear();
            cmbCategory.SelectedIndex = -1;
            txtDescription.Clear();
            txtAttachment.Clear();

            UpdateProgress();

            txtLocation.Focus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void txtAttachment_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
