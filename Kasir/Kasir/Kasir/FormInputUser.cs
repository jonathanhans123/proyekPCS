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
    public partial class FormInputUser : Form
    {
        FormAdmin admin;
        public FormInputUser(FormAdmin admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin.Show();
            this.Close();
        }

        private void FormInputUser_Load(object sender, EventArgs e)
        {
            clear();
        }

        DataTable dtuser;
        private void loadgrid()
        {
            string query = "SELECT us.`us_id` AS 'ID',us.`us_name` as 'Name',us.`us_username` AS 'Username',us.`us_email`AS 'Email',us.`us_phone` AS 'Phone',us.`us_rank` AS 'Rank' FROM USER us order by 1";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dtuser = new DataTable();
            da.Fill(dtuser);
            dataGridView1.DataSource = dtuser.DefaultView;
        }
        int idx = -1;
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idx = e.RowIndex;

            textBox1.Text = dataGridView1.Rows[idx].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[idx].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[idx].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[idx].Cells[4].Value.ToString();
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
        }

        private void clear()
        {
            loadgrid();
            idx = -1;
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (idx != -1 && textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox4.Text != "")
            {
                string query = "UPDATE USER SET us_name = @NAME,us_username = @USERNAME,us_email = @email,us_phone =@PHONE WHERE us_id =@ID";
                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.Add(new MySqlParameter("ID", dataGridView1.Rows[idx].Cells[0].Value.ToString()));
                cmd.Parameters.Add(new MySqlParameter("NAME", textBox1.Text));
                cmd.Parameters.Add(new MySqlParameter("USERNAME", textBox2.Text));
                cmd.Parameters.Add(new MySqlParameter("EMAIL", textBox4.Text));
                cmd.Parameters.Add(new MySqlParameter("PHONE", textBox5.Text));

                Program.conn.Open();
                cmd.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Berhasil Edit");
                clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (idx != -1)
            {
                string query = "DELETE FROM USER WHERE us_id =@ID";
                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.Add(new MySqlParameter("ID", dataGridView1.Rows[idx].Cells[0].Value.ToString()));

                Program.conn.Open();
                cmd.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Berhasil Delete");
                clear();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (idx != -1 && textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox4.Text != "")
            {
                string rank = "";
                if (dataGridView1.Rows[idx].Cells[5].Value.ToString() == "Bronze")
                {
                    rank = "Silver";
                }else 
                {
                    rank = "Gold";
                }
                string query = "UPDATE USER SET us_rank = @RANK WHERE us_id =@ID";
                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.Add(new MySqlParameter("ID", dataGridView1.Rows[idx].Cells[0].Value.ToString()));
                cmd.Parameters.Add(new MySqlParameter("RANK", rank));

                Program.conn.Open();
                cmd.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Berhasil Upgrade");
                clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!=""&& textBox2.Text != "" && textBox5.Text != "" && textBox4.Text != "")
            {
                string query = "INSERT INTO user VALUES(0,@NAME,@USERNAME,@EMAIL,@PHONE,'Bronze')";
                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.Add(new MySqlParameter("NAME", textBox1.Text));
                cmd.Parameters.Add(new MySqlParameter("USERNAME", textBox2.Text));
                cmd.Parameters.Add(new MySqlParameter("EMAIL", textBox4.Text));
                cmd.Parameters.Add(new MySqlParameter("PHONE", textBox5.Text));

                Program.conn.Open();
                cmd.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Berhasil Add");
                clear();
            }
        }
    }
}
