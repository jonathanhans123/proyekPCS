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
        FormLogin login;
        public FormKasir(FormLogin login)
        {
            InitializeComponent();
            this.login = login;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FormInputBarangKasir input = new FormInputBarangKasir(this);
            input.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FormInputUserKasir input = new FormInputUserKasir(this);
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
            
            loadgrid();
            loadcombo();
            generateID();
            dataGridView2.Columns[0].Width = 25;
            dataGridView2.Columns[3].Width = 85;

            textBox1.Enabled = false;
        }

        private void generateID()
        {
            MySqlCommand cmd = new MySqlCommand("select ifnull(max(ori.or_id),0) from orders ori", Program.conn);
            Program.conn.Open();
            int orderid = Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1;
            Program.conn.Close();
            textBox1.Text = orderid.ToString();

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
        private void loadgrid(string search)
        {
            string query = "SELECT it.`it_nama` AS 'Nama',CONCAT('Rp. ', FORMAT(it.it_price, 'c', 'id-ID')) AS 'Harga',it.it_stock as 'Stock' FROM item it,merk me, tipe ti WHERE it.`me_id`= me.`me_id` AND it.`ti_id` = ti.`ti_id` AND it.`it_nama` LIKE '%"+search+"%' ORDER BY it.it_id";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dtitem = new DataTable();
            da.Fill(dtitem);
            dataGridView1.DataSource = dtitem.DefaultView;

            query = "SELECT me.me_name,ti.ti_name,it.it_size,it.it_id FROM item it,merk me, tipe ti WHERE it.`me_id`= me.`me_id` AND it.`ti_id` = ti.`ti_id` AND it.`it_nama` LIKE '%" + search + "%'  ORDER BY it.it_id";
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
            string query = "SELECT it.`it_nama` AS 'Nama',CONCAT('Rp. ', FORMAT(it.it_price, 'c', 'id-ID')) AS 'Harga',it.it_stock as 'Stock'  FROM item it,merk me, tipe ti WHERE it.`me_id`= me.`me_id` AND it.`ti_id` = ti.`ti_id` AND me.me_name =" + merk+" AND ti.ti_name = "+tipe+" AND it.it_size = "+ukuran+" AND it.it_price >= "+min+" AND it.it_price <= "+max+" and it_status = 1 ORDER BY it.it_id";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dtitem = new DataTable();
            da.Fill(dtitem);
            dataGridView1.DataSource = dtitem.DefaultView;

            query = "SELECT me.me_name,ti.ti_name,it.it_size,it.it_id FROM item it,merk me, tipe ti WHERE it.`me_id`= me.`me_id` AND it.`ti_id` = ti.`ti_id` AND me.me_name =" + merk + " AND ti.ti_name = " + tipe + " AND it.it_size = " + ukuran + " AND it.it_price >= " + min + " AND it.it_price <= " + max + " and it_status = 1  ORDER BY it.it_id";
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
        int idx = -1;
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idx = e.RowIndex;
            numericUpDown1.Value = 1;
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

                int diskon = Convert.ToInt32(dtdiskon.Rows[0][1].ToString().Substring(7, dtdiskon.Rows[0][1].ToString().Length - 7).Substring(0, dtdiskon.Rows[0][1].ToString().Substring(7, dtdiskon.Rows[0][1].ToString().Length - 7).Length - 1));
                for (int i = 0; i < numericUpDown1.Value; i++)
                {
                    items.Add(new Item(Convert.ToInt32(dtdiskon.Rows[0][2].ToString()), dtdiskon.Rows[0][0].ToString(), diskon, buy1get1, buy2get3, Convert.ToInt32(dtdiskon.Rows[0][3].ToString())));
                    dataGridView2.Rows.Add(dataGridView2.Rows.Count + 1, items[items.Count - 1].nama,labelUkuran.Text, items[items.Count - 1].harga * (100 - items[items.Count - 1].diskon) / 100);
                }

                int harga = 0;
                int count = 0;
                
                labelNama.Text = "-";
                labelMerk.Text = "-";
                labelTipe.Text = "-";
                labelUkuran.Text = "-";
                labelHarga.Text ="-";
                labelDiskon.Text = "-";
                numericUpDown1.Value = 0;
                determine_diskon();
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (dataGridView2.Rows[i].DefaultCellStyle.BackColor == DataGridView.DefaultBackColor)
                    {
                        harga += Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value.ToString());
                        count = dataGridView2.Rows.Count;
                    }
                }
                label1.Text = "Total Pesanan : " + count;
                label2.Text = "Total Harga : Rp. " + harga;
            }
            else
            {
                MessageBox.Show("Jumlah harus lebih dari 0");
            }
        }

        private void determine_diskon()
        {
            int counterbuy1get1 = 0;
            int counterbuy2get3 = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                dataGridView2.Rows[i].DefaultCellStyle.BackColor = DataGridView.DefaultBackColor;
            }
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].buy1get1)
                {
                    counterbuy1get1++;
                }
                if (items[i].buy2get3)
                {
                    counterbuy2get3++;
                }
            }
            if (counterbuy1get1 > 1)
            {
                counterbuy1get1 = (counterbuy1get1 - counterbuy1get1 % 2)/2;
                List<int> temp = new List<int>();
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].buy1get1)
                    {
                        temp.Add(items[i].harga*(100-items[i].diskon)/100);
                    }
                }
                temp.Sort();
                for (int i = 0; i < temp.Count; i++)
                {
                    Console.WriteLine(temp[i]);
                }
                for (int i = 0; i < counterbuy1get1; i++)
                {
                    for (int j = 0; j < dataGridView2.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(dataGridView2.Rows[j].Cells[3].Value.ToString()) == temp[i] 
                            && dataGridView2.Rows[j].DefaultCellStyle.BackColor != Color.Green
                            && dataGridView2.Rows[j].DefaultCellStyle.BackColor != Color.Orange)
                        {
                            dataGridView2.Rows[j].DefaultCellStyle.BackColor = Color.Green;
                            break;
                        }
                    }
                }
            }
            if (counterbuy2get3 > 2)
            {
                counterbuy2get3 = (counterbuy2get3 - counterbuy2get3 % 3)/3;
                List<int> temp = new List<int>();
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].buy2get3)
                    {
                        temp.Add(items[i].harga);
                    }
                }
                temp.Sort();
                for (int i = 0; i < counterbuy2get3; i++)
                {
                    for (int j = 0; j < dataGridView2.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(dataGridView2.Rows[j].Cells[3].Value.ToString()) == temp[i]
                            && dataGridView2.Rows[j].DefaultCellStyle.BackColor != Color.Orange
                            && dataGridView2.Rows[j].DefaultCellStyle.BackColor != Color.Green)
                        {
                            dataGridView2.Rows[j].DefaultCellStyle.BackColor = Color.Orange;
                            break;
                        }
                    }
                }
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
                items.RemoveAt(transaksiidx);
            }
            int harga = 0;
            int count = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].DefaultCellStyle.BackColor == DataGridView.DefaultBackColor)
                {
                    harga += Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value.ToString());
                    count = dataGridView2.Rows.Count;
                }
            }

            label1.Text = "Total Pesanan : " + count;
            label2.Text = "Total Harga : Rp. " + harga;
            determine_diskon();
        }
        int transaksiidx = -1;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            transaksiidx = e.RowIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            items.Clear();
            int harga = 0;
            int count = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].DefaultCellStyle.BackColor == DataGridView.DefaultBackColor)
                {
                    harga += Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value.ToString());
                    count = dataGridView2.Rows.Count;
                }
            }

            label1.Text = "Total Pesanan : " + count;
            label2.Text = "Total Harga : Rp. " + harga;
            determine_diskon();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            loadgrid(textBox2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count>=1)
            {
                Program.conn.Open();
                MySqlTransaction trans = Program.conn.BeginTransaction();
                try
                {
                    MySqlCommand cmd = new MySqlCommand("set autocommit = 0;SET FOREIGN_KEY_CHECKS=0;");
                    cmd.Connection = Program.conn;
                    cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand("insert into orders values (@ID,@TOTAL,@TANGGAL)");
                    cmd.Connection = Program.conn;
                    cmd.Parameters.AddWithValue("@ID", textBox1.Text);
                    cmd.Parameters.AddWithValue("@TOTAL", label2.Text.Substring(17));
                    cmd.Parameters.AddWithValue("@TANGGAL", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.ExecuteNonQuery();

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        cmd = new MySqlCommand("select it.it_id from item it where it.it_nama = '" + dataGridView2.Rows[i].Cells[1].Value.ToString() + "' and it.it_size = "+ dataGridView2.Rows[i].Cells[2].Value.ToString() + "");
                        cmd.Connection = Program.conn;
                        string id = cmd.ExecuteScalar().ToString();

                        cmd = new MySqlCommand("select it.it_stock from item it where it.it_id = " + id, Program.conn);
                        int stock = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        stock--;

                        cmd = new MySqlCommand("update item set it_stock = " + stock + " where it_id = "+id, Program.conn);

                        int price = 0;
                        if (dataGridView2.Rows[i].DefaultCellStyle.BackColor!=Color.Green && dataGridView2.Rows[i].DefaultCellStyle.BackColor != Color.Orange)
                        {
                            price = Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value.ToString());
                        }
                        else
                        {
                            price = 0;
                        }

                        cmd = new MySqlCommand("insert into ordered_item values (0,@ID,@ITID,@PRICE)");
                        cmd.Connection = Program.conn;
                        cmd.Parameters.AddWithValue("@ID", textBox1.Text);
                        cmd.Parameters.AddWithValue("@ITID", id);
                        cmd.Parameters.AddWithValue("@PRICE", price);

                        cmd.ExecuteNonQuery();
                    }

                    string rank = "Tidak ada user";
                    if (comboBox4.SelectedItem != null)
                    {
                        rank = "";
                        cmd = new MySqlCommand("select us.us_id from user us where us.us_name = '" + comboBox4.SelectedItem.ToString() + "'", Program.conn);
                        string name = cmd.ExecuteScalar().ToString();

                        cmd = new MySqlCommand("select us.us_rank from user us where us.us_name = '" + comboBox4.SelectedItem.ToString() + "'", Program.conn);
                        rank = cmd.ExecuteScalar().ToString();

                        cmd = new MySqlCommand("insert into user_ordered values (@ID1,@ID2)");
                        cmd.Connection = Program.conn;
                        cmd.Parameters.AddWithValue("@ID1", name);
                        cmd.Parameters.AddWithValue("@ID2", textBox1.Text);
                        cmd.ExecuteNonQuery();
                    }
                    int total = Convert.ToInt32(label2.Text.Substring(17));
                    if (rank == "Bronze")
                    {
                        total = total * 95 / 100;
                        rank += " (+5% Diskon)";
                    }
                    else if (rank == "Silver")
                    {
                        total = total * 90 / 100;
                        rank += " (+10% Diskon)";
                    }
                    else if (rank == "Gold")
                    {
                        total = total * 85 / 100;
                        rank += " (+15% Diskon)";
                    }
                    cmd = new MySqlCommand("UPDATE orders SET or_hargatotal = " + total + " WHERE or_id = " + textBox1.Text,Program.conn);
                    cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand("SET FOREIGN_KEY_CHECKS=1;");
                    cmd.Connection = Program.conn;
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    MessageBox.Show("Total Transaksi : Rp." + total + Environment.NewLine +
                                    "Total Barang : " + label1.Text.Substring(16) + " barang"+Environment.NewLine+
                                    "Tanggal Beli : "+ DateTime.Now.ToString("dd MMMM yyyy")+ Environment.NewLine+
                                    "Rank User : "+rank+Environment.NewLine+
                                    "Berhasil");
                    FormReport report = new FormReport("receipt",textBox1.Text);
                    report.ShowDialog();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Console.WriteLine(ex.Message);
                }
                dataGridView2.Rows.Clear();
                button2_Click(null, null);
                Program.conn.Close();
                generateID();
            }
            else
            {
                MessageBox.Show("Pesan lebih dari 1!");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (idx != -1)
            {
                int stock = Convert.ToInt32(dataGridView1.Rows[idx].Cells[2].Value.ToString());
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (dataGridView2.Rows[i].Cells[1].Value.ToString() == labelNama.Text &&
                        dataGridView2.Rows[i].Cells[2].Value.ToString() == labelUkuran.Text)
                    {
                        stock--;
                    }
                }
                if (numericUpDown1.Value > stock)
                {
                    numericUpDown1.Value = stock;
                    MessageBox.Show("Melebihi Stock!");
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            FormListDiscountKasir diskon = new FormListDiscountKasir(this);
            this.Hide();
            diskon.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            login.Show();
            this.Hide();
        }
    }
}
