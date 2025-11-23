using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Activity_S_R
{
    public partial class FrmRegistration : Form
    {
        public FrmRegistration()
        {
            InitializeComponent();
        }

        private void FrmRegistration_Load(object sender, EventArgs e)
        {

        }

        private void rdoOtherr_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textname_TextChanged(object sender, EventArgs e)
        {

        }

        private void textEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void textPassword_TextChanged(object sender, EventArgs e)
        {
            textPassword.PasswordChar ='*';

        }

        private void btnSubmait_Click(object sender, EventArgs e)
        {
            // Validate Name 
            if (string.IsNullOrWhiteSpace(textName.Text))
            {
                MessageBox.Show("Name is required!", "Validation Error",
        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textName.Focus();
                return;
            }

            // Validate Email (Basic Email Format Check) 
            if (!Regex.IsMatch(textEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Enter a valid email address!", "Validation Error",
        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEmail.Focus();
                return;
            }

            // Validate Password (Min 6 characters) 
            if (textPassword.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters!",
"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textPassword.Focus();
                return;
            }

            // Validate Gender Selection 
            if (!rdoMale.Checked && !rdoFemale.Checked && !rdoOther.Checked)
            {
                MessageBox.Show("Please select a gender!", "Validation Error",
        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Country Selection 
            if (cmbCountry.SelectedItem == null)
            {
                MessageBox.Show("Please select a country!", "Validation Error",
        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCountry.Focus();
                return;
            }

            // Validate Favorite Color Selection 
            if (lblSelectedColor.Text == "No Color Selected")
            {
                MessageBox.Show("Please select your favorite color!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return;
            }

            // All validations passed → Proceed with displaying data 
            string name = textName.Text;
            string email = textEmail.Text;
            string password = textPassword.Text;
            string gender = rdoMale.Checked ? "Male" : rdoFemale.Checked ?
"Female" : "Other";
            string birthdate = dtpBirthdate.Value.ToShortDateString();
            string country = cmbCountry.SelectedItem.ToString();
            string color = lblSelectedColor.Text.Replace("Selected Color: ", ""); // 
            //Extracting selected color
// Displaying result 
lblResult.Text = $"Name: {name}\nEmail: {email}\nGender: {gender}\n" +
$"Birthdate: {birthdate}\nCountry: {country}\nFavorite Color: { color} "; }


        private void btnBickColor_Click(object sender, EventArgs e)
        {
            {
                ColorDialog colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    lblSelectedColor.Text = $"Selected Color: {colorDialog.Color.Name}";
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Clear text fields 
            textName.Text = "";
            textEmail.Text = "";
            textPassword.Text = "";
            // Uncheck radio buttons 
            rdoMale.Checked = false;
            rdoFemale.Checked = false;
            rdoOther.Checked = false;

            // Reset dropdown list selection 
            cmbCountry.SelectedIndex = -1;

            // Reset Date Picker to current date 
            dtpBirthdate.Value = DateTime.Now;

            // Reset favorite color label 
            lblSelectedColor.Text = "No Color Selected";

            // Clear result label 
            lblResult.Text = "";

            // Set focus back to the first input field 
            textName.Focus();
            // Clear student picture 
            picStudent.Image = null;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog 
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set file filters (allow only image files) 
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Display selected image in PictureBox 
                picStudent.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate fields before saving 
            if (string.IsNullOrWhiteSpace(textName.Text) ||
        string.IsNullOrWhiteSpace(textEmail.Text))
            {
                MessageBox.Show("Please fill in at least Name and Email before  saving!", "Validation Error", MessageBoxButtons.OK, 
        MessageBoxIcon.Warning);
                return;
            }

            // Prepare data in a structured text format 
            string data = $"{textName.Text}\n" +          // Name 
                          $"{textEmail.Text}\n" +         // Email 
                          $"{textPassword.Text}\n" +      // Password 
                          $"{(rdoMale.Checked ? "Male" : rdoFemale.Checked ? "Female" : "Other")}\n" + // Gender 
                  $"{dtpBirthdate.Value.ToShortDateString()}\n" + // Birthdate 
                  $"{cmbCountry.SelectedItem?.ToString()}\n" + // Country 
                  $"{lblSelectedColor.Text.Replace("Selected Color: ", "")}\n" + // Favorite Color 
                  $"{(picStudent.Image != null ? "student_picture.jpg" : "NoImage")}\n"; // Image Path 
 
    // Save to text file 
    File.WriteAllText("student_data.txt", data); 
 
    // Save the image if available
      if (picStudent.Image != null) 
    { 
        picStudent.Image.Save("student_picture.jpg"); // Save image locally 
    }

MessageBox.Show("Data saved successfully!", "Success", 
MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // Check if file exists 
            if (!File.Exists("student_data.txt"))
            {
                MessageBox.Show("No saved data found!", "Error",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Read all lines from the text file 
            string[] lines = File.ReadAllLines("student_data.txt");

            // Ensure file has the expected number of lines 
            if (lines.Length < 8)
            {
                MessageBox.Show("Saved data is incomplete or corrupted!", "Error",
        MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            // Populate form fields 
            textName.Text = lines[0];
            textEmail.Text = lines[1];
            textPassword.Text = lines[2];

            if (lines[3] == "Male") rdoMale.Checked = true;
            else if (lines[3] == "Female") rdoFemale.Checked = true;
            else rdoOther.Checked = true;

            dtpBirthdate.Value = DateTime.Parse(lines[4]);
            cmbCountry.SelectedItem = lines[5];
            lblSelectedColor.Text = "Selected Color: " + lines[6];

            // Load image if available 
            if (File.Exists("student_picture.jpg") && lines[7] == "student_picture.jpg")
            {
                picStudent.Image = Image.FromFile("student_picture.jpg");
            }
            else
            {
                picStudent.Image = null;
            }

            MessageBox.Show("Data loaded successfully!", "Success",
        MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
