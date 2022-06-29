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
    public partial class FormTransaksi : Form
    {
        FormKasir kasir;
        FormAdmin admin;
        public FormTransaksi(FormKasir kasir)
        {
            InitializeComponent();
            this.kasir = kasir;
        }
        public FormTransaksi(FormAdmin admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (kasir == null)
            {
                admin.Show(); 
            }
            else
            {
                kasir.Show();
            }
            this.Close();
        }

        private void FormTransaksi_Load(object sender, EventArgs e)
        {
            loadgrid();
        }

        DataTable dtorder;
        private void loadgrid()
        {
            string query = "SELECT ori.`or_id` AS 'ID',ori.`or_hargatotal` AS 'Total',ori.`or_tanggalorder` AS 'Harga',us.`us_username` AS 'User' FROM orders ori LEFT JOIN user_ordered uo ON uo.`or_id`=ori.`or_id` LEFT JOIN USER us ON us.`us_id`=uo.`us_id` ORDER BY 1";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dtorder = new DataTable();
            da.Fill(dtorder);
            dataGridView1.DataSource = dtorder.DefaultView;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormReport report = new FormReport("totaltransaksi");
            report.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormReport report = new FormReport("sepatu");
            report.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormReport report = new FormReport("brand");
            report.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormReport report = new FormReport("tipe");
            report.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormReport report = new FormReport("user");
            report.ShowDialog();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;

            FormTransaksiDetail detail = new FormTransaksiDetail(Convert.ToInt32(dataGridView1.Rows[idx].Cells[0].Value.ToString()));
            detail.ShowDialog();
        }
    }
}
