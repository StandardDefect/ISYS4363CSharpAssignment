using System;
using System.CodeDom;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace ISYS4363CSharpAssignment
{
    public partial class Form1 : Form
    {
        //Establishes Global SQL rules and information
        SqlConnectionStringBuilder bldr;
        string server = null;
        string database = null;
        string username = null;
        string password = null;
        bool validConnection = false;
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader dataReader;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.Selecting += new TabControlCancelEventHandler(tabControl1_Selecting);
        }

        void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabPage current = (sender as TabControl).SelectedTab;
            if (validConnection == true)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("You are not logged in to a database. Please log in before continuing.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void connectButton_Click(object sender, EventArgs e)
        {
            server = serverTextbox.Text;
            database = databaseTextbox.Text;
            username = usernameTextbox.Text;
            password = passwordTextbox.Text;
            try
            {
                bldr = new SqlConnectionStringBuilder();
                bldr["Server"] = server;
                bldr["User ID"] = username;
                bldr.InitialCatalog = database;
                bldr.Password = password;
                connection = new SqlConnection(bldr.ConnectionString);
                connection.Open();
                validConnection = true;
                MessageBox.Show("Login Successful. You may now proceed to the rest of the application.",
                    "Login Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                connection.Close();
                serverTextbox.Text = "";
                databaseTextbox.Text = "";
                usernameTextbox.Text = "";
                passwordTextbox.Text = "";
            }
            catch
            {
                MessageBox.Show("Login failed. Please try again. Connection string is " + bldr.ConnectionString,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            DialogResult logout =
                MessageBox.Show(
                    "Are you sure you want to log out? You will not be able to access the rest of the application without logging in again.",
                    "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (logout == DialogResult.Yes)
            {
                server = null;
                database = null;
                username = null;
                password = null;
                validConnection = false;
            }
            else
            {

            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                MessageBox.Show("There is nothing to load on this tab. Try this button on the other tabs.",
                    "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                //Data Grid View
                connection = new SqlConnection(bldr.ConnectionString);
                var sql = "SELECT * FROM Student"; //Second SQL query 
                var dataadapter = new SqlDataAdapter(sql, connection);
                var ds = new DataSet(); //establishes ds as a DataSet
                dataadapter.Fill(ds); //Fills ds using SQL
                srDataGridView.DataSource =
                    ds.Tables[0]; 
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                //Data Grid View
                connection = new SqlConnection(bldr.ConnectionString);
                var sql = "SELECT * FROM Class"; //Second SQL query 
                var dataadapter = new SqlDataAdapter(sql, connection);
                var ds = new DataSet(); //establishes ds as a DataSet
                dataadapter.Fill(ds); //Fills ds using SQL
                crDataGridView.DataSource =
                    ds.Tables[0]; 
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                //Data Grid View
                connection = new SqlConnection(bldr.ConnectionString);
                var sql = "SELECT * FROM Instructor"; //Second SQL query 
                var dataadapter = new SqlDataAdapter(sql, connection);
                var ds = new DataSet(); //establishes ds as a DataSet
                dataadapter.Fill(ds); //Fills ds using SQL
                irDataGridView.DataSource =
                    ds.Tables[0];
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                //Data Grid View
                ssStudentCombobox.Items.Clear();
                connection = new SqlConnection(bldr.ConnectionString);
                var SQL = "SELECT Name FROM Student"; //Second SQL query 
                connection.Open();
                command = new SqlCommand(SQL, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    ssStudentCombobox.Items.Add(dataReader[0].ToString());
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            else if (tabControl1.SelectedIndex == 5)
            {
                //Data Grid View
                pcNameCombobox.Items.Clear();
                connection = new SqlConnection(bldr.ConnectionString);
                var SQL = "SELECT Name FROM Instructor"; //Second SQL query 
                connection.Open();
                command = new SqlCommand(SQL, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                   pcNameCombobox.Items.Add(dataReader[0].ToString());
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            else if (tabControl1.SelectedIndex == 6)
            {
                //Data Grid View
                roClassCombobox.Items.Clear();
                connection = new SqlConnection(bldr.ConnectionString);
                var SQL = "SELECT Class_Name FROM Class"; //Second SQL query 
                connection.Open();
                command = new SqlCommand(SQL, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    roClassCombobox.Items.Add(dataReader[0].ToString());
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                MessageBox.Show(
                    "This tab is for connecting to the SQL database. You must log in before accessing the rest of the application.",
                    "Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                MessageBox.Show(
                    "This tab is for viewing, adding, editing, or removing students. Hit the Reload button to get started. Select a student in the data grid to populate their information. Please note that, when making changes to the database, all fields except Major 2, Minor 1, and Minor 2 are required.",
                    "Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                MessageBox.Show(
                    "This tab is for viewing, adding, editing, or removing classes. Hit the Reload button to get started. Select a class in the data grid to populate their information. Please note that, when making changes to the database, the Instructor ID must be one present in the Instructor Report and all fields are required.",
                    "Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                MessageBox.Show(
                    "This tab is for viewing, adding, editing, or removing instructors. Hit the Reload button to get started. Select an instructor in the data grid to populate their information. Please note that, when making changes to the database, all fields are required.",
                    "Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                MessageBox.Show(
                    "This tab is for viewing student schedules based on student name. Hit the Reload button to get started. The combo box of student names will populate, though you may type in a name of your choice. Once you have selected a Student, hit the View button to see their schedule",
                    "Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (tabControl1.SelectedIndex == 5)
            {
                MessageBox.Show(
                    "This tab is for viewing the list of classes a professor teaches based on their name. Hit the Reload button to get started. The combo box of professor names will populate, though you may type in a name of your choice. Once you have selected a Student, hit the View button to see the list of classes.",
                    "Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (tabControl1.SelectedIndex == 6)
            {
                MessageBox.Show(
                    "This tab is for viewing the list of students and professors involved in a given class. Hit the Reload button to get started. The combo box of class names will populate, though you may type in a name of your choice. Once you have selected a Class, hit the View button to see the list of students and professors associated with it.",
                    "Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void srDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            srStudentIDTextbox.Text = srDataGridView.CurrentRow.Cells[0].Value.ToString();
            srNameTextbox.Text = srDataGridView.CurrentRow.Cells[1].Value.ToString();
            srAgeTextbox.Text = srDataGridView.CurrentRow.Cells[2].Value.ToString();
            srClassificationTextbox.Text = srDataGridView.CurrentRow.Cells[3].Value.ToString();
            srMajor1Textbox.Text = srDataGridView.CurrentRow.Cells[4].Value.ToString();
            srMajor2Textbox.Text = srDataGridView.CurrentRow.Cells[5].Value.ToString();
            srMinor1TextBox.Text = srDataGridView.CurrentRow.Cells[6].Value.ToString();
            srMinor2Textbox.Text = srDataGridView.CurrentRow.Cells[7].Value.ToString();
        }

        private void srAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(bldr.ConnectionString);
                int answer; //Returns the resultant number of rows affected by the query.
                string SQL =
                    "INSERT INTO Student VALUES (@Name, @Age, @Classification, @Major1, @Major2, @Minor1, @Minor2)";
                connection.Open();
                command = new SqlCommand(SQL, connection);
                command.Parameters.AddWithValue("@Name", srNameTextbox.Text);
                command.Parameters.AddWithValue("@Age", srAgeTextbox.Text);
                command.Parameters.AddWithValue("@Classification", srClassificationTextbox.Text);
                command.Parameters.AddWithValue("@Major1", srMajor1Textbox.Text);
                command.Parameters.AddWithValue("@Major2", srMajor2Textbox.Text);
                command.Parameters.AddWithValue("@Minor1", srMinor1TextBox.Text);
                command.Parameters.AddWithValue("@Minor2", srMinor2Textbox.Text);

                answer = command
                    .ExecuteNonQuery(); //Returns result of the query to C# to keep the application from breaking.

                command.Dispose();
                connection.Close();

                MessageBox.Show(answer + " rows have been entered into the database.", "Notification",
                    MessageBoxButtons.OK, MessageBoxIcon.Information); //Success Message
            }

            catch (Exception ex)
            {
                MessageBox.Show("The following error occurred:" + ex, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void srEditButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(bldr.ConnectionString);
                int answer;
                string ID = srDataGridView.CurrentRow.Cells[0].Value.ToString();
                string SQL =
                    "UPDATE Student SET Name = @Name, Age = @Age, Classification = @Classification, Major1 = @Major1, Major2 = @Major2, Minor1 = @Minor1, Minor2 = @Minor2 WHERE Student_ID = @ID";
                connection.Open();
                command = new SqlCommand(SQL, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@Name", srNameTextbox.Text);
                command.Parameters.AddWithValue("@Age", srAgeTextbox.Text);
                command.Parameters.AddWithValue("@Classification", srClassificationTextbox.Text);
                command.Parameters.AddWithValue("@Major1", srMajor1Textbox.Text);
                command.Parameters.AddWithValue("@Major2", srMajor2Textbox.Text);
                command.Parameters.AddWithValue("@Minor1", srMinor1TextBox.Text);
                command.Parameters.AddWithValue("@Minor2", srMinor2Textbox.Text);

                answer = command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                MessageBox.Show(answer + " rows have been updated in the database.", "Notification",
                    MessageBoxButtons.OK, MessageBoxIcon.Information); //Success Message

            }
            catch (Exception ex)
            {
                MessageBox.Show("The following error occurred:" + ex, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void srRemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(bldr.ConnectionString);
                int answer;
                string ID = srDataGridView.CurrentRow.Cells[0].Value.ToString();
                string SQL = "DELETE FROM Student WHERE Student_ID = @ID";
                connection.Open();
                command = new SqlCommand(SQL, connection);

                command.Parameters.AddWithValue("@ID", ID);
                answer = command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                MessageBox.Show(answer + " rows have been deleted from the database.", "Notification",
                    MessageBoxButtons.OK, MessageBoxIcon.Information); //Success Message
            }
            catch (Exception ex)
            {
                MessageBox.Show("The following error occurred:" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void crDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            crClassIDTextbox.Text = crDataGridView.CurrentRow.Cells[0].Value.ToString();
            crClassNameTextbox.Text = crDataGridView.CurrentRow.Cells[1].Value.ToString();
            crInstructorIDTextbox.Text = crDataGridView.CurrentRow.Cells[2].Value.ToString();
            crDaysTextbox.Text = crDataGridView.CurrentRow.Cells[3].Value.ToString();
            crStartTimeTextbox.Text = crDataGridView.CurrentRow.Cells[4].Value.ToString();
            crEndTimeTextbox.Text = crDataGridView.CurrentRow.Cells[5].Value.ToString();
            crDepartmentTextbox.Text = crDataGridView.CurrentRow.Cells[6].Value.ToString();
            crCourseNumberTextbox.Text = crDataGridView.CurrentRow.Cells[7].Value.ToString();
        }

        private void crAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(bldr.ConnectionString);
                int answer; //Returns the resultant number of rows affected by the query.
                string SQL =
                    "INSERT INTO Class VALUES(@Class_Name,@Instructor_ID,@Class_Days,@Start_Time,@End_Time,@Department,@Course_Number)";
                connection.Open();
                command = new SqlCommand(SQL, connection);

                command.Parameters.AddWithValue("@Class_Name", crClassNameTextbox.Text);
                command.Parameters.AddWithValue("@Instructor_ID", crInstructorIDTextbox.Text);
                command.Parameters.AddWithValue("@Class_Days", crDaysTextbox.Text);
                command.Parameters.AddWithValue("@Start_Time", crStartTimeTextbox.Text);
                command.Parameters.AddWithValue("@End_Time", crEndTimeTextbox.Text);
                command.Parameters.AddWithValue("@Department", crDepartmentTextbox.Text);
                command.Parameters.AddWithValue("@Course_Number", crCourseNumberTextbox.Text);

                answer = command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                MessageBox.Show(answer + " rows have been added to the database.", "Notification",
                    MessageBoxButtons.OK, MessageBoxIcon.Information); //Success Message
            }
            catch (Exception ex)
            {
                MessageBox.Show("The following error occurred:" + ex, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void crEditButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(bldr.ConnectionString);
                int answer;
                string ID = crDataGridView.CurrentRow.Cells[0].Value.ToString();
                string SQL =
                    "UPDATE Class SET Class_Name = @Class_Name, Instructor_ID = @Instructor_ID, Class_Days = @Class_Days, Start_Time = @Start_Time, End_Time = @End_Time, Department = @Department, Course_Number = @Course_Number WHERE Class_ID = @ID";
                connection.Open();
                command = new SqlCommand(SQL, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@Class_Name", crClassNameTextbox.Text);
                command.Parameters.AddWithValue("@Instructor_ID", crInstructorIDTextbox.Text);
                command.Parameters.AddWithValue("@Class_Days", crDaysTextbox.Text);
                command.Parameters.AddWithValue("@Start_Time", crStartTimeTextbox.Text);
                command.Parameters.AddWithValue("@End_Time", crEndTimeTextbox.Text);
                command.Parameters.AddWithValue("@Department", crDepartmentTextbox.Text);
                command.Parameters.AddWithValue("@Course_Number", crCourseNumberTextbox.Text);


                answer = command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                MessageBox.Show(answer + " rows have been updated in the database.", "Notification",
                    MessageBoxButtons.OK, MessageBoxIcon.Information); //Success Message

            }
            catch (Exception ex)
            {
                MessageBox.Show("The following error occurred:" + ex, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void crRemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(bldr.ConnectionString);
                int answer;
                string ID = crDataGridView.CurrentRow.Cells[0].Value.ToString();
                string SQL = "DELETE FROM Class WHERE Class_ID = @ID";
                connection.Open();
                command = new SqlCommand(SQL, connection);

                command.Parameters.AddWithValue("@ID", ID);
                answer = command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                MessageBox.Show(answer + " rows have been deleted from the database.", "Notification",
                    MessageBoxButtons.OK, MessageBoxIcon.Information); //Success Message
            }
            catch (Exception ex)
            {
                MessageBox.Show("The following error occurred:" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void irDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            irInstructorIDTextbox.Text = irDataGridView.CurrentRow.Cells[0].Value.ToString();
            irNameTextbox.Text = irDataGridView.CurrentRow.Cells[1].Value.ToString();
        }

        private void irAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(bldr.ConnectionString);
                int answer; //Returns the resultant number of rows affected by the query.
                string SQL =
                    "INSERT INTO Instructor VALUES(@Name)";
                connection.Open();
                command = new SqlCommand(SQL, connection);

                command.Parameters.AddWithValue("@Name", irNameTextbox.Text);

                answer = command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                MessageBox.Show(answer + " rows have been added to the database.", "Notification",
                    MessageBoxButtons.OK, MessageBoxIcon.Information); //Success Message
            }
            catch (Exception ex)
            {
                MessageBox.Show("The following error occurred:" + ex, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void irEditButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(bldr.ConnectionString);
                int answer;
                string ID = irDataGridView.CurrentRow.Cells[0].Value.ToString();
                string SQL =
                    "UPDATE Instructor SET Name = @Name WHERE Instructor_ID = @ID";
                connection.Open();
                command = new SqlCommand(SQL, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@Name", irNameTextbox.Text);

                answer = command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                MessageBox.Show(answer + " rows have been updated in the database.", "Notification",
                    MessageBoxButtons.OK, MessageBoxIcon.Information); //Success Message

            }
            catch (Exception ex)
            {
                MessageBox.Show("The following error occurred:" + ex, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void irRemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(bldr.ConnectionString);
                int answer;
                string ID = irDataGridView.CurrentRow.Cells[0].Value.ToString();
                string SQL = "DELETE FROM Instructor WHERE Instructor_ID = @ID";
                connection.Open();
                command = new SqlCommand(SQL, connection);

                command.Parameters.AddWithValue("@ID", ID);
                answer = command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();

                MessageBox.Show(answer + " rows have been deleted from the database.", "Notification",
                    MessageBoxButtons.OK, MessageBoxIcon.Information); //Success Message
            }
            catch (Exception ex)
            {
                MessageBox.Show("The following error occurred:" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ssViewButton_Click(object sender, EventArgs e)
        {
            //Data Grid View
            connection = new SqlConnection(bldr.ConnectionString);
            var sql =
                "SELECT Student.Name AS 'Student Name', Class.Class_Name,Instructor.Name AS 'Instructor_Name' FROM Student INNER JOIN Enrollment ON Student.Student_ID = Enrollment.Student_ID INNER JOIN Class ON Enrollment.Class_ID = Class.Class_ID INNER JOIN Instructor ON Class.Instructor_ID = Instructor.Instructor_ID WHERE Student.Name = '" + ssStudentCombobox.SelectedItem.ToString()+"'";
            var dataadapter = new SqlDataAdapter(sql, connection);
            var ds = new DataSet(); //establishes ds as a DataSet
            dataadapter.Fill(ds); //Fills ds using SQL
            ssDataGridView.DataSource =
                ds.Tables[0];
        }

        private void pcViewButton_Click(object sender, EventArgs e)
        {
            //Data Grid View
            connection = new SqlConnection(bldr.ConnectionString);
            var sql = "SELECT Instructor.Name AS 'Instructor Name', Class.Class_Name AS 'Class Name', Student.Name AS 'Student Name' FROM Student INNER JOIN Enrollment ON Student.Student_ID = Enrollment.Student_ID INNER JOIN Class ON Enrollment.Class_ID = Class.Class_ID INNER JOIN Instructor ON Class.Instructor_ID = Instructor.Instructor_ID WHERE Instructor.Name = '" + pcNameCombobox.SelectedItem.ToString() + "'";
            var dataadapter = new SqlDataAdapter(sql, connection);
            var ds = new DataSet(); //establishes ds as a DataSet
            dataadapter.Fill(ds); //Fills ds using SQL
            pcDataGridView.DataSource =
                ds.Tables[0];
        }

        private void roViewButton_Click(object sender, EventArgs e)
        {
            //Data Grid View
            connection = new SqlConnection(bldr.ConnectionString);
            var sql = "SELECT Class.Class_Name AS 'Class Name', Instructor.Name AS 'Instructor Name',  Student.Name AS 'Student Name' FROM Student INNER JOIN Enrollment ON Student.Student_ID = Enrollment.Student_ID INNER JOIN Class ON Enrollment.Class_ID = Class.Class_ID INNER JOIN Instructor ON Class.Instructor_ID = Instructor.Instructor_ID WHERE Class.Class_Name = '" + roClassCombobox.SelectedItem.ToString() + "'";
            var dataadapter = new SqlDataAdapter(sql, connection);
            var ds = new DataSet(); //establishes ds as a DataSet
            dataadapter.Fill(ds); //Fills ds using SQL
            roDataGridView.DataSource =
                ds.Tables[0];
        }
    }
}
