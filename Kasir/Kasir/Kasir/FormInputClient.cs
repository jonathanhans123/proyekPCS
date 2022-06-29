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
    public partial class FormInputClient : Form
    {
        FormAdmin admin;
        public FormInputClient(FormAdmin admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void FormInputClient_Load(object sender, EventArgs e)
        {
            loadgrid();
            button3.Enabled = false;
            button4.Enabled = false;
            radioButton1.Checked = true;
        }

        DataTable dtclient;
        private void loadgrid()
        {
            string query = "SELECT cl_id as 'ID',cl_username as 'Username',cl_password as 'Password',cl_position as 'Position' from client  ORDER BY 1";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dtclient = new DataTable();
            da.Fill(dtclient);
            dataGridView1.DataSource = dtclient.DefaultView;
            
        }
        int idx = -1;
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idx = e.RowIndex;

            textBox1.Text = dataGridView1.Rows[idx].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[idx].Cells[2].Value.ToString();
            label4.Text = dataGridView1.Rows[idx].Cells[0].Value.ToString();
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //add
            if (textBox1.Text!=""&& textBox2.Text != "")
            {
                string query = "select count(*) from client where cl_username = ?NAME and cl_password=?PASS";
                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.Add(new MySqlParameter("NAME", textBox1.Text));
                cmd.Parameters.Add(new MySqlParameter("PASS", textBox2.Text));
                Program.conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                Program.conn.Close();

                if (count == 0)
                {

                    query = "INSERT INTO client values (0,?NAME,?PASS,?POS);";
                    cmd = new MySqlCommand(query, Program.conn);
                    cmd.Parameters.Add(new MySqlParameter("NAME", textBox1.Text));
                    cmd.Parameters.Add(new MySqlParameter("PASS", textBox2.Text));
                    string pos = "";
                    if (radioButton1.Checked)
                    {
                        pos = "Admin";
                    }
                    else
                    {
                        pos = "Kasir";
                    }
                    cmd.Parameters.Add(new MySqlParameter("POS", pos));

                    Program.conn.Open();
                    cmd.ExecuteNonQuery();
                    Program.conn.Close();
                    MessageBox.Show("Berhasil Add");
                    clear();
                }
                else
                {
                    MessageBox.Show("Sudah ada user itu");
                }
            }
            else
            {
                MessageBox.Show("Input semua field");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                string query = "select count(*) from client where cl_username = ?NAME and cl_password=?PASS";
                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.Add(new MySqlParameter("NAME", textBox1.Text));
                cmd.Parameters.Add(new MySqlParameter("PASS", textBox2.Text));
                Program.conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                Program.conn.Close();

                if (count == 0)
                {

                    query = "UPDATE client set cl_username=?NAME,cl_password=?PASS,cl_position=?POS where cl_id =?ID;";
                    cmd = new MySqlCommand(query, Program.conn);
                    cmd.Parameters.Add(new MySqlParameter("NAME", textBox1.Text));
                    cmd.Parameters.Add(new MySqlParameter("PASS", textBox2.Text));
                    string pos = "";
                    if (radioButton1.Checked)
                    {
                        pos = "Admin";
                    }
                    else
                    {
                        pos = "Kasir";
                    }
                    cmd.Parameters.Add(new MySqlParameter("POS", pos));
                    cmd.Parameters.Add(new MySqlParameter("ID", label4.Text));

                    Program.conn.Open();
                    cmd.ExecuteNonQuery();
                    Program.conn.Close();
                    MessageBox.Show("Berhasil Edit");
                    clear();
                }
                else
                {
                    MessageBox.Show("Sudah ada user itu");
                }
            }
            else
            {
                MessageBox.Show("Input semua field");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM client where cl_id = " + label4.Text;
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);
            Program.conn.Open();
            cmd.ExecuteNonQuery();
            Program.conn.Close();
            MessageBox.Show("Berhasil Delete");
            clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear()
        {
            loadgrid();
            textBox1.Text = "";
            textBox2.Text = "";
            label4.Text = "-";
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin.Show();
            this.Close();
        }
    }
}
