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
       
        ConnectionService connectionService;

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            connectionService = new ConnectionService(ref dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            string sql = textBox1.Text;

            if(transactionCheck.Checked) {
               // connectionService.doQuery("START TRANSACTION;");
                richTextBox1.Text = connectionService.doTransactionQuery(sql) ? "OK! The transaction is ended" : connectionService.lastExeption;
                //richTextBox1.Text = connectionService.doQuery(sql) ? "OK! The transaction is ended" : connectionService.lastExeption;  
      
            } else {
                
                richTextBox1.Text = connectionService.doQuery(sql)? "OK! The query is done" : connectionService.lastExeption;  
      
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
        
     


        private void button3_Click(object sender, EventArgs e)
        {
            if (false == connectionService.backup()) MessageBox.Show(connectionService.lastExeption);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           

           if(false == connectionService.restore()) MessageBox.Show(connectionService.lastExeption);
        }

        private void RollbackButton_Click(object sender, EventArgs e)
        {
           // textBox1.Text = "";
            connectionService.conn.Open();
            connectionService.lastTr.Rollback();

            connectionService.conn.Close();
          //  connectionService.doQuery("ROLLBACK;");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connectionService = new ConnectionService(ref dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
           // connectionService.doQuery("COMMIT;");
            connectionService.conn.Open();
            connectionService.lastTr.Commit();
            connectionService.conn.Close();

        }
    }
}
