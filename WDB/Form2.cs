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
    public partial class Form2 : Form
    {
        SqlConnection sqlConnection;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();

            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();

            Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DateTime dateToday = new DateTime();
            dateToday=DateTime.Today;

            string startupPath = System.IO.Path.GetFullPath(".\\");
            string NameDir = startupPath.Substring(0, startupPath.Length - 10);
            //MessageBox.Show(NameDir);
            //string connection = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\1\Desktop\DB\WDB\WDB\Database1.mdf;Integrated Security=True;User Instance=True";
            string connection = @"Data Source=.\SQLEXPRESS;AttachDbFilename=" + NameDir + "Database1.mdf;Integrated Security=True;User Instance=True";
            
            sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            SqlDataReader sqlReader1 = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Exam]", sqlConnection);

            //try
            //{
                sqlReader1 = command.ExecuteReader();
                while (sqlReader1.Read())
                {
                    TimeSpan difference = Convert.ToDateTime(sqlReader1["date"]) - dateToday;
                    int days = (int)Math.Ceiling(difference.TotalDays);

                    if (Convert.ToDateTime(sqlReader1["date"])==dateToday)
                        listBox1.Items.Add(Convert.ToString(sqlReader1["id"]) + "   " + Convert.ToString(sqlReader1["name"]) + "   " + Convert.ToString(sqlReader1["date"]));
                    if (days<=7)
                        listBox2.Items.Add(Convert.ToString(sqlReader1["id"]) + "   " + Convert.ToString(sqlReader1["name"]) + "   " + Convert.ToString(sqlReader1["date"]));
                    if (days <= 30)
                        listBox3.Items.Add(Convert.ToString(sqlReader1["id"]) + "   " + Convert.ToString(sqlReader1["name"]) + "   " + Convert.ToString(sqlReader1["date"]));
                }
           // }
            //catch
            //{
            //}
            //finally
            //{
                if (sqlReader1 != null)
                    sqlReader1.Close();
            //}
        }
    }
}
