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
        OperationsWithDB OperDB = new OperationsWithDB();
        SqlConnection sqlConnection1 = new SqlConnection();

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 f1 = new Form1();
            OperDB.CloseConnection(sqlConnection1);
            f1.ShowDialog();
            Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            OperDB.CloseConnection(sqlConnection1);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DateTime dateToday = new DateTime();
            dateToday=DateTime.Today;

            sqlConnection1 = OperDB.ConnectionWithDB();

            List<Exam> ExList = new List<Exam>();

            ExList = OperDB.CreateExList(sqlConnection1);

            foreach (Exam ex in ExList)
            {
                TimeSpan difference = ex.Date - dateToday;
                int days = (int)Math.Ceiling(difference.TotalDays);

                if(ex.Date==dateToday)
                    listBox1.Items.Add(ex.Id + " " + ex.Name + " " + ex.Date);
                if(days <= 7)
                    listBox2.Items.Add(ex.Id + " " + ex.Name + " " + ex.Date);
                if (days <= 30)
                    listBox3.Items.Add(ex.Id + " " + ex.Name + " " + ex.Date);
            }

        }
    }
}
