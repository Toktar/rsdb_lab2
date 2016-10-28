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
            string sql = textBox1.Text;
            bool correctEnd = transactionCheck.Checked ? connectionService.doTransactionQuery(sql) : correctEnd = connectionService.doQuery(sql);
            richTextBox1.Text = correctEnd?"OK! The process is ended":connectionService.lastExeption;  
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
           
            connectionService.backup();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           

            connectionService.restore();
        }

        private void RollbackButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
