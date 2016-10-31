using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;

namespace DB2
{
    public class ConnectionService
    {

       public MySqlConnection conn;
       private DataGridView dataGridOut;
       private BackupService backupService;
       public MySqlTransaction lastTr = null;
         public string lastExeption = "";

       

        public ConnectionService(ref DataGridView dataGrid)
        {
           Form2 connectForm = new Form2();
           connectForm.Show();
           while (connectForm.getConnectionString() == "")
           {}
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

            String[] queryList = query.Split(';');
            for (int i = 1; i < queryList.Length; i++)
            {
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, conn);
                DataSet DS = new DataSet();
                try
                {
                    mySqlDataAdapter.Fill(DS);
                    try
                    {
                        if(DS.Tables.Count>0) dataGridOut.DataSource = DS.Tables[0];
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

            }
            return true;

        }



        public bool doTransactionQuery(String query)
        {
            if (conn.Database == "")
            {
                lastExeption = "Connection Error";
                return false;
            }
            bool corectEnd = true;
            MySqlTransaction tr = null;

            String[] queryList = query.Split(';');

            try
            {
                conn.Open();

                tr = conn.BeginTransaction();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = tr;
                foreach (String curQuery in queryList)
                {
                    cmd.CommandText = curQuery;
                   if(curQuery!="") cmd.ExecuteNonQuery();


                   if (curQuery.Contains("SELECT") || curQuery.Contains("select"))
                   {
                       MySqlDataReader thisReader = cmd.ExecuteReader();
                       DataSet DS = new DataSet();
                       while (thisReader.Read())
                       {

                           DataTable dt = null;

                           dt = new DataTable();

                           dt.Load(thisReader);

                           dataGridOut.DataSource = dt;
                       }
                   }
                }

               

              //  tr.Commit();
                

            }
            catch (MySqlException ex)
            {
                try
                {
                  //  tr.Rollback();

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
                
               
                if (conn != null)
                {
                    conn.Close();
                }

                lastTr = tr;
                
                
            }
            return corectEnd;
        }

        public bool backup()
        {
            if (conn.Database == "")
            {
                lastExeption = "Connection Error";
                return false;
            }

           
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
            if (conn.Database == "")
            {
                lastExeption = "Connection Error";
                return false;
            }

            if (conn.Database == "") return false; 
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
