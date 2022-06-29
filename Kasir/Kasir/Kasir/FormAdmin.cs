using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Kasir
{
    public partial class FormAdmin : Form
    {
        FormLogin login;
        public FormAdmin(FormLogin login)
        {
            InitializeComponent();
            this.login = login;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormInputBarang barang = new FormInputBarang(this);
            this.Hide();
            barang.Show();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            FormInputUser user = new FormInputUser(this);
            this.Hide();
            user.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormTransaksi trans = new FormTransaksi(this);
            this.Hide();
            trans.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            login.Show();
            this.Hide();
        }
    }
}
