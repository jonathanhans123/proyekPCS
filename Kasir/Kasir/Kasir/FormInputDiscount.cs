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
    public partial class FormInputDiscount : Form
    {
        FormInputBarang barang;
        FormListDiscount discount;
        public FormInputDiscount(FormInputBarang barang)
        {
            InitializeComponent();
            this.barang = barang;
        }

        private void FormInputDiscount_Load(object sender, EventArgs e)
        {
            loadcombo();
        }
        private void loadcombo()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Name");
            comboBox1.Items.Add("Merk");
            comboBox1.Items.Add("Tipe");
            comboBox1.Items.Add("Size");

            comboBox2.Items.Clear();
            comboBox2.Items.Add("Diskon Percentage");
            comboBox2.Items.Add("Buy 1 Get 1 Free");
            comboBox2.Items.Add("Buy 2 Get 3 Free");
            comboBox2.SelectedIndex = 0;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            barang.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString()!=""&& comboBox2.SelectedItem.ToString() != ""&& comboBox3.SelectedItem.ToString() != "")
            {
                string query = "INSERT INTO discount VALUES(0,?NAME,?CATE,?VALUE,?TYPE,1);";
                if (comboBox2.SelectedItem.ToString() != "Diskon Percentage")
                {
                    numericUpDown1.Value = 0;
                }
                MySqlCommand cmd = new MySqlCommand(query, Program.conn);
                cmd.Parameters.Add(new MySqlParameter("NAME", comboBox3.SelectedItem.ToString()));
                cmd.Parameters.Add(new MySqlParameter("CATE", comboBox1.SelectedItem.ToString()));
                cmd.Parameters.Add(new MySqlParameter("VALUE", numericUpDown1.Value));
                cmd.Parameters.Add(new MySqlParameter("TYPE", comboBox2.SelectedItem.ToString()));

                Program.conn.Open();
                cmd.ExecuteNonQuery();
                Program.conn.Close();
                MessageBox.Show("Berhasil Add");

                discount = new FormListDiscount(barang);
                discount.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Isi semua field!");
            }
            
        }
        DataTable dtnama;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            if (comboBox1.SelectedItem.ToString()=="Name")
            {
                string query = "SELECT it_id,it_nama FROM ITEM order by 1;";

                MySqlCommand cmd = new MySqlCommand(query, Program.conn);

                Program.conn.Open();
                cmd.ExecuteReader();
                Program.conn.Close();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                dtnama = new DataTable();
                da.Fill(dtnama);
                for (int i = 0; i < dtnama.Rows.Count; i++)
                {
                    comboBox3.Items.Add(dtnama.Rows[i][1]);
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "Merk")
            {
                string query = "SELECT me_id,me_name FROM merk order by 1;";

                MySqlCommand cmd = new MySqlCommand(query, Program.conn);

                Program.conn.Open();
                cmd.ExecuteReader();
                Program.conn.Close();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                dtnama = new DataTable();
                da.Fill(dtnama);
                for (int i = 0; i < dtnama.Rows.Count; i++)
                {
                    comboBox3.Items.Add(dtnama.Rows[i][1]);
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "Tipe")
            {
                string query = "SELECT ti_id,ti_name FROM tipe order by 1;";

                MySqlCommand cmd = new MySqlCommand(query, Program.conn);

                Program.conn.Open();
                cmd.ExecuteReader();
                Program.conn.Close();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                dtnama = new DataTable();
                da.Fill(dtnama);
                for (int i = 0; i < dtnama.Rows.Count; i++)
                {
                    comboBox3.Items.Add(dtnama.Rows[i][1]);
                }
            }
            else if (comboBox1.SelectedItem.ToString() == "Size")
            {
                string query = "SELECT distinct it_size FROM item order by 1;";

                MySqlCommand cmd = new MySqlCommand(query, Program.conn);

                Program.conn.Open();
                cmd.ExecuteReader();
                Program.conn.Close();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                dtnama = new DataTable();
                da.Fill(dtnama);
                for (int i = 0; i < dtnama.Rows.Count; i++)
                {
                    comboBox3.Items.Add(dtnama.Rows[i][0]);
                }
            }
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString()!="Diskon Percentage")
            {
                label4.Visible = false;
                numericUpDown1.Visible = false;
            }
            else
            {
                label4.Visible = true;
                numericUpDown1.Visible = true;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(comboBox3.SelectedItem.ToString());
        }
    }
}
