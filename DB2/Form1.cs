using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DB2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection conn;

        private void Form1_Load(object sender, EventArgs e)
        {
            string serverName = "localhost"; // Адрес сервера (для локальной базы пишите "localhost")
            string userName = "root"; // Имя пользователя
            string dbName = "sakila"; //Имя базы данных
            string port = "3306"; // Порт для подключения
            string password = "root"; // Пароль для подключения
            string connStr = "server=" + serverName +
                ";user=" + userName +
                ";database=" + dbName +
                ";port=" + port +
                ";password=" + password + ";";
            conn = new MySqlConnection(connStr);

            dataGridView1.DataError +=
        new DataGridViewDataErrorEventHandler(dataGridView1_DataError);




        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            conn.Open();

            string sql = "SELECT * FROM actor"; // Строка запроса
            sql = textBox1.Text;
            if (transactionCheck.Checked)
            {
                doTransactionQuery(sql);
            }
            else
            {
                doQuery(sql);
            }

            conn.Close();
        }

        private void doQuery(String query)
        {

            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, conn);
            DataSet DS = new DataSet();
            try
            {
                mySqlDataAdapter.Fill(DS);

               // dataGridView1 = startGridView;
                

                try
                {
                    dataGridView1.DataSource = DS.Tables[0];
                }
                catch (DataException dataExp)
                {

                }
                this.dataGridView1.AutoResizeColumns(
        DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                
            }
            catch (MySqlException exp)
            {
                richTextBox1.Text = exp.Message;
            }

        }
        private void dataGridView1_DataError(object sender,
     DataGridViewDataErrorEventArgs e)
        {
            // If the data source raises an exception when a cell value is 
            // commited, display an error message.
            if (e.Exception != null &&
                e.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Error of Grid");
            }
        }
        private void doTransactionQuery(String query)
        {
            MySqlTransaction tr = null;

            String [] queryList = query.Split('\n');

            try
            {
                
                tr = conn.BeginTransaction();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = tr;
                foreach(String curQuery in queryList) {
                    cmd.CommandText = curQuery;
                    cmd.ExecuteNonQuery();
                }
              

                tr.Commit();
                richTextBox1.Text = "OK";

            }
            catch (MySqlException ex)
            {
                try
                {
                    tr.Rollback();

                }
                catch (MySqlException ex1)
                {
                     richTextBox1.Text =  ("Error: {0}" +  ex1.ToString());
                }

                richTextBox1.Text = ("Error: {0}" + ex.ToString());

            }
            finally
            {

                if (queryList.Length == 1)
                {
                    doQuery(queryList[0]);
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void backup(string file)
        {
           
            
            using (conn)
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(file);
                        conn.Close();
                    }
                }
            }
            MessageBox.Show("OK! The process is ended");
        }

        private void restore(string file)
        {

           
            using (conn)
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ImportFromFile(file);
                        conn.Close();
                    }
                }
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string file = "";
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.FileName = "backup.sql";
            saveFileDialog1.ShowDialog();
            file = saveFileDialog1.FileName;
            if (file == "")
            {
                MessageBox.Show("Error: Search other folder");
                return;
            }
            backup(file);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string file = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "Текстовые файлы(*.sql)|*.sql" };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                file = openFileDialog1.FileName;

            if (file == "")
            {
                MessageBox.Show("Error: Search other file");
                return;
            }

            restore(file);
        }
    }
}
