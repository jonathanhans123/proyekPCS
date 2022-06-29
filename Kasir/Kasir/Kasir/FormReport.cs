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
    public partial class FormReport : Form
    {
        string report;
        string id;
        public FormReport(string report)
        {
            InitializeComponent();
            this.report = report;
        }
        public FormReport(string report,string id)
        {
            InitializeComponent();
            this.report = report;
            this.id = id;
            
        }

        private void FormReport_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            button2.Visible = false;

            if (report == "totaltransaksi")
            {
                CrystalReport1 rpt = new CrystalReport1();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                rpt.SetParameterValue(0, DateTime.MinValue.ToString()) ;
                rpt.SetParameterValue(1, DateTime.Now.ToString());
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
                comboBox1.Visible = false;
                button1.Visible = false;
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                button2.Visible = true;
            }
            else if (report == "sepatu")
            {
                CrystalReport2 rpt = new CrystalReport2();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                rpt.SetParameterValue(0, "Semua");
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
                comboBox1.Visible = true;
                button1.Visible = true;
                MySqlCommand cmd = new MySqlCommand("select it_nama from item", Program.conn);
                Program.conn.Open();
                cmd.ExecuteReader();
                Program.conn.Close();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dtmerk = new DataTable();
                da.Fill(dtmerk);
                comboBox1.Items.Add("Semua");
                for (int i = 0; i < dtmerk.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dtmerk.Rows[i][0].ToString());
                }
            }
            else if (report == "brand")
            {
                CrystalReport4 rpt = new CrystalReport4();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                rpt.SetParameterValue(0, "Semua");
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
                comboBox1.Visible = true;
                button1.Visible = true;
                MySqlCommand cmd = new MySqlCommand("select me_name from merk",Program.conn);
                Program.conn.Open();
                cmd.ExecuteReader();
                Program.conn.Close();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dtmerk = new DataTable();
                da.Fill(dtmerk);
                comboBox1.Items.Add("Semua");
                for (int i = 0; i < dtmerk.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dtmerk.Rows[i][0].ToString());
                }
            }
            else if (report == "tipe")
            {
                CrystalReport5 rpt = new CrystalReport5();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                rpt.SetParameterValue(0, "Semua");
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
                comboBox1.Visible = true;
                button1.Visible = true;

                MySqlCommand cmd = new MySqlCommand("select ti_name from tipe", Program.conn);
                Program.conn.Open();
                cmd.ExecuteReader();
                Program.conn.Close();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dtmerk = new DataTable();
                da.Fill(dtmerk);
                comboBox1.Items.Add("Semua");
                for (int i = 0; i < dtmerk.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dtmerk.Rows[i][0].ToString());
                }

            }
            else if (report == "user")
            {
                CrystalReport3 rpt = new CrystalReport3();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                rpt.SetParameterValue(0, "Semua");
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
                comboBox1.Visible = true;
                button1.Visible = true;

                MySqlCommand cmd = new MySqlCommand("select us_name from user", Program.conn);
                Program.conn.Open();
                cmd.ExecuteReader();
                Program.conn.Close();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dtmerk = new DataTable();
                da.Fill(dtmerk);
                comboBox1.Items.Add("Semua");
                for (int i = 0; i < dtmerk.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dtmerk.Rows[i][0].ToString());
                }
            }
            else if (report == "receipt")
            {
                CrystalReport6 rpt = new CrystalReport6();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                rpt.SetParameterValue(0, Convert.ToInt32(id));
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
                comboBox1.Visible = false;
                button1.Visible = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (report == "brand")
            {
                CrystalReport4 rpt = new CrystalReport4();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                rpt.SetParameterValue(0, comboBox1.SelectedItem);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
            else if (report == "tipe")
            {
                CrystalReport5 rpt = new CrystalReport5();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                rpt.SetParameterValue(0, comboBox1.SelectedItem);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
            else if (report == "user")
            {
                CrystalReport3 rpt = new CrystalReport3();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                rpt.SetParameterValue(0, comboBox1.SelectedItem);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }else if (report == "sepatu")
            {
                CrystalReport2 rpt = new CrystalReport2();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                rpt.SetParameterValue(0, comboBox1.SelectedItem);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CrystalReport1 rpt = new CrystalReport1();
            rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
            rpt.SetParameterValue(0, dateTimePicker1.Value.ToString());
            rpt.SetParameterValue(1, dateTimePicker2.Value.ToString());
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
    }
}
