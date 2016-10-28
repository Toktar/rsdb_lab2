using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace DB2
{
    public class BackupService
    {

        private MySqlConnection conn;

        public BackupService(MySqlConnection newConn)
        {
            this.conn = newConn;
        }

       

        public bool backup(string file)
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
                        return true;
                    }
                }
            }
            
        }



        public bool restore(string file)
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
                        return true;
                    }
                }
            }

        }
    }
}
