using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DB2
{
    public partial class Form2 : Form
    {
        private String connectionString = "";
        public Form2()
        {
            InitializeComponent();
        }

        private void go_Click(object sender, EventArgs e)
        {
            if (serverName.Text.Equals("") || userName.Text.Equals("") || dbName.Text.Equals("") || port.Text.Equals("") || password.Text.Equals(""))
            {
                MessageBox.Show("Input data, please");
            }
            else
            {
                setConnectionString();
                this.Close();
            }
            
        }

        private void setConnectionString() {
            connectionString = "server=" + serverName.Text +
               ";user=" + userName.Text +
               ";database=" + dbName.Text +
               ";port=" + port.Text +
               ";password=" + password.Text + ";";
        }
        public String getConnectionString()
        {
            return connectionString;
        }

        private void def_Click(object sender, EventArgs e)
        {
             serverName.Text = "localhost"; // Адрес сервера (для локальной базы пишите "localhost")
             userName.Text = "root"; // Имя пользователя
             dbName.Text = "sakila"; //Имя базы данных
             port.Text = "3306"; // Порт для подключения
             password.Text = "root"; // Пароль для подключения          
        }

      

        

       
    }
}
