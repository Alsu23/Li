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
        OperationsWithDB OperDB = new OperationsWithDB();
        SqlConnection sqlConnection1 = new SqlConnection();

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string rolest;
            Form2 f1 = new Form2();
            Form2 f2 = new Form2();
            Form3 f3 = new Form3();

            sqlConnection1 = OperDB.ConnectionWithDB();

        M:
            if (label3.Visible)
                label3.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text)
                && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                SqlCommand command = new SqlCommand("SELECT [User].[role] FROM [Autho] JOIN [User] ON [Autho].[user_id] = [User].[id] WHERE [User].[id] =  ( Select [Autho].[user_id] FROM [Autho] WHERE [Autho].[login]=@login AND [Autho].[password]=@password) ", sqlConnection1);
                command.Parameters.AddWithValue("login", textBox1.Text);
                command.Parameters.AddWithValue("password", textBox2.Text);

                command.ExecuteNonQuery();
                rolest = Convert.ToString(command.ExecuteScalar());

                textBox2.Text = "";

                if (rolest == "student")
                {
                    OperDB.CloseConnection(sqlConnection1);
                    Hide();
                    f1.Close();
                    f2.ShowDialog();
                }
                if (rolest == "teacher")
                {
                    OperDB.CloseConnection(sqlConnection1);
                    Hide();
                    f1.Close();
                    f3.ShowDialog();
                }
                if (rolest != "student" && rolest != "teacher")
                {
                    label3.Visible = true;
                    label3.Text = "Ошибка! Статус пользователя не определен";
                    goto M;
                }
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
            OperDB.CloseConnection(sqlConnection1);
        }
    }
}
