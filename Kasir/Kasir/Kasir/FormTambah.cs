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
    public partial class FormTambah : Form
    {
        FormInputBarang barang;
        string add;
        public FormTambah(FormInputBarang barang,string add)
        {
            InitializeComponent();
            if (add == "merk")
            {
                label1.Text = "Add Merk";
            }
            else
            {
                label1.Text = "Add Tipe";
            }
            this.barang = barang;
            this.add = add;
        }

        private void FormTambah_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //tambah
            if (add == "merk")
            {
                string query = "INSERT INTO merk (0,?NAME)";

                MySqlCommand cmd = new MySqlCommand(query,Program.conn);
                cmd.Parameters.Add(new MySqlParameter("NAME", textBox1.Text));

                Program.conn.Open();
                cmd.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Berhasil Add");
            }
            else
            {
                string query = "INSERT INTO tipe (0,?NAME)";

                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.Add(new MySqlParameter("NAME", textBox1.Text));

                Program.conn.Open();
                cmd.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Berhasil Add");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //cancel
            this.Close();
        }
    }
}
