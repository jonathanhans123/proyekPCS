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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            Program.setConn("localhost", "root", "proyekpcs");
            Program.conn.Close();
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                MySqlCommand cmd = new MySqlCommand("select * from client cl where cl.cl_username =?USERNAME and cl.cl_password =?PASSWORD",Program.conn);
                cmd.Parameters.AddWithValue("USERNAME", textBox1.Text);
                cmd.Parameters.AddWithValue("PASSWORD", textBox2.Text);

                Program.conn.Open();
                cmd.ExecuteReader();
                Program.conn.Close();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dtclient = new DataTable();
                da.Fill(dtclient);

                if (dtclient.Rows.Count > 0)
                {
                    if (dtclient.Rows[0][3].ToString() == "Admin")
                    {
                        FormAdmin admin = new FormAdmin(this);
                        admin.Show();
                        this.Hide();
                    }
                    else if (dtclient.Rows[0][3].ToString() == "Kasir")
                    {
                        FormKasir kasir = new FormKasir(this);
                        kasir.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Tidak ada user itu");
                }
            }
            else
            {
                MessageBox.Show("Isi semua field");
            }
        }
    }
}
