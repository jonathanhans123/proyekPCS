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
    public partial class FormListDiscount : Form
    {
        FormInputBarang barang;
        FormKasir kasir;
        public FormListDiscount(FormInputBarang barang)
        {
            InitializeComponent();
            this.barang = barang;
        }
        public FormListDiscount(FormKasir kasir)
        {
            InitializeComponent();
            this.kasir = kasir;
        }

        private void FormListDiscount_Load(object sender, EventArgs e)
        {
            loadgrid();
            loadcombo();
            button3.Enabled = false;
        }
        private void loadcombo()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Diskon Percentage");
            comboBox1.Items.Add("Buy 1 Get 1 Free");
            comboBox1.Items.Add("Buy 2 Get 3 Free");
            comboBox1.Items.Add("Diskon Disabled");
        }
        DataTable dtdiskon;
        private void loadgrid()
        {
            string query = "SELECT di.di_id as 'ID',di.di_name as 'Name',di.di_type as 'Type',di.di_value as 'Value' FROM discount di where di.di_status = 1 order by 1";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dtdiskon = new DataTable();
            da.Fill(dtdiskon);
            dataGridView1.DataSource = dtdiskon.DefaultView;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (barang == null)
            {
                kasir.Show();
            }
            else
            {
                barang.Show();
            }
            this.Close();
        }

        int idx = -1;
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idx = e.RowIndex;

            label3.Text = dataGridView1.Rows[idx].Cells[0].Value.ToString();
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "UPDATE discount SET status=0 where di_id=?DI_ID;";

            MySqlCommand cmd = new MySqlCommand(query, Program.conn);
            cmd.Parameters.Add(new MySqlParameter("DI_ID", dataGridView1.Rows[idx].Cells[0].Value.ToString()));
            Program.conn.Open();
            cmd.ExecuteNonQuery();
            Program.conn.Close();
            MessageBox.Show("Berhasil Delete");
            loadgrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query;
            if (comboBox1.SelectedItem.ToString() == "Diskon Disabled")
            {
                query = "SELECT di.di_id as 'ID',di.di_name as 'Name',di.di_type as 'Type',di.di_value as 'Value' FROM discount di where di.di_status = 1 and di.di_status = 0 order by 1";

            }
            else
            {
                query = "SELECT di.di_id as 'ID',di.di_name as 'Name',di.di_type as 'Type',di.di_value as 'Value' FROM discount di where di.di_status = 1 and di.di_type = '" + comboBox1.SelectedItem.ToString() + "' order by 1";
            }
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dtdiskon = new DataTable();
            da.Fill(dtdiskon);
            dataGridView1.DataSource = dtdiskon.DefaultView;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadgrid();
        }
    }
}
