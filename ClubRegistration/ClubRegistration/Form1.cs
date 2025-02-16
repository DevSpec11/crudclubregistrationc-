using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubRegistration
{
    public partial class Form1 : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        private int count = 1;

        public Form1()
        {
            InitializeComponent();
            clubRegistrationQuery = new ClubRegistrationQuery();

        }
        private void combobox()
        {
            cbGender.Items.AddRange(new string[] { "Male", "Female" });
            cboProgram.Items.AddRange(new string[] { "BSIT", "BSCpE", "BSTM", "BSHM", "BSBA" });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = GenerateRegistrationID();
                long studentId = Convert.ToInt64(txtStudent.Text);
                string firstName = txt1FirstName.Text;
                string middleName = txt1MiddleName.Text;
                string lastName = txt1LastName.Text;
                int age = Convert.ToInt32(txt1Age.Text);
                string gender = cbGender.Text;
                string program = cboProgram.Text;

                clubRegistrationQuery.RegisterStudent(ID, studentId, firstName, middleName, lastName, age, gender, program);
                MessageBox.Show("Student registered successfully!");
                RefreshClubMembersList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration failed: {ex.Message}");
            }

        }
        private void RefreshClubMembersList()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView1.DataSource = clubRegistrationQuery.BindingSource;
        }
        private int GenerateRegistrationID()
        {
            return count++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmUpdateMember updateForm = new FrmUpdateMember();
            updateForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshClubMembersList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshClubMembersList();
            combobox();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
