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
    public partial class FormTransaksiDetail : Form
    {
        int idx;
        public FormTransaksiDetail(int idx)
        {
            InitializeComponent();
            this.idx = idx;
        }
        DataTable dtdetail;
        private void FormTransaksiDetail_Load(object sender, EventArgs e)
        {
            string query = "SELECT oi.oi_id,oi.or_id,it.it_nama,oi.oi_itemprice FROM orders ori,ordered_item oi,item it WHERE ori.or_id = oi.or_id AND it.it_id = oi.it_id AND ori.or_id = "+idx+" ORDER BY 1";
            MySqlCommand cmd = new MySqlCommand(query, Program.conn);

            Program.conn.Open();
            cmd.ExecuteReader();
            Program.conn.Close();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dtdetail = new DataTable();
            da.Fill(dtdetail);
            dataGridView1.DataSource = dtdetail.DefaultView;
        }
    }
}
