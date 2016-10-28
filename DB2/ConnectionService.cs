using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace DB2
{
    public class ConnectionService
    {

       private MySqlConnection conn;
       private DataGridView dataGridOut;
       private BackupService backupService;
         public string lastExeption = "";

       

        public ConnectionService(ref DataGridView dataGrid)
        {
           Form2 connectForm = new Form2();
           connectForm.Show();
           init(connectForm.getConnectionString(), ref dataGrid);

        }
        private void init(string connStr, ref DataGridView dataGrid)
        {

            conn = new MySqlConnection(connStr);
            backupService = new BackupService(conn);
            dataGridOut = dataGrid;
        }

        public bool doQuery(String query)
        {
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, conn);
            DataSet DS = new DataSet();
            try
            {
                mySqlDataAdapter.Fill(DS);
                try
                {
                    dataGridOut.DataSource = DS.Tables[0];
                }
                catch (DataException dataExp)
                {
                    lastExeption = dataExp.Message;
                    return false;
                }
                dataGridOut.AutoResizeColumns(
        DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);


            }
            catch (MySqlException exp)
            {
                lastExeption = exp.Message;
                return false;
            }
            return true;

        }



        public bool doTransactionQuery(String query)
        {
            bool corectEnd = true;
            MySqlTransaction tr = null;

            String[] queryList = query.Split('\n');

            try
            {

                tr = conn.BeginTransaction();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = tr;
                foreach (String curQuery in queryList)
                {
                    cmd.CommandText = curQuery;
                    cmd.ExecuteNonQuery();
                }


                tr.Commit();
                

            }
            catch (MySqlException ex)
            {
                try
                {
                    tr.Rollback();

                }
                catch (MySqlException ex1)
                {
                    lastExeption = ("Error: {0}" + ex1.ToString());
                    
                }

                lastExeption += ("Error: {0}" + ex.ToString());
                corectEnd = false;

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
            return corectEnd;
        }

        public bool backup()
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
                return false;
            }
            return backupService.backup(file);

            
        }



        public bool restore()
        {
            string file = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "Текстовые файлы(*.sql)|*.sql" };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                file = openFileDialog1.FileName;

            if (file == "")
            {
                MessageBox.Show("Error: Search other file");
                return false;
            }
            return backupService.restore(file);
        }
    }
}
