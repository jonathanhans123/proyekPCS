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
    public partial class FormKasir : Form
    {
        public FormKasir()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FormInputBarang input = new FormInputBarang(this);
            input.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FormInputUser input = new FormInputUser(this);
            input.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FormTransaksi trans = new FormTransaksi(this);
            trans.Show();
            this.Hide();
        }

        private void FormKasir_Load(object sender, EventArgs e)
        {
            Program.setConn("localhost", "root", "proyekpcs");
            Program.conn.Close();
            loadgrid();
            loadcombo();
            dataGridView2.Columns[0].Width = 25;
            dataGridView2.Columns[2].Width = 25;
            dataGridView2.Columns[3].Width = 60;
        }
        DataTable dtitem;
        DataTable dtitemdesc;
        private void loadgrid()
        {
            string query = "SELECT it.`it_nama` AS 'Nama',CONCAT('Rp. ', FORMAT(it.it_price, 'c', 'id-ID')) AS 'Harga',it.it_stock as 'Stock' FROM item it,merk me, tipe ti WHERE it.`me_id`= me.`me_id` AND it.`ti_id` = ti.`ti_id` ORDER BY it.it_id";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dtitem = new DataTable();
            da.Fill(dtitem);
            dataGridView1.DataSource = dtitem.DefaultView;

            query = "SELECT me.me_name,ti.ti_name,it.it_size,it.it_id FROM item it,merk me, tipe ti WHERE it.`me_id`= me.`me_id` AND it.`ti_id` = ti.`ti_id` ORDER BY it.it_id";
            cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            da = new MySqlDataAdapter(cmd);
            dtitemdesc = new DataTable();
            da.Fill(dtitemdesc);
        }
        private void loadgrid(string merk, string tipe, string ukuran,int min,int max)
        {
            if (merk!="Semua")
            { merk = "'" + merk + "'";}
            else { merk = "me.me_name"; }
            if (ukuran != "Semua")
            { ukuran = "'" + ukuran + "'"; }
            else { ukuran = "it.it_size"; }
            if (tipe != "Semua")
            { tipe = "'" + tipe + "'"; }
            else { tipe = "ti.ti_name"; }
            string query = "SELECT it.`it_nama` AS 'Nama',CONCAT('Rp. ', FORMAT(it.it_price, 'c', 'id-ID')) AS 'Harga' FROM item it,merk me, tipe ti WHERE it.`me_id`= me.`me_id` AND it.`ti_id` = ti.`ti_id` AND me.me_name ="+merk+" AND ti.ti_name = "+tipe+" AND it.it_size = "+ukuran+" AND it.it_price >= "+min+" AND it.it_price <= "+max+" and it_status = 1 ORDER BY it.it_id";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dtitem = new DataTable();
            da.Fill(dtitem);
            dataGridView1.DataSource = dtitem.DefaultView;

            query = "SELECT me.me_name,ti.ti_name,it.it_size FROM item it,merk me, tipe ti WHERE it.`me_id`= me.`me_id` AND it.`ti_id` = ti.`ti_id` AND me.me_name =" + merk + " AND ti.ti_name = " + tipe + " AND it.it_size = " + ukuran + " AND it.it_price >= " + min + " AND it.it_price <= " + max + " and it_status = 1  ORDER BY it.it_id";
            cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            da = new MySqlDataAdapter(cmd);
            dtitemdesc = new DataTable();
            da.Fill(dtitemdesc);
        }
        DataTable dtmerk;
        DataTable dttipe;
        DataTable dtsize;
        DataTable dtmember;
        private void loadcombo()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox1.Items.Add("Semua");
            comboBox2.Items.Add("Semua");
            comboBox3.Items.Add("Semua");
            string query = "select me_name from merk order by 1";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dtmerk = new DataTable();
            da.Fill(dtmerk);
            for (int i = 0; i < dtmerk.Rows.Count; i++)
            {
                comboBox1.Items.Add(dtmerk.Rows[i][0].ToString());
            }

            query = "select ti_name from tipe order by 1";
            cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            da = new MySqlDataAdapter(cmd);
            dttipe = new DataTable();
            da.Fill(dttipe);
            for (int i = 0; i < dttipe.Rows.Count; i++)
            {
                comboBox2.Items.Add(dttipe.Rows[i][0].ToString());
            }

            query = "select distinct it_size from item order by 1";
            cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            da = new MySqlDataAdapter(cmd);
            dtsize = new DataTable();
            da.Fill(dtsize);
            for (int i = 0; i < dtsize.Rows.Count; i++)
            {
                comboBox3.Items.Add(dtsize.Rows[i][0].ToString());
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

            query = "select us.us_name from user us order by 1";
            cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            da = new MySqlDataAdapter(cmd);
            dtmember = new DataTable();
            da.Fill(dtmember);
            for (int i = 0; i < dtmember.Rows.Count; i++)
            {
                comboBox4.Items.Add(dtmember.Rows[i][0].ToString());
            }
        }
        DataTable dtdiskon;
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;

            labelNama.Text = dtitem.Rows[idx][0].ToString();
            labelMerk.Text = dtitemdesc.Rows[idx][0].ToString();
            labelTipe.Text = dtitemdesc.Rows[idx][1].ToString();
            labelUkuran.Text = dtitemdesc.Rows[idx][2].ToString();
            labelHarga.Text = dtitem.Rows[idx][1].ToString();
            int id = Convert.ToInt32(dtitemdesc.Rows[idx][3].ToString());

            string query = "SELECT it.`it_nama`,CONCAT('Diskon ',SUM(t.diskon),'%'),it.`it_id`,it.`it_price`          " +
                           "FROM item it,(                                                   " +
                           "SELECT it.it_id AS id,IFNULL(SUM(di.`di_value`), 0) AS diskon    " +
                           "FROM item it,merk me, tipe ti,discount di                        " +
                           "WHERE it.`me_id`= me.`me_id`                                     " +
                           "AND it.`ti_id`= ti.`ti_id`                                       " +
                           "AND it.`it_id`= " + id + "                                       " +
                           "AND di.`di_name`= it.`it_nama`                                   " +
                           "AND di.`di_category` = 'Name'                                    " +
                           "UNION                                                            " +
                           "SELECT it.it_id AS id,IFNULL(SUM(di.`di_value`), 0) AS diskon    " +
                           "FROM item it,merk me, tipe ti,discount di                        " +
                           "WHERE it.`me_id`= me.`me_id`                                     " +
                           "AND it.`ti_id`= ti.`ti_id`                                       " +
                           "AND it.`it_id`= " + id + "                                       " +
                           "AND di.`di_name`= me.`me_name`                                   " +
                           "AND di.`di_category` = 'Merk'                                    " +
                           "UNION                                                            " +
                           "SELECT it.it_id AS id,IFNULL(SUM(di.`di_value`), 0) AS diskon    " +
                           "FROM item it,merk me, tipe ti,discount di                        " +
                           "WHERE it.`me_id`= me.`me_id`                                     " +
                           "AND it.`ti_id`= ti.`ti_id`                                       " +
                           "AND it.`it_id`= " + id + "                                       " +
                           "AND ti.`ti_name`= di.`di_name`                                   " +
                           "AND di.`di_category` = 'Tipe'                                    " +
                           "UNION                                                            " +
                           "SELECT it.it_id AS id,IFNULL(SUM(di.`di_value`), 0) AS diskon    " +
                           "FROM item it,merk me, tipe ti,discount di                        " +
                           "WHERE it.`me_id`= me.`me_id`                                     " +
                           "AND it.`ti_id`= ti.`ti_id`                                       " +
                           "AND it.`it_id`= " + id + "                                       " +
                           "AND it.`it_size`= it.`it_size`                                   " +
                           "AND di.`di_category` = 'Size') t                                 " +
                           "WHERE t.id = it.`it_id`                                          " +
                           "UNION                                                            " +
                           "SELECT distinct it.`it_nama`,t.diskon,it.`it_id`,it.`it_price`                            " +
                           "FROM item it,(                                                   " +
                           "SELECT it.it_id AS id, di.`di_type` AS diskon                    " +
                           "FROM item it, merk me, tipe ti, discount di                      " +
                           "WHERE it.`me_id`= me.`me_id`                                     " +
                           "AND it.`ti_id`= ti.`ti_id`                                       " +
                           "AND it.`it_id`= " + id + "                                       " +
                           "AND di.`di_name`= it.`it_nama`                                   " +
                           "AND di.`di_category` = 'Name'                                    " +
                           "AND di.`di_type` != 'Diskon Percentage'                          " +
                           "UNION                                                            " +
                           "SELECT it.it_id AS id, di.`di_type` AS diskon                    " +
                           "FROM item it, merk me, tipe ti, discount di                      " +
                           "WHERE it.`me_id`= me.`me_id`                                     " +
                           "AND it.`ti_id`= ti.`ti_id`                                       " +
                           "AND it.`it_id`= " + id + "                                       " +
                           "AND di.`di_name`= me.`me_name`                                   " +
                           "AND di.`di_category` = 'Merk'                                    " +
                           "AND di.`di_type` != 'Diskon Percentage'                          " +
                           "UNION                                                            " +
                           "SELECT it.it_id AS id, di.`di_type` AS diskon                    " +
                           "FROM item it, merk me, tipe ti, discount di                      " +
                           "WHERE it.`me_id`= me.`me_id`                                     " +
                           "AND it.`ti_id`= ti.`ti_id`                                       " +
                           "AND it.`it_id`= " + id + "                                       " +
                           "AND ti.`ti_name`= di.`di_name`                                   " +
                           "AND di.`di_category` = 'Tipe'                                    " +
                           "AND di.`di_type` != 'Diskon Percentage'                          " +
                           "UNION                                                            " +
                           "SELECT it.it_id AS id, di.`di_type` AS diskon                    " +
                           "FROM item it, merk me, tipe ti, discount di                      " +
                           "WHERE it.`me_id`= me.`me_id`                                     " +
                           "AND it.`ti_id`= ti.`ti_id`                                       " +
                           "AND it.`it_id`=" + id + "                                        " +
                           "AND it.`it_size`=it.`it_size`                                    " +
                           "AND di.`di_category` = 'Size'                                    " +
                           "AND di.`di_type` != 'Diskon Percentage') t                       " +
                           "WHERE t.id = it.`it_id`;                                         ";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dtdiskon = new DataTable();
            da.Fill(dtdiskon);
            string diskon = "";
            for (int i = 0; i < dtdiskon.Rows.Count; i++)
            {
                if (i == 0)
                {
                    diskon = dtdiskon.Rows[i][1].ToString();
                }
                else
                {
                    diskon += ", " + dtdiskon.Rows[i][1].ToString();
                }
            }
            labelDiskon.Text = diskon;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            loadgrid(comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString(), Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown3.Value));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            loadgrid();
        }

        List<Item> items = new List<Item>();
        private void button6_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value != 0)
            {
                bool buy1get1 = false;
                bool buy2get3 = false;
                for (int i = 0; i < dtdiskon.Rows.Count; i++)
                {
                    if (dtdiskon.Rows[i][1].ToString() == "Buy 1 Get 1 Free")
                    {
                        buy1get1 = true;
                    }
                    if (dtdiskon.Rows[i][1].ToString() == "Buy 2 Get 3 Free")
                    {
                        buy1get1 = true;
                    }
                }
                Console.WriteLine(dtdiskon.Rows[0][3].ToString());

                int diskon = Convert.ToInt32(dtdiskon.Rows[0][1].ToString().Substring(7, dtdiskon.Rows[0][1].ToString().Length - 7).Substring(0, dtdiskon.Rows[0][1].ToString().Substring(7, dtdiskon.Rows[0][1].ToString().Length - 7).Length - 1));
                items.Add(new Item(Convert.ToInt32(dtdiskon.Rows[0][2].ToString()), dtdiskon.Rows[0][0].ToString(), diskon, buy1get1, buy2get3, Convert.ToInt32(dtdiskon.Rows[0][3].ToString()), Convert.ToInt32(numericUpDown1.Value)));
                dataGridView2.Rows.Add(dataGridView2.Rows.Count + 1, items[items.Count - 1].nama, items[items.Count - 1].jumlah, items[items.Count - 1].harga * items[items.Count - 1].jumlah * (100 - items[items.Count - 1].diskon) / 100);
                
                int harga = 0;
                int count = 0;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    harga += Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value.ToString());
                    count += Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value.ToString());
                }
                label1.Text = "Total Pesanan : " + count;
                label2.Text = "Total Harga : Rp. " + harga;
                labelNama.Text = "-";
                labelMerk.Text = "-";
                labelTipe.Text = "-";
                labelUkuran.Text = "-";
                labelHarga.Text ="-";
                labelDiskon.Text = "-";
                numericUpDown1.Value = 0;
            }
            else
            {
                MessageBox.Show("Jumlah harus lebih dari 0");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (transaksiidx == -1)
            {
                MessageBox.Show("Select item first");
            }
            else
            {
                dataGridView2.Rows.RemoveAt(transaksiidx);
            }
            int harga = 0;
            int count = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                harga += Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value.ToString());
                count += Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value.ToString());
            }

            label1.Text = "Total Pesanan : " + count;
            label2.Text = "Total Harga : Rp. " + harga;
        }
        int transaksiidx = -1;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            transaksiidx = e.RowIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            int harga = 0;
            int count = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                harga += Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value.ToString());
                count += Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value.ToString());
            }

            label1.Text = "Total Pesanan : " + count;
            label2.Text = "Total Harga : Rp. " + harga;
        }
    }
}
