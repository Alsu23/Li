using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace WDB
{
   partial class OperationsWithDB
    {
        public SqlConnection ConnectionWithDB()
        {
            string startupPath = System.IO.Path.GetFullPath(".\\");
            string NameDir = startupPath.Substring(0, startupPath.Length - 10);
            string connection = @"Data Source=.\SQLEXPRESS;AttachDbFilename=" + NameDir + "Database1.mdf;Integrated Security=True;User Instance=True";

            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();
            return sqlConnection;
        }

        public void CloseConnection(SqlConnection sqlConnection)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        public void UpdateEx(Exam updateExam, SqlConnection sqlConnection)
        {
            SqlCommand commandUp = new SqlCommand("UPDATE [Exam] SET [name]=@name, [date]=@date WHERE [ID]=@ID", sqlConnection);
            commandUp.Parameters.AddWithValue("ID", updateExam.Id);
            commandUp.Parameters.AddWithValue("name", updateExam.Name);
            commandUp.Parameters.AddWithValue("date", updateExam.Date);
            commandUp.ExecuteNonQuery();
        }
        public List<Exam> CreateExList(SqlConnection sqlConnection)
        {

            SqlCommand command = new SqlCommand("SELECT * FROM [Exam]", sqlConnection);

            List<Exam> ExList = new List<Exam>();

            SqlDataReader sqlReader = null;
            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
                ExList.Add(new Exam() { Id = Convert.ToInt16(sqlReader["id"]), Name = Convert.ToString(sqlReader["name"]), Date = Convert.ToDateTime(sqlReader["date"]) });
            sqlReader.Close();

            return ExList;
        }

        public void AddEx(Exam newEx1, SqlConnection sqlConnection)
        {
            SqlCommand commandIn = new SqlCommand("INSERT INTO [Exam] (ID, name, date) VALUES(@ID, @name, @date)", sqlConnection);
            commandIn.Parameters.AddWithValue("id", newEx1.Id);
            commandIn.Parameters.AddWithValue("name", newEx1.Name);
            commandIn.Parameters.AddWithValue("date", newEx1.Date);
            commandIn.ExecuteNonQuery();
        }

        public void DellEx(string id, SqlConnection sqlConnection)
        {
            SqlCommand commandDel = new SqlCommand("DELETE FROM [Exam] WHERE [ID]=@ID", sqlConnection);
            commandDel.Parameters.AddWithValue("ID", id);
            commandDel.ExecuteNonQuery();
        }
    }
}
