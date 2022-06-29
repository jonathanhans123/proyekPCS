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
    public partial class FormInputBarangKasir : Form
    {
        FormKasir kasir;
        public FormInputBarangKasir(FormKasir kasir)
        {
            InitializeComponent();
            this.kasir = kasir;
        }

        private void FormInputBarangKasir_Load(object sender, EventArgs e)
        {
            loadgrid();
            loadcombo();
            button2.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            textBox1.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            kasir.Show();
            this.Close();
        }
        DataTable dtitem;
        private void loadgrid()
        {
            string query = "SELECT it.it_id as 'ID',it.`it_nama` AS 'Nama',it.`it_size` AS 'Size',me.`me_name` AS 'Merk',ti.`ti_name` AS 'Tipe',it.it_price AS 'Harga', it.it_stock as 'Stock' FROM item it,merk me,tipe ti WHERE it.`me_id`= me.`me_id` AND it.`ti_id` = ti.`ti_id` and it_status = 1 order by 1";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dtitem = new DataTable();
            da.Fill(dtitem);
            dataGridView1.DataSource = dtitem.DefaultView;
        }
        DataTable dtmerk;
        DataTable dttipe;
        private void loadcombo()
        {

            string query = "select me_id,me_name from merk order by 1";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dtmerk = new DataTable();
            da.Fill(dtmerk);
            comboBox1.DisplayMember = "me_name";
            comboBox1.ValueMember = "me_id";
            comboBox1.DataSource = dtmerk.DefaultView;

            query = "select ti_id,ti_name from tipe order by 1";
            cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            da = new MySqlDataAdapter(cmd);
            dttipe = new DataTable();
            da.Fill(dttipe);
            comboBox2.DisplayMember = "ti_name";
            comboBox2.ValueMember = "ti_id";
            comboBox2.DataSource = dttipe.DefaultView;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;

            textBox1.Text = dataGridView1.Rows[idx].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[idx].Cells[1].Value.ToString();
            numericUpDown2.Value = Convert.ToInt32(dataGridView1.Rows[idx].Cells[2].Value.ToString());
            comboBox1.SelectedItem = dataGridView1.Rows[idx].Cells[3].Value.ToString();
            comboBox2.SelectedItem = dataGridView1.Rows[idx].Cells[4].Value.ToString();
            numericUpDown1.Value = Convert.ToInt32(dataGridView1.Rows[idx].Cells[5].Value.ToString());
            numericUpDown3.Value = Convert.ToInt32(dataGridView1.Rows[idx].Cells[6].Value.ToString());
            if (Convert.ToInt32(dataGridView1.Rows[idx].Cells[6].Value.ToString()) > 0)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            button2.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormInputBarangKasir_Load(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //add
            if (textBox2.Text != "" && numericUpDown1.Value != 0 && numericUpDown2.Value != 0 && numericUpDown3.Value != 0)
            {
                string query = "INSERT INTO item VALUES (0,?NAME,?STOCK,?PRICE,?SIZE,?ME_ID,?TI_ID,1);";

                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.Add(new MySqlParameter("NAME", textBox2.Text));
                cmd.Parameters.Add(new MySqlParameter("STOCK", numericUpDown3.Value));
                cmd.Parameters.Add(new MySqlParameter("PRICE", numericUpDown1.Value));
                cmd.Parameters.Add(new MySqlParameter("SIZE", numericUpDown2.Value));
                cmd.Parameters.Add(new MySqlParameter("ME_ID", comboBox1.SelectedValue));
                cmd.Parameters.Add(new MySqlParameter("TI_ID", comboBox2.SelectedValue));

                Program.conn.Open();
                cmd.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Berhasil Add");
                FormInputBarangKasir_Load(null, null);
            }
            else
            {
                MessageBox.Show("Isi semua field!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //edit
            if (textBox2.Text != "" && numericUpDown1.Value != 0 && numericUpDown2.Value != 0 && numericUpDown3.Value != 0)
            {
                string query = "UPDATE item SET it_stock=?STOCK where it_id=?IT_ID;";

                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.Add(new MySqlParameter("STOCK", numericUpDown3.Value));
                cmd.Parameters.Add(new MySqlParameter("IT_ID", textBox1.Text));

                Program.conn.Open();
                cmd.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Berhasil Edit Stock");
                FormInputBarangKasir_Load(null, null);
            }
            else
            {
                MessageBox.Show("Isi semua field!");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //hapus
            string query = "UPDATE item set it_status=0 where it_id=?IT_ID";

            MySqlCommand cmd = new MySqlCommand(query, Program.conn);
            cmd.Parameters.Add(new MySqlParameter("IT_ID", textBox1.Text));

            Program.conn.Open();
            cmd.ExecuteNonQuery();
            Program.conn.Close();
            MessageBox.Show("Berhasil Hapus");
            FormInputBarangKasir_Load(null, null);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown3.Value > 0)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
