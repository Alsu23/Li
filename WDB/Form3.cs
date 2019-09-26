using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WDB
{
    public partial class Form3 : Form
    {
        OperationsWithDB OperDB = new OperationsWithDB();
        SqlConnection sqlConnection1=new SqlConnection();
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
            OperDB.CloseConnection(sqlConnection1);
            Close();
        }

        public void Form3_Load(object sender, EventArgs e)
        {
            sqlConnection1=OperDB.ConnectionWithDB();

            List<Exam> ExList = new List<Exam>();

            ExList = OperDB.CreateExList(sqlConnection1);

            foreach (Exam ex in ExList)
                listBox1.Items.Add(ex.Id + " " + ex.Name + " " + ex.Date);
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            OperDB.CloseConnection(sqlConnection1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(label8.Visible)
                label8.Visible=false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text)
                && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {

                Exam newEx = new Exam();
                newEx.Id = Convert.ToInt16(textBox1.Text);
                newEx.Name = Convert.ToString(textBox2.Text);
                newEx.Date = Convert.ToDateTime(dateTimePicker1.Text);

                OperDB.AddEx(newEx, sqlConnection1);

                textBox1.Text = "";
                textBox2.Text = "";

                List<Exam> ExList = new List<Exam>();

                ExList=OperDB.CreateExList(sqlConnection1);

                listBox1.Items.Clear();
                foreach (Exam ex in ExList)
                    listBox1.Items.Add(ex.Id + " " + ex.Name + " " + ex.Date);
            }
            else
            {
                label8.Visible = true;
                label8.Text = "Все поля должны быть заполнены";
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if(label9.Visible)
                label9.Visible=false;

            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text)
                && !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                Exam updateExam = new Exam();

                updateExam.Id = Convert.ToInt16(textBox3.Text);
                updateExam.Name = Convert.ToString(textBox4.Text);
                updateExam.Date = Convert.ToDateTime(dateTimePicker2.Text);

               // OperDB.ConnectionWithDB();
                OperDB.UpdateEx(updateExam, sqlConnection1);
                textBox3.Text = "";
                textBox4.Text = "";
                OperDB.CreateExList(sqlConnection1);
                List<Exam> ExList = new List<Exam>();

                ExList = OperDB.CreateExList(sqlConnection1);

                listBox1.Items.Clear();
                foreach (Exam ex in ExList)
                    listBox1.Items.Add(ex.Id + " " + ex.Name + " " + ex.Date);

            }
            else
            {
                label9.Visible = true;
                label9.Text = "Все поля должны быть заполнены";
            }

        }


        private void button4_Click(object sender, EventArgs e)
        {

            if (label10.Visible)
                label10.Visible = false;

            if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
               // OperDB.ConnectionWithDB();
                OperDB.DellEx(textBox5.Text, sqlConnection1);
                textBox5.Text = "";
                OperDB.CreateExList(sqlConnection1);

                List<Exam> ExList = new List<Exam>();
                ExList = OperDB.CreateExList(sqlConnection1);

                listBox1.Items.Clear();
                foreach (Exam ex in ExList)
                    listBox1.Items.Add(ex.Id + " " + ex.Name + " " + ex.Date);
            }
            else
            {
                label10.Visible = true;
                label10.Text = "Все поля должны быть заполнены";
            }
        }
    }
}
