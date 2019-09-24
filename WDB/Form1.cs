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
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string rolest;
            //int k=2;
           // Hide();
            Form2 f2 = new Form2();
            Form3 f3 = new Form3();

            string startupPath = System.IO.Path.GetFullPath(".\\");
            string NameDir = startupPath.Substring(0, startupPath.Length - 10);
            MessageBox.Show(NameDir);

            //string connection = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\1\Desktop\DB\WDB\WDB\Database1.mdf;Integrated Security=True;User Instance=True";
            string connection = @"Data Source=.\SQLEXPRESS;AttachDbFilename="+NameDir+"Database1.mdf;Integrated Security=True;User Instance=True";
            sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            M:
            if (label3.Visible)
                label3.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text)
                && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                SqlCommand command = new SqlCommand("SELECT [User].[role] FROM [Autho] JOIN [User] ON [Autho].[user_id] = [User].[id] WHERE [User].[id] =  ( Select [Autho].[user_id] FROM [Autho] WHERE [Autho].[login]=@login AND [Autho].[password]=@password) ", sqlConnection);
                command.Parameters.AddWithValue("login", textBox1.Text);
                command.Parameters.AddWithValue("password", textBox2.Text);

                command.ExecuteNonQuery();
                rolest = Convert.ToString(command.ExecuteScalar());

                textBox2.Text = "";

                if (rolest == "student")
                    f2.ShowDialog();
                if (rolest == "teacher")
                    f3.ShowDialog();
                if (rolest == "")
                    goto M;

                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
                Hide();
                Close();
            }
            else
            {
                label3.Visible = true;
                label3.Text = "Введен неверный логин или пароль";
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
    }
}
