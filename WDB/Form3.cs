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
        SqlConnection sqlConnection;

        public Form3()
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


        private void Form3_Load(object sender, EventArgs e)
        {
            string startupPath = System.IO.Path.GetFullPath(".\\");
            string NameDir = startupPath.Substring(0, startupPath.Length - 10);
            //MessageBox.Show(NameDir);

            //string connection = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\1\Desktop\DB\WDB\WDB\Database1.mdf;Integrated Security=True;User Instance=True";
            string connection = @"Data Source=.\SQLEXPRESS;AttachDbFilename=" + NameDir + "Database1.mdf;Integrated Security=True;User Instance=True";
            
            sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Exam]", sqlConnection);

          //  try
           // {
                sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id"]) + "   " + Convert.ToString(sqlReader["name"]) + "   " + Convert.ToString(sqlReader["date"]));
                }
          //  }
          //  catch
          //  {
           // }
           // finally
           // {
                if (sqlReader != null)
                    sqlReader.Close();
           // }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(label8.Visible)
                label8.Visible=false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text)
                && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                SqlCommand commandIn = new SqlCommand("INSERT INTO [Exam] (ID, name, date) VALUES(@ID, @name, @date)", sqlConnection);
                commandIn.Parameters.AddWithValue("ID", Convert.ToInt16(textBox1.Text));
                commandIn.Parameters.AddWithValue("name", textBox2.Text);
                commandIn.Parameters.AddWithValue("date", Convert.ToDateTime(dateTimePicker1.Text));
                commandIn.ExecuteNonQuery();

                textBox1.Text = "";
                textBox2.Text = "";

                listBox1.Items.Clear();
                SqlDataReader sqlReader = null;

                SqlCommand command = new SqlCommand("SELECT * FROM [Exam]", sqlConnection);

             //   try
              //  {
                    sqlReader = command.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        listBox1.Items.Add(Convert.ToString(sqlReader["id"]) + "   " + Convert.ToString(sqlReader["name"]) + "   " + Convert.ToDateTime(sqlReader["date"]));
                    }
              //  }
              //  catch
              //  {
              //  }
               // finally
               // {
                    if (sqlReader != null)
                        sqlReader.Close();
              //  }
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
                SqlCommand commandUp = new SqlCommand("UPDATE [Exam] SET [name]=@name, [date]=@date WHERE [ID]=@ID", sqlConnection);
                commandUp.Parameters.AddWithValue("ID", Convert.ToInt16(textBox3.Text));
                commandUp.Parameters.AddWithValue("name", textBox4.Text);
                commandUp.Parameters.AddWithValue("date", Convert.ToDateTime(dateTimePicker2.Text));
                commandUp.ExecuteNonQuery();

                textBox3.Text = "";
                textBox4.Text = "";

                listBox1.Items.Clear();
                SqlDataReader sqlReader = null;

                SqlCommand command = new SqlCommand("SELECT * FROM [Exam]", sqlConnection);

               // try
               // {
                    sqlReader = command.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        listBox1.Items.Add(Convert.ToString(sqlReader["id"]) + "   " + Convert.ToString(sqlReader["name"]) + "   " + Convert.ToString(sqlReader["date"]));
                    }
              //  }
              //  catch
              //  {
              //  }
              //  finally
               // {
                    if (sqlReader != null)
                        sqlReader.Close();
               // }

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
                SqlCommand commandDel = new SqlCommand("DELETE FROM [Exam] WHERE [ID]=@ID", sqlConnection);
                commandDel.Parameters.AddWithValue("ID", textBox5.Text);
                commandDel.ExecuteNonQuery();
                textBox5.Text = "";
                listBox1.Items.Clear();
                SqlDataReader sqlReader = null;

                SqlCommand command = new SqlCommand("SELECT * FROM [Exam]", sqlConnection);

                //try
              //  {
                    sqlReader = command.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        listBox1.Items.Add(Convert.ToString(sqlReader["id"]) + "   " + Convert.ToString(sqlReader["name"]) + "   " + Convert.ToString(sqlReader["date"]));
                    }
              // }
              //  catch
              //  {
              //  }
             //   finally
              //  {
                    if (sqlReader != null)
                        sqlReader.Close();
              //  }

            }
            else
            {
                label10.Visible = true;
                label10.Text = "Все поля должны быть заполнены";
            }
        }
    }
}
