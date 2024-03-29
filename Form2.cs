using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment3_Student_Management_Sysytem
{
    public partial class Form2 : Form
    {
        private const string studentFilePath = "../../students.txt";
        private List<Student> students;

        public Form2()
        {
            InitializeComponent();
            LoadStudent();
            PopulateDataGridView();
            
        }

        private void LoadStudent()
        {
            students = new List<Student>();
            if (File.Exists(studentFilePath))
            {
                string[] studentData = File.ReadAllLines(studentFilePath);
                foreach (string line in studentData)
                {
                    string[] data = line.Split(',');
                    Student student = new Student
                    {
                        StudentID = int.Parse(data[0]),
                        FirstName = data[1],
                        LastName = data[2],
                        Age = int.Parse(data[3]),
                        Gender = data[4],
                        ClassName = data[5],
                        Grade = double.Parse(data[6])
                    };
                    students.Add(student);
                }
            }
            else
            {
                MessageBox.Show("Student file not found.");
            }
        }

        private void PopulateDataGridView()
        {
            dataGridView1.DataSource = students;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int studentID = int.Parse(txtStudentId.Text);
                string firstName = txtFirstname.Text;
                string lastName = txtLastname.Text;
                int age = int.Parse(txtAge.Text);
                string gender = txtGender.Text;
                string className = txtClassname.Text;
                double grade = double.Parse(txtGrade.Text);

                Student student = new Student
                {
                    StudentID = studentID,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Gender = gender,
                    ClassName = className,
                    Grade = grade
                };
                students.Add(student);
                RefreshDataGridView1();
                SaveStudentsToFile();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridView1.CurrentCell.RowIndex;
                int studentID = int.Parse(txtStudentId.Text);
                string firstName = txtFirstname.Text;
                string lastName = txtLastname.Text;
                int age = int.Parse(txtAge.Text);
                string gender = txtGender.Text;
                string className = txtClassname.Text;
                double grade = double.Parse(txtGrade.Text);

                students[index].StudentID = studentID;
                students[index].FirstName = firstName;
                students[index].LastName = lastName;
                students[index].Age = age;
                students[index].Gender = gender;
                students[index].ClassName = className;
                students[index].Grade = grade;

                RefreshDataGridView1();
                SaveStudentsToFile();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridView1.CurrentCell.RowIndex;

                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this student?", "Delete Student", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    students.RemoveAt(index);
                    RefreshDataGridView1();
                    SaveStudentsToFile();
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text;
            var searchResults = students.Where(student => student.FirstName.Contains(searchQuery) || student.LastName.Contains(searchQuery)).ToList();
            dataGridView1.DataSource = searchResults;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RefreshDataGridView1()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = students;
        }

        private void SaveStudentsToFile()
        {
            using (StreamWriter writer = new StreamWriter(studentFilePath))
            {
                foreach (var student in students)
                {
                    writer.WriteLine($"{student.StudentID},{student.FirstName},{student.LastName},{student.Age},{student.Gender},{student.ClassName},{student.Grade}");
                }
            }
        }

        private void ClearInputFields()
        {
            txtStudentId.Clear();
            txtFirstname.Clear();
            txtLastname.Clear();
            txtAge.Clear();
            txtGender.Clear();
            txtClassname.Clear();
            txtGrade.Clear();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            Student selectedStudent = students[index];
            txtStudentId.Text = selectedStudent.StudentID.ToString();
            txtFirstname.Text = selectedStudent.FirstName;
            txtLastname.Text = selectedStudent.LastName;
            txtAge.Text = selectedStudent.Age.ToString();
            txtGender.Text = selectedStudent.Gender.ToString();
            txtClassname.Text = selectedStudent.ClassName;
            txtGrade.Text = selectedStudent.Grade.ToString();
        }

        private void btnShowall_Click(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }
    }
}
