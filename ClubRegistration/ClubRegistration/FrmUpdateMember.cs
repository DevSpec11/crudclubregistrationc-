using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClubRegistration
{
    public partial class FrmUpdateMember : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        public FrmUpdateMember()
        {
            InitializeComponent();
            clubRegistrationQuery = new ClubRegistrationQuery();
        }

      

        private void LoadStudentIDs()
        {
            clubRegistrationQuery.DisplayID(comboBoxID);
        }

        private void comboBoxID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedId = comboBoxID.Text;
                clubRegistrationQuery.DisplayText(selectedId);
                FillTextFields(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private void FillTextFields()
        {
            txtFirstName.Text = clubRegistrationQuery.FirstName;
            txtMiddleName.Text = clubRegistrationQuery.MiddleName;
            txtLastName.Text = clubRegistrationQuery.LastName;
            txtAge.Text = clubRegistrationQuery.Age.ToString();
            cbGender.Text = clubRegistrationQuery.Gender;
            cbPrograms.Text = clubRegistrationQuery.Program;
        }
        private void combobox()
        {
            cbGender.Items.AddRange(new string[] { "Male", "Female" });
            cbPrograms.Items.AddRange(new string[] { "BSIT", "BSCpE", "BSTM", "BSHM", "BSBA" });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                long studentId = Convert.ToInt64(comboBoxID.Text);
                string firstname = txtFirstName.Text;
                string midname = txtMiddleName.Text;
                string lastname = txtLastName.Text;
                int age = Convert.ToInt32(txtAge.Text);
                string gender = cbGender.Text;
                string program = cbPrograms.Text;

                clubRegistrationQuery.UpdateStudent(studentId,firstname,midname,lastname, age, program, gender);
                MessageBox.Show("Student updated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed: {ex.Message}");
            }
        }

        private void FrmUpdateMember_Load_1(object sender, EventArgs e)
        {
            combobox();
            LoadStudentIDs();
            comboBoxID.SelectedIndexChanged += comboBoxID_SelectedIndexChanged;
        }
    }
}
