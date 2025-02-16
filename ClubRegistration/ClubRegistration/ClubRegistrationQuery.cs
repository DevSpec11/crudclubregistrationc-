using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubRegistration
{
    public class ClubRegistrationQuery
    {
        private readonly string connectionString = @"Data Source=laptop-49156\sqlexpress01;Initial Catalog=ClubDB;Integrated Security=True;Encrypt=False";
        private SqlConnection sqlConnection;
        public BindingSource BindingSource { get; private set; }
        public string FirstName, MiddleName, LastName, Gender, Program;
        public int Age;

        public ClubRegistrationQuery()
        {
            sqlConnection = new SqlConnection(connectionString);
            BindingSource = new BindingSource();
        }
        public void DisplayList()
        {
            string query = "SELECT StudentId, FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers";
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlAdapter.Fill(dataTable);
            BindingSource.DataSource = dataTable;
        }

        public void RegisterStudent(int ID, long studentId, string firstName, string middleName, string lastName, int age, string gender, string program)
        {
            string query = "INSERT INTO ClubMembers (ID,StudentId, FirstName, MiddleName, LastName, Age, Gender, Program) VALUES (@ID, @StudentId, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)";
            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@ID", ID);
                sqlCommand.Parameters.AddWithValue("@StudentId", studentId);
                sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
                sqlCommand.Parameters.AddWithValue("@MiddleName", middleName);
                sqlCommand.Parameters.AddWithValue("@LastName", lastName);
                sqlCommand.Parameters.AddWithValue("@Age", age);
                sqlCommand.Parameters.AddWithValue("@Gender", gender);
                sqlCommand.Parameters.AddWithValue("@Program", program);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void UpdateStudent(long studentId, string firstname, string midname, string lastname, int age, string program, string gender)
        {
            string query = "UPDATE ClubMembers SET FirstName = @firstname, MiddleName = @midname, LastName = @lastname, Age = @age, Gender = @gender, Program = @program WHERE StudentID = @studentID";
            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@firstname", firstname);
                sqlCommand.Parameters.AddWithValue("@midname", midname);
                sqlCommand.Parameters.AddWithValue("@lastname", lastname);
                sqlCommand.Parameters.AddWithValue("@age", age);
                sqlCommand.Parameters.AddWithValue("@gender", gender);
                sqlCommand.Parameters.AddWithValue("@program", program);
                sqlCommand.Parameters.AddWithValue("@studentID", studentId);


                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void DisplayID(ComboBox comboBox)
        {
            string query = "SELECT StudentId FROM ClubMembers";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlConnection.Open();
            SqlDataReader sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                comboBox.Items.Add(sqlReader["StudentId"]);
            }
            sqlConnection.Close();
        }

        public void DisplayText(string id)
        {
            string query = "SELECT * FROM ClubMembers WHERE StudentId = @Id";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            sqlConnection.Open();
            SqlDataReader sqlReader = sqlCommand.ExecuteReader();
            if (sqlReader.Read())
            {
                FirstName = sqlReader["FirstName"].ToString();
                MiddleName = sqlReader["MiddleName"].ToString();
                LastName = sqlReader["LastName"].ToString();
                Age = Convert.ToInt32(sqlReader["Age"]);
                Gender = sqlReader["Gender"].ToString();
                Program = sqlReader["Program"].ToString();
            }
            sqlConnection.Close();
        }
    }
}

